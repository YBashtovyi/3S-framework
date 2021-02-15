import {
  getRoleRight,
  createRoleRight,
  deleteRoleRight,
} from '../../../../services/adm-api/role-api'

export default {
  props: {
    roleId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      roleRightList: [],
      columns: [
        {
          name: 'code',
          label: 'Код',
          field: 'code',
          align: 'left',
          required: true,
        },
        {
          name: 'name',
          label: 'Назва',
          field: 'name',
          align: 'left',
          required: true,
        },
        {
          name: 'actions',
          label: '',
          align: 'left',
          field: '-',
        },
      ],

      selectedItem: {},

      isVisibleMenu: false,
      isVisibleRoleRightDialog: false,
    }
  },

  methods: {
    // #region Load data

    getRoleRight() {
      return getRoleRight(this.roleId).then(this.setRoleRight)
    },

    setRoleRight(roles) {
      this.roleRightList = roles
    },

    // #endregion

    // #region Actions

    onOpenAddRightDialog() {
      this.isVisibleRoleRightDialog = true
    },

    onDeleteRoleRight(row) {
      deleteRoleRight(row.id)
        .then(this.onDeleteRoleRightSuccess)
        .then(this.getRoleRight)
        .catch(this.onDeleteRoleRightError)
    },

    onAddRightToRole(rights) {
      const rightIds = rights.filter(p => p.selected).map(p => p.id)
      if (this.checkIfExistsRight(rightIds)) {
        this.roleRightIsExistsError()
        return
      }

      createRoleRight(this.roleId, rightIds)
        .then(this.onAddRoleRightSuccess)
        .then(this.getRoleRight)
        .catch(this.onAddRoleRightError)
    },

    // #endregion

    // #region Notify

    onAddRoleRightSuccess() {
      this.$q.notify({ icon: 'check', color: 'positive', message: 'Права успішно додані' })
    },

    onAddRoleRightError() {
      this.$q.notify({ color: 'negative', message: 'Помилка при додаванні права' })
    },

    onDeleteRoleRightSuccess() {
      this.$q.notify({ icon: 'check', color: 'positive', message: 'Право успішно видалено' })
    },

    onDeleteRoleRightError() {
      this.$q.notify({ color: 'negative', message: 'Помилка під час видалення права' })
    },

    roleRightIsExistsError() {
      this.$q.notify({ color: 'negative', message: 'Роль вже має це право' })
    },

    // #endregion

    // #region Checks

    checkIfExistsRight(rightIds) {
      if (!Array.isArray(this.roleRightList) || !this.roleRightList.length) return false

      return this.roleRightList.map(p => p.id).some(p => rightIds.includes(p))
    },

    // #endregion

    /**
     * Display row menu
     */
    onShowMenu(row) {
      this.isVisibleMenu = !this.isVisibleMenu
      this.selectedItem = row
    },
  },

  computed: {
    actions() {
      return [
        {
          type: 'delete',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.onDeleteRoleRight(row),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.getRoleRight()
  },
}
