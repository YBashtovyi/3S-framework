<template>
    <q-dialog persistent v-model="isDialogVisible">
      <q-card style="width: 30vw; max-width: 80vw;">
        <q-card-section class="row items-center q-pb-none">
          <div class="text-h6">{{ title }}</div>
            <div class="text-caption text-grey q-pt-lg">
                Логін має бути від 4-ьох до 40-а символів латиницею та не містити заборонені символи.
                <!-- Дозволені знаки: _  .  -  @ -->
             </div>
        </q-card-section>
        <q-form @submit="onSubmit">
          <q-card-section class="q-mt-md">
            <div class="row">
                <div class="col-xs-12 col-md-12 q-pb-lg">
                  <q-input
                    autocomplete="userName"
                    :loading="isLoading"
                    :disable="isLoading"
                    :rules="loginRules"
                    outlined
                    dense
                    :label="loginLabel"
                    v-mask="maskLogin"
                    v-model="newLogin"
                    type="text" />
              </div>
              <div class="col-xs-12 col-md-12 q-pb-lg">
                  <q-input
                    autocomplete="new-password"
                    :loading="isLoading"
                    :disable="isLoading"
                    outlined
                    dense
                    :label="newPasswordLabel"
                    :rules="passwordRules"
                    v-mask="maskPassword"
                    v-model="newPassword"
                    type="password" />
              </div>
              <div class="col-xs-12 col-md-12 q-mb-lg">
                  <q-input
                    autocomplete="new-password"
                    :loading="isLoading"
                    :disable="isLoading"
                    outlined
                    dense
                    :label="newPassword2Label"
                    :rules="password2Rules"
                    v-mask="maskPassword"
                    v-model="newPassword2"
                    type="password" />
              </div>
            </div>

            <div class="text-caption text-grey">
                Пароль має бути не менше 8 символів, 
                містити принаймні один символ нижнього регістру,
                один символ верхнього регістру,
                одну цифру
                і не повинен збігатися з ім'ям та ел.поштою.
            </div>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn label="Відмінити" color="negative" flat @click="onCardCancel" />
            <q-btn :disable="!canSubmit" type="submit" label="Змінити" color="primary" class="on-right" />
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
</template>

<script>
    import { 
        loginRegex,
        hasSymbolInLowercaseRegex,
        hasSymbolInUppercaseRegex,
        hasDigitRegex,
        min8SymbolsLength
    } from '../../utils/patterns'
    import { mapState } from 'vuex'
    import { 
        fetchMisUser,
        updateCredentials
    } from '../../services/identity-api/misUsers-api'
    import maskUtil from '../../mixins/maskUtil'
    import { isNotEmpty, isEmpty, stringEmpty } from '../../utils/function-helpers'

    import get from 'lodash.get'
    import isEqual from 'lodash.isequal'
    
    export default {
        mixins: [maskUtil],
        data: function() {
            return {
                title:                              "Змінити логін/пароль користувача",
                loginLabel:                         "Логін користувача в системі",
                newPasswordLabel:                   "Придумайте новий пароль",
                newPassword2Label:                  "Новий пароль ще раз",
                successMessage:                     "Дані для аутентифікації успішно змінено.",
                hasNoLowercaseSymbolsError:         "Пароль мусить містити принаймні один символ нижнього регістру",
                hasNoUppercaseSymbolsError:         "Пароль мусить містити принаймні один символ верхнього регістру",
                hasNoDigitError:                    "Пароль мусить містити принаймні одну цифру",
                isLongerThan8SymbolsError:          "Мінімальна довжина паролю 8 символів",
                requieredFieldError:                "Обов'язкове поле",
                passwordsDontMatch:                 "Паролі не співпадають",
                loginIncorrectError:                "Логін не відповідає нормам",
                identityServerError:                "Помилка під час виконання запиту в Identity server",
                identityServerErrorwWhileUpdate:    "Помилка під час спроби оновлення користувача",
                userId: stringEmpty(),
                currentLogin: stringEmpty(),
                newLogin: stringEmpty(),
                newPassword: stringEmpty(),
                newPassword2: stringEmpty(),
                defaultPasswordRules: [],
                isLoading: false
            };
        },
        
        props: { 
            isDialogVisible: Boolean 
        },

        mounted() {
            this.userId = get(this.user, "userId", null)
            this.setIdentityUserCredentialsInformation()
            this.defaultPasswordRules = [
                val => (isEmpty(val) || hasSymbolInLowercaseRegex.test(val))    || this.hasNoLowercaseSymbolsError,
                val => (isEmpty(val) || hasSymbolInUppercaseRegex.test(val))    || this.hasNoUppercaseSymbolsError,
                val => (isEmpty(val) || hasDigitRegex.test(val))                || this.hasNoDigitError,
                val => (isEmpty(val) || min8SymbolsLength.test(val))            || this.isLongerThan8SymbolsError
            ]
        },

        methods: {

            setIdentityUserCredentialsInformation() {
                
                const setLogin = (identityUser) => {
                    const login = get(identityUser, "login", stringEmpty())
                    this.currentLogin = login
                    this.newLogin = this.currentLogin
                }

                const showErrorMessage = () => {
                    this.showSystemMessage("negative", this.identityServerError)
                }

                const errorCallback = () => {
                    this.closeDialog()
                    showErrorMessage()
                }

                this.changeLoadingState()

                fetchMisUser(this.userId)
                    .then(setLogin)
                    .then(this.changeLoadingState)
                    .catch(errorCallback)
            },

            closeDialog() {
                this.$emit('dialogSubmitedEvent')
            },

            changeLoadingState() {
                this.isLoading = !this.isLoading
            },

            showSystemMessage(color, message) {
                this.$q.notify({ color, message })
            },

            onCardCancel() {
                this.closeDialog()
            },
            
            onSubmit() {
                const context = {
                    id: this.userId,
                    login: this.newLogin,
                    password: btoa(this.newPassword),
                    fromEvomis: true
                }

                const showSuccessMessage = () => {
                    this.showSystemMessage("positive", this.successMessage)
                }

                const showErrorMessage = () => {
                    this.showSystemMessage("negative", this.identityServerErrorwWhileUpdate)
                }

                const errorCallback = () => {
                    this.closeDialog()
                    showErrorMessage()
                }
                
                this.changeLoadingState()

                updateCredentials(context)
                    .then(this.closeDialog)
                    .then(showSuccessMessage)
                    .catch(errorCallback)
            }
        },

        computed: { 
            ...mapState("baseElements", ['user']),

            passwordRules() {
                return this.defaultPasswordRules
            },

            password2Rules() {
                const passwordRules = [
                    val => isEqual(this.newPassword, val) || this.passwordsDontMatch,
                    ...this.defaultPasswordRules
                ]
                return passwordRules
            },

            loginRules() {
                return [val => (isEmpty(val) || loginRegex.test(val)) || this.loginIncorrectError]
            },

            canSubmit() {
                return isNotEmpty(this.newLogin) ||
                    (isNotEmpty(this.newPassword) && isNotEmpty(this.newPassword2))
            }
        }
    }
</script>
