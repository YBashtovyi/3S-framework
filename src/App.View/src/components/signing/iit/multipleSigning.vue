<template>
  <q-card flat>
    <q-card-section>
      <q-tabs
        v-model="keyValue"
        dense
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        inline
        narrow-indicator
        align="justify"
      >
        <q-tab name="file" label="Файловий носій" />
        <q-tab name="flashDrive" label="Захищений носій" />
      </q-tabs>
      <q-separator />
      <q-tab-panels
        v-model="keyValue"
        animated
        transition-prev="jump-up"
        transition-next="jump-down"
      >
        <q-tab-panel name="file">
          <transition
            appear
            enter-active-class="animated fadeIn"
            leave-active-class="animated fadeOut"
          >
            <div :showing="!visiblePluginTransition">
              <div class="row">
                <div class="col-xs-12 col-md-12">
                  <q-select
                    outlined
                    dense
                    options-dense
                    label="Виберіть АЦСК *"
                    v-model="caServer"
                    :options="caServers"
                    @input="caServerChanged"
                    @filter="getCaServers"
                    option-value="code"
                    option-label="value"
                    :rules="[val => !!val || 'Обов\'язкове поле']"
                    class="q-mr-md-md q-mb-xs-md"
                  >
                    <template v-slot:no-option>
                      <q-item>
                        <q-item-section class="text-grey">Відсутні значення</q-item-section>
                      </q-item>
                    </template>
                  </q-select>
                </div>
                <div class="col-xs-12 col-md-12">
                  <input
                    id="addKeyFile"
                    type="file"
                    accept=".dat, .pfx, .cnt, .pk8, Key-6.dat, .jks, .zs2, .pck"
                    @change="loadFileData"
                    class="hidden"
                    ref="inputFile"
                  />
                  <q-input
                    v-model="keyText"
                    outlined
                    dense
                    class="q-mr-md-md q-mb-xs-md"
                    autogrow
                    :rules="[val => !!val || 'Обов\'язкове поле']"
                    label="Оберіть ключ *"
                    @click="$refs.inputFile.click()"
                  >
                    <template v-slot:append>
                      <q-icon
                        name="attach_file"
                        class="text-grey-7 cursor-pointer"
                        style="font-size: 24px"
                      />
                    </template>
                  </q-input>
                </div>
                <div class="col-xs-12 col-md-12">
                  <q-input
                    outlined
                    dense
                    v-model="password"
                    :type="isPwdMask ? 'password' : 'text'"
                    label="Пароль *"
                    ref="password"
                    class="q-mr-md-md q-mb-xs-md"
                  >
                    <template v-slot:append>
                      <q-icon
                        :name="isPwdMask ? 'visibility_off' : 'visibility'"
                        class="cursor-pointer"
                        @click="isPwdMask = !isPwdMask"
                        style="font-size: 22px"
                      />
                    </template>
                  </q-input>
                </div>
                <div class="col-xs-12 col-md-12">
                  <div class="bg-grey-2 q-pa-sm rounded-borders">
                    <q-list>
                      <q-item>
                        <q-item-section avatar top>
                          <q-checkbox v-model="isSaveSigningInfo" />
                        </q-item-section>
                        <q-item-section>
                          <q-item-label>Зберегти АЦСК та файл ключа</q-item-label>
                          <q-item-label
                            caption
                          >Увага! Дані зберігаються до перезавантаження сторінки</q-item-label>
                        </q-item-section>
                      </q-item>
                    </q-list>
                  </div>
                </div>
              </div>
              <div class="row items-center q-mt-md">
                <q-btn
                  @click="tryLoadKeyAndSignData"
                  :loading="registerLoading"
                  glossy
                  unelevated
                  class="q-ml-auto q-mr-md-md"
                  color="primary"
                  :label="registerButtonCaption"
                >
                  <template v-slot:loading>
                    <q-spinner-gears class="on-left" />Підпис...
                  </template>
                </q-btn>
              </div>
            </div>
          </transition>
          <q-inner-loading :showing="visiblePluginTransition">
            <q-spinner-gears size="50px" color="primary" />
          </q-inner-loading>
        </q-tab-panel>

        <q-tab-panel name="flashDrive">
          <transition
            appear
            enter-active-class="animated fadeIn"
            leave-active-class="animated fadeOut"
          >
            <div v-if="errorLoadLib" v-html="notFoundExtention"></div>
            <div v-else :showing="visibleFileDriveTransition">
              <div class="row">
                <div v-if="signedData === null">
                  <div id="mainBlock1" class="Block">
                    <div id="pkTypesBlock" class="Block"></div>
                    <div id="dimmerViewBlock" style="display:none;">
                      <div
                        style="display:table;position: absolute;top:0px;left:0px;height:100%;width:100%;"
                      >
                        <div style="display: table-cell;vertical-align: middle;">
                          <label id="dimmerViewMessageLabel" style="line-height:normal"></label>
                        </div>
                      </div>
                    </div>
                    <div id="statusBlock" class="Block" style="display:none">
                      <label id="statusLabel"></label>
                    </div>
                    <div id="installBlock" class="Block" style="display:none;"></div>

                    <div id="pkBlock" class="Block">
                      <div id="pkCABloсk" class="Block" style="visibility: hidden; height: 0">
                        <div id="pkCASelectBlock" class="Block">
                          <label id="pkCATitleLabel">Кваліфікований надавач ел. довірчих послуг:</label>
                          <br />
                          <select id="pkCASelect" class="Select"></select>
                        </div>
                      </div>

                      <div id="pkReadKMBlock" class="Block" style="display:none">
                        <q-chip dense>
                          <q-avatar icon="vpn_key" color="primary" text-color="white" size="sm" />
                          <div id="privateSignKeyName"></div>
                        </q-chip>
                        <div class="col-xs-12 col-md-12">
                          <div
                            id="pkReadKMSelectBlock"
                            class="Block"
                            style="visibility: hidden; height: 0"
                          >
                            <label id="pkReadKMTitleLabel">Носій особистого ключа:</label>
                            <br />
                            <select id="pkReadKMSelect" class="Select"></select>
                          </div>
                          <div id="pkReadKMUserBlock" class="Block" style="display:none">
                            <label id="pkReadKMUserTitleLabel">Ім'я користувача:</label>
                            <br />
                            <input id="pkReadKMUserTextField" class="TextField" disabled />
                          </div>
                          <div id="pkReadKMPasswordBlock" class="Block">
                            <span
                              style="font-size: 14px; color: #969AA3; display: block; margin-bottom: 8px;"
                            >
                              Введіть пароль
                              для підтвердження накладання підпису
                            </span>
                            <div class="col-xs-12 col-md-12">
                              <q-input
                                outlined
                                dense
                                v-model="password"
                                v-mask="maskPassword"
                                :rules="[val => /(^[a-zA-Z0-9$@$!%*?&^-_. +])/g.test(String(val)) || 'Пароль повинен містити латинські букви']"
                                :type="isPwdMask ? 'password' : 'text'"
                                label="Пароль *"
                                ref="password"
                                class="q-mr-md-md q-mb-xs-md"
                                for="pkReadKMPasswordTextField"
                              >
                                <template v-slot:append>
                                  <q-icon
                                    :name="isPwdMask ? 'visibility_off' : 'visibility'"
                                    class="cursor-pointer"
                                    @click="isPwdMask = !isPwdMask"
                                    style="font-size: 22px"
                                  />
                                </template>
                              </q-input>
                            </div>
                          </div>
                          <div id="pkReadKMCertsBlock" class="Block" style="display:none">
                            <label>Сертифікати ос. ключа (*.cer або *.crt):</label>
                            <br />
                            <input
                              id="pkReadKMCertsTextField"
                              class="TextField"
                              type="text"
                              readonly="true"
                              style="float:left;"
                              onclick="document.getElementById('pkReadKMCertsInput').click();"
                            />
                            <div
                              id="pkReadKMCertsFileSelectButton"
                              class="Button"
                              style="margin-left:8px;"
                            >
                              <a
                                href="javascript:void(0);"
                                title="Обрати"
                                style="pointer-events:inherit;"
                                onclick="document.getElementById('pkReadKMCertsInput').click();"
                              >Обрати</a>
                              <input
                                id="pkReadKMCertsInput"
                                type="file"
                                multiple="true"
                                accept=".cer, .crt"
                              />
                            </div>
                          </div>
                        </div>
                        <div class="row items-center">
                          <q-btn
                            @click="tryLoadKeyAndSignDataFileDrive"
                            glossy
                            unelevated
                            class="q-ml-auto q-mt-md"
                            color="primary"
                            :label="registerButtonCaption"
                          />
                        </div>
                      </div>
                    </div>
                    <div id="signBlock" class="Block" style="display:none">
                      <div id="signParamsBlock" class="Block">
                        <div id="signTypeBlock" class="Block" style="visibility: hidden; height: 0">
                          <label>Тип підпису:</label>
                          <br />
                          <input
                            type="radio"
                            name="signTypesRadio"
                            value="1"
                            align="left"
                            checked="checked"
                          />
                          <label>Дані та підпис в одному файлі (формат CAdES)</label>
                          <br />
                          <input type="radio" name="signTypesRadio" value="2" align="left" />
                          <label>Дані та підпис окремими файлами (формат CAdES)</label>
                          <br />
                          <input type="radio" name="signTypesRadio" value="3" align="left" />
                          <label>Дані та підпис в архіві (формат ASiC-S)</label>
                          <br />
                        </div>
                        <div id="signAlgoBlock" class="Block" style="visibility: hidden; height: 0">
                          <label>Алгоритм підпису:</label>
                          <br />
                          <select id="signAlgoSelect" class="Select"></select>
                        </div>
                        <div
                          id="signFormatBlock"
                          class="Block"
                          style="visibility: hidden; height: 0"
                        >
                          <label>Формат підпису:</label>
                          <br />
                          <select id="signFormatSelect" class="Select">
                            <option value="16">з повними даними для перевірки (CAdES-X-Long)</option>
                            <option value="8">з посиланням на повні дані для перевірки (CAdES-C)</option>
                            <option value="4">з позначкою часу від КЕП (CAdES-T)</option>
                            <option value="1">базовий (CAdES-BES)</option>
                          </select>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </transition>
          <q-inner-loading :showing="visibleFileDriveTransition">
            <q-spinner-gears size="50px" color="primary" />
          </q-inner-loading>
        </q-tab-panel>
      </q-tab-panels>
    </q-card-section>
  </q-card>
</template>

<script>
import multipleSigning from './mixins/multipleSigning'
import maskUtil from "@/mixins/maskUtil"

export default {
  mixins: [multipleSigning, maskUtil]
}
</script>