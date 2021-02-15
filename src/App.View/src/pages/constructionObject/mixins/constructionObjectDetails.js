import {
  getDetailsObjectById,
  changeObjectStatus,
} from '../../../services/common-api/construction-object-api'
import get from 'lodash.get'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      details: null,
      label: `Об'єкт`,
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
    }
  },

  methods: {
    fetchData() {
      getDetailsObjectById(this.objectId)
        .then(this.setDetails)
        .finally(p => (this.pageReady = true))
    },

    setDetails(data) {
      this.details = data
    },

    onEditActionClick() {
      const routeParams = {
        path: `/objects/edit/${this.objectId}`, // TODO: Переделать в нормальный вид, потом еще в друнгих файлах
      }

      this.$router.push(routeParams)
    },

    onChangeStatus(status) {
      changeObjectStatus(this.objectId, status)
        .then(this.fetchData)
        .then(this.changeStatusSuccess)
    },

    changeStatusSuccess() {
      this.$q.notify({
        position: 'bottom',
        timeout: 5000,
        message: `Статус об'єкту успішно змінено`,
        color: 'positive',
        icon: 'check',
      })
    },
  },

  computed: {
    objectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    objectTitle() {
      return {
        label: `Об'єкт - ${this.details.name}`,
        content: 'Основна інформація',
        icon: 'description',
        actions: [
          {
            action: this.onEditActionClick,
            icon: 'far fa-edit',
            iconSize: '18px',
            label: 'Редагувати',
            disable: !this.hasEdit,
          },
          {
            action: () => this.onChangeStatus('Project'), // TODO: вынести в константу
            icon: 'fa fa-arrow-left',
            iconSize: '18px',
            label: 'Повернути до проєкту',
            disable: !this.hasChangeToProject,
          },
          {
            action: () => this.onChangeStatus('Active'), // TODO: вынести в константу
            icon: 'fa fa-arrow-right',
            iconSize: '18px',
            label: 'Ввести в дію',
            disable: !this.hasChangeToActive,
          },
          {
            action: () => this.onChangeStatus('NotActive'), // TODO: вынести в константу
            icon: 'fa fa-pause-circle',
            iconSize: '18px',
            label: 'Вивести з дії',
            disable: !this.hasChangeToNotActive,
          },
        ],
      }
    },

    hasChangeToProject() {
      return this.details.objectStatus === 'Active'
    },

    hasChangeToActive() {
      return this.details.objectStatus === 'Project'
    },

    hasChangeToNotActive() {
      return this.details.objectStatus === 'Active'
    },

    hasEdit() {
      return this.details.objectStatus !== 'NotActive'
    },
  },

  created() {
    this.fetchData()
  },
}
