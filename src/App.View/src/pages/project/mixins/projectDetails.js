import { getDetailsProjectById } from '../../../services/prj-api/project-api'
import { stringEmpty } from '../../../utils/function-helpers'
import get from 'lodash.get'

export default {
  data() {
    return {
      details: null,
      label: 'Проект',
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
      dateTimeFormat: 'DD.MM.YYYY HH:mm',
    }
  },

  methods: {
    fetchData() {
      getDetailsProjectById(this.projectId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/edit/${this.projectId}`, // TODO: Переделать в нормальный вид, потом еще в друнгих файлах
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    projectTitle() {
      return {
        label: `Проект - ${this.details.name}`,
        content: 'Основна інформація',
        icon: 'description',
        actions: [
          {
            action: this.onEditActionClick,
            icon: 'far fa-edit',
            iconSize: '18px',
            label: 'Редагувати',
          },
        ],
      }
    },

    typeOfProjectWorkCodeAndName() {
      return `${this.details.typeOfProjectWorkCode} ${this.details.typeOfProjectWorkName}`
    },
  },

  created() {
    this.fetchData()
  },
}
