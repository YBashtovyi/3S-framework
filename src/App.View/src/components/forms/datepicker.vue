<template>
  <q-input
    v-model="model"
    @input="onChanged"
    :bg-color="color"
    :disable="disabled"
    :filled="filled"
    :label="labelValue"
    :outlined="outlined"
    ref="valid"
    :rules="requiredRule"
    :readonly="readonly"
    dense
    mask="##.##.####"
    clearable
    clear-icon="close"
    @clear="clearField()"
  >
    <template v-slot:prepend v-if="icon">
      <q-icon name="event">
        <q-popup-proxy
          :breakpoint="1440"
          ref="qstartDateProxy"
          transition-show="scale"
          transition-hide="scale"
        >
          <q-date
            v-model="model"
            :options="setLimit"
            @input="
              () => {
                $refs.qstartDateProxy.hide(), onChanged()
              }
            "
            today-btn
            mask="DD.MM.YYYY"
            format="DD.MM.YYYY"
          >
            <div class="row items-center justify-end">
              <q-btn label="Закрити" color="primary" v-close-popup />
            </div>
          </q-date>
        </q-popup-proxy>
      </q-icon>
    </template>
  </q-input>
</template>
<script>
import moment from "moment"
// import timezone from "moment-timezone"
export default {
  data() {
    return {
      model: "",
      requiredRule: [],
      date: "",
      time: "",
      color: "transparent"
    }
  },
  props: {
    label: String,
    placeholder: String,
    outlined: {
      type: Boolean,
      default: false
    },
    icon: {
      type: Boolean,
      default: true
    },
    filled: Boolean,
    required: Boolean,
    limit: String,
    range: String,
    disabled: Boolean,
    value: String,
    lt: String,
    gt: String,
    validate: Boolean,
    readonly: {
      default: false,
      type: Boolean
    },
    isCleared: Boolean,
    extRules: {
      type: Array,
       default: function() {
        return []
      }
    }
  },
  computed: {
    hasError() {
      return this.$refs.valid.hasError
    },
    cardId() {
      return this.$route.params.id
    },
    // todayDate() {
    //   return moment()
    //     .locale("uk")
    //     .format("L")
    // },
    labelValue() {
      if (this.required && !this.readonly) {
        return this.label + " *"
      } else {
        return this.label
      }
    }
  },
  watch: {

    extRules () {

      this.setRules()

    },

    model() {

      // emit model if it changed
      if (this.model) {
        this.$emit(
          "updateData",
          moment.utc(this.model, ["DD.MM.YYYY"], "uk").format()
        )

        
      } else {
        this.$emit("updateData", "")
      }

    },
    value() {
      // set received value
      if (this.value) {
        this.model = moment(this.value)
          .locale("uk")
          .format("L")
        // this.date = timezone(this.value)
        //   .tz(timezone.tz.guess())
        //   .format("DD.MM.YYYY")
        // this.time = timezone(this.value)
        //   .tz(timezone.tz.guess())
        //   .format("HH:mm")
      } else {
        this.model = ""
      }
    },
    validate() {

      this.$refs.valid.validate()
      this.$emit("valid", this.hasError)

    },
    required() {

      this.setRules()

    }
  },
  mounted() {

    if (this.value) {
      this.model = moment(this.value)
        .locale("uk")
        .format("L")
    }
    this.setRequiredRules()
  },
  methods: {

    setRules() {

      this.requiredRule = []
      this.setRequiredRules()
      this.setExtRules()
      
      setTimeout(() => {
        this.$refs.valid.validate()
      }, 0)
    },

    setRequiredRules() {
      if (!this.required) {
        this.requiredRule.push(
          val =>
            !val ||
            /^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d$/.test(
              val
            ) ||
            "Невірний формат дати"
        )
      } else {
        this.requiredRule.push(
          val => !!val || "Обов'язкове поле",
          val =>
            /^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d$/.test(
              val
            ) || "Невірний формат дати"
        )
      }
    },

    setExtRules () {

      if (this.extRules.length > 0) {

        for (let i = 0;i < this.extRules.length; i++) {

          const extRule = this.extRules[i]
          this.requiredRule.push(extRule)

        }
      }
    },

    clearField() {
      this.$emit("isCleared", true)
    },
    onChanged() {
      if (this.cardId) {
        setTimeout(() => {
          this.$emit("valid", this.hasError)
          if (!this.hasError) {
            this.color = "green-1"
          } else {
            this.color = "red-1"
          }

          if (!this.required && !this.model) {
            this.color = "transparent"
          }
        }, 0)
      }
    },
    // range dates func
    setLimit(date) {
      if (this.lt && this.gt) {
        return (
          date <= moment(this.lt).format("YYYY/MM/DD") &&
          date >= moment(this.gt).format("YYYY/MM/DD")
        )
      }
      if (this.lt) {
        return date <= moment(this.lt).format("YYYY/MM/DD")
      }
      if (this.gt) {
        return date >= moment(this.gt).format("YYYY/MM/DD")
      }
      return true
    }
  }
}
</script>
