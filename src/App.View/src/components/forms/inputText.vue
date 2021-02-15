<template>
  <div class="row">
    <q-input
      v-if="type === `text`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      :label-color="labelColor"
      class="q-mb-md full-width"
      :outlined="outlined"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `number`"
      v-model.number="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      type="number"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `name`"
      v-model="model"
      v-mask="maskName"
      @input="onChanged"
      :disable="disabled"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      :input-class="'text-capitalize'"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />
    <q-input
      v-if="type === `passportAndNationalId`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      maxlength="10"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />
    <q-input
      v-if="type === `passport`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      maxlength="8"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />
    <q-input
      v-else-if="type === `ipn`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      minlength="10"
      maxlength="10"
      mask="##########"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      :disable="disabled"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `secretCode`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
      minlength="6"
      maxlength="20"
    />

    <q-input
      v-else-if="type === `unzr`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      class="q-mb-md full-width"
      mask="########-#####"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `code`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      mask="##########"
      minlength="8"
      maxlength="10"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `email`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      :disable="disabled"
      class="q-mb-md full-width"
      type="email"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `phone`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      v-mask="maskPhone"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-else-if="type === `readonly`"
      v-model="model"
      :label="labelValue"
      class="q-mb-md full-width"
      stack-label
      readonly
      :outlined="outlined"
      :label-color="labelColor"
    />

    <q-input
      v-else-if="type === `search`"
      v-model="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      class="q-mb-md full-width"
      dense
      clearable
      :outlined="outlined"
      :label-color="labelColor"
      clear-icon="close"
      ref="valid"
    />

    <q-input
      v-if="type === `latitude`"
      v-model.number="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />
    
    <q-input
      v-if="type === `longitude`"
      v-model.number="model"
      @input="onChanged"
      :bg-color="color"
      :rules="requiredRule"
      :label="labelValue"
      class="q-mb-md full-width"
      :outlined="outlined"
      :label-color="labelColor"
      dense
      clearable
      clear-icon="close"
      ref="valid"
    />

  </div>
