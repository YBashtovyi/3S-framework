export default {
  data() {
    return {
      activeValidate: false,
      step: 1
    };
  },
  methods: {
    // saveErrors(arr, event, name) {
    //   // this[arr] = [];
    //   console.log(event, name);
    //   this[arr].push(event);
    // },

    validateStep(step, arr, nextStep) {
      this[arr] = [];
      this.activeValidate = !this.activeValidate;
      setTimeout(() => {
        if (!this[step] && !this.cardId && nextStep) {
          this.step = nextStep;
        }
        if (!nextStep) {
          this.validateForm();
        }
      }, 300);
    },

    validateForm() {
      this.activeValidate = !this.activeValidate
        if (this.validForm) {
          this.onSubmit();
        } else {
          this.$q.notify({
            position: "top",
            timeout: 2000,
            message: "Перевірте, будь ласка, заповнені дані",
            color: "warning",
            icon: "warning",
            textColor: "grey-9"
          });
        }
    },
    testErrors(data, arr) {
      // for (let key in this[data]) {
      //   if (this[data][key]) {
      //     return true;
      //   }
      // }
      if (this.cardId) {
        for (let key in arr) {
          if (!arr[key]) {
            return true;
          }
        }
      } else {
        for (let key in this[data]) {
          if (this[data][key]) {
            return true;
          }
        }
      }
      return false;
    }
  },
  computed: {
    cardId() {
      return this.$route.params.id;
    }
  }
};
