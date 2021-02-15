import isEqual from "lodash.isequal";
import cloneDeep from "lodash.clonedeep";

export default {
  data() {
    return {
      savedData: {},
      changedData: {},
      userChooseOption: false,
      submitClicked: false
    };
  },
  methods: {
    setData(data) {
      this.savedData = cloneDeep(data);
      this.changedData = data;
    },

    setSavedData(data) {
      this.savedData = data
    },

    setChangedData(data) {
      this.changedData = data
    }
  },
  computed: {
    isChangedData() {
      return isEqual(this.changedData, this.savedData);
    }
  },
  beforeRouteLeave(to, from, next) {
    if (!this.submitClicked) {
      if (!this.isChangedData) {
        this.$q
          .dialog({
            title: "Ви покидаєте сторінку",
            message: "На ній присутні незбережені дані. Зберегти?",
            persistent: true,
            ok: {
              flat: true,
              label: "Так",
              color: "green-7"
            },
            cancel: {
              flat: true,
              label: "Ні",
              color: "negative"
            }
          })
          .onOk(() => {
            if (this.step) {
              this.validateForm();
            } else {
              this.onSubmit();
            }
            next(false);
          })
          .onCancel(() => {
            next();
          })
          .onDismiss(() => {
            next(false);
          });
        this.userChooseOption = true;
      } else {
        next();
      }
    } else {
      next();
    }
  }
};