</template>
<script>
import maskUtil from "@/mixins/maskUtil";
import validationMixin from '../../mixins/innValidation';
export default {
  name: "inputText",
  data() {
    return {
      model: "",
      requiredRule: [],
      color: "transparent"
    };
  },
  mixins: [maskUtil, validationMixin],
  props: {
    label: String,
    required: Boolean,
    disabled: Boolean,
    value: [String, Number],
    mask: String,
    type: String,
    validate: Boolean,
    genderCode: String,
    birthDate: String,
    outlined: {
      type: Boolean,
      default: true
    },
    labelColor: String
  },
  computed: {
    hasError() {
      return this.$refs.valid.hasError;
    },
    cardId() {
      return this.$route.params.id;
    },
    labelValue() {
      if (this.required) {
        return this.label + " *";
      } else {
        return this.label;
      }
    }
  },
  methods: {
    onChanged() {
      if (this.cardId) {
        setTimeout(() => {
          this.$refs.valid.validate();
          if (!this.hasError) {
            this.color = "green-1";
          } else {
            this.color = "red-1";
          }

          if (!this.required && !this.model) {
            this.color = "transparent";
          }
        }, 0);
      }
    }
  },
  watch: {
    model() {
      // if (!this.model) {
      //   this.onChanged();
      // }
  
      if ((this.model || this.model === 0) && this.type !== "name") {
        this.$emit("updateData", this.model);
      } else if (this.model && this.type === "name") {
        this.$emit("updateData", this.capitalizeFirstLetter(this.model));
      } else {
        if (this.type === "number") {
          this.$emit("updateData", null);
        } else {
          this.$emit("updateData", "");
        }
      }

      if (this.type === "text" && this.required) {
        this.requiredRule = [];
        this.requiredRule.push(val => !!val || "Обов'язкове поле");
      }
      
      // toogle validation for input type=passportAndNationalId
      if (this.type === "passportAndNationalId") {
        this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /^([0-9]{9,10}$|[А-ЯЁЇIЄҐ]{2}[0-9]{6}$)/.test(val) ||
            "Невірний формат документу"
        );
      } 
      
      // else if (this.type === "passportAndNationalId" && !this.model && !this.required) {
      //  this.requiredRule = [true];
      // }

      // toogle validation for input type=pasport
      if (this.type === "passport") {
        this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /^[А-ЩЬЮЯҐЄІЇ-\s']{2}[0-9]{6}/.test(val) ||
            "Невірний формат документу"
        );
      }

      // toogle validation for input type=email
      if (this.type === "email" && this.model && !this.required) {
        // this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/gim.test(
              String(val).toLowerCase()
            ) || "Невірний формат"
        );
      } else if (this.type === "email" && !this.model && !this.required) {
        this.requiredRule = [true];
      }

      // toogle validation for input type=latitude
      if (this.type === "latitude" && this.model && !this.required) {
        // this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$/g.test(String(val)) ||
            'Невірний формат'
        );
      } else if (this.type === "latitude" && !this.model && !this.required) {
        this.requiredRule = [true];
      }

      // toogle validation for input type=longitude
      if (this.type === "longitude" && this.model && !this.required) {
        // this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$/g.test(String(val)) ||
            'Невірний формат'
        );
      } else if (this.type === "longitude" && !this.model && !this.required) {
        this.requiredRule = [true];
      }

      // toogle validation for input type=name
      if (this.type === "name" && this.model && !this.required) {
        // this.requiredRule = [];
        this.requiredRule.push(
          val =>
            /(^[А-ЩЬЮЯҐЄІЇа-щьюяґєії]{1})+([А-ЩЬЮЯҐЄІЇа-щьюяґєії-\s']{0,70})/g.test(
              String(val)
            ) || "Значення має починатись з літери",
          val => (val && val.length >= 2) || "Введіть не менше 2-х символів"
        );
      } else if (this.type === "name" && !this.required) {
        this.requiredRule = [true];
      }

      // toogle validation for input type=ipn
      
      if (this.type === "ipn") {
        this.requiredRule = [];

        if (this.model && this.model.length > 0) {

          this.requiredRule.push(val => (val && val.length === 10) || "Значення має складатись із 10 цифр")

          if (this.genderCode) {

          this.requiredRule.push(
            val =>
              (val && val.length === 10 && this.isValidUATaxId(val, this.genderCode, this.birthDate)) ||
              "РНОКПП не валідний"
            )
          }
        }
        else {

          this.requiredRule = [true]
        }        
      } 

      // toogle validation for input type=number
      if (this.type === "number" && this.model && !this.required) {
        this.requiredRule = [];
        this.requiredRule.push(val => val >= 0 || "Не може бути від'ємним");
      } else if (this.type === "number" && !this.required) {
        this.requiredRule = [true];
      }

      // if (this.cardId) {
      //   setTimeout(() => {
      //     this.$refs.valid.validate();
      //     this.$emit("valid", this.hasError);
      //   }, 0);
      // }
    },

    value() {

      this.model = this.value
    },

    validate() {
      this.$refs.valid.validate();
      this.$emit("valid", this.hasError);
    }
  },
  mounted() {
    if (this.value || this.value === 0) {
      this.model = this.value;
    }
    // validate required
    if (this.required) {
      this.requiredRule.push(val => !!val || "Обов'язкове поле");
    }
    // validate email
    if (this.type === "email" && this.required) {
      this.requiredRule.push(
        val =>
          /^[a-zA-Z0-9._-]+@[a-z0-9.-]+\.[a-z]{2,4}$/.test(
            String(val).toLowerCase()
          ) || "Невірний формат"
      );
    }

    // validate latitude
    if (this.type === "latitude") {
      this.requiredRule.push(
        val =>
          /^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$/g.test(String(val)) ||
          'Невірний формат'
      );
    }
    
    // validate longitude
    if (this.type === "longitude") {
      this.requiredRule.push(
        val =>
          /^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$/g.test(String(val)) ||
          'Невірний формат'
      );
    }

    // validate input type - name
    if (this.type === "name" && this.required) {
      this.requiredRule.push(
        val =>
          /(^[А-ЩЬЮЯҐЄІЇа-щьюяґєії]{1})+([А-ЩЬЮЯҐЄІЇа-щьюяґєії-\s']{0,70})/g.test(
            String(val)
          ) || "Значення має починатись з літери",
        val => (val && val.length >= 2) || "Введіть не менше 2-х символів"
      );
    }
    
    // validate input type - ipn
    if (this.type === "ipn" && this.required) {

      if (this.value && this.value.length > 0) {

        this.requiredRule.push(val => (val && val.length === 10) || "Значення має складатись із 10 цифр")
      }
    }

    // validate input type - passportAndNationalId
    if (this.type === "passportAndNationalId" && this.required) {
      this.requiredRule.push(
        val =>
          /^([0-9]{9,10}$|[А-ЯЁЇIЄҐ]{2}[0-9]{6}$)/.test(val) ||
          "Невірний формат документу"
      );
    }

    // validate input type - code
    if (this.type === "code" && this.required) {
      this.requiredRule.push(
        val =>
          (val && val.length >= 8 && val.length <= 10) ||
          "Значення має складатись із 8-10 цифер"
      );
    }

    // validate input type - secret code
    if (this.type === "secretCode" && this.required) {
      this.requiredRule.push(
        val =>
          (val && val.length >= 6 && val.length <= 20) ||
          "Значення має складатись із 6-20 символів"
      );
    }

    // validate input type - unzr
    if (this.type === "unzr" && this.required) {
      // this.requiredRule.push(
      //   val =>
      //     (val && val.length >= 6 && val.length <= 20) ||
      //     "Значення має складатись із 6-20 символів"
      // );
    }

    // validate input type - number
    if (this.type === "number" && this.required) {
      this.requiredRule.push(val => val >= 0 || "Не може бути від'ємним");
    }
  }
};
</script>

