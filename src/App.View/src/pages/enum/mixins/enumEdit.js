import { createEnum } from '../../../services/enum-api'

export default {
  data() {
    return {
      editData: {
        code: '',
        name: '',
        group: '',
        value: '',
        itemNumber: 0,
      },
    }
  },

  methods: {
    onSubmit() {
      createEnum(this.editData).then(data => this.goToDetails(data.id))
    },

    goToDetails(id) {
      this.$router.push('/enum/details/' + id)
    },
  },
}
