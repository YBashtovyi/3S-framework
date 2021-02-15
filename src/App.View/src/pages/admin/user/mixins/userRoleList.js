import {
  getUserRoles,
  addRolesToUser,
  deleteRoleFromUser,
} from '../../../../services/adm-api/user-api'

export default {
  props: {
    userId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      userRoleList: [],
      columns: [
        {
          name: 'name',
          label: 'Назва',
          field: 'name',
          align: 'left',
          required: true,
        },
        {
          name: 'description',
          label: 'Опис',
          field: 'description',
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
      isVisibleUserRoleEditDialog: false,
    }
  },

  methods: {
    // #region Load data

    getRoles() {
      return getUserRoles(this.userId).then(this.setRoles)
    },

    setRoles(roles) {
      this.userRoleList = roles
    },

    // #endregion

    // #region Actions

    onOpenAddRoleDialog() {
      this.isVisibleUserRoleEditDialog = true
    },

    onDeleteRoleFromUser(row) {
      deleteRoleFromUser(this.userId, row.id)
        .then(this.onDeleteRoleSuccess)
        .then(this.getRoles)
        .catch(this.onDeleteRoleError)
    },

    onAddRoleToUser(roles) {
      const roleIds = roles.map(p => p.id)
      if (this.checkIfExistsRole(roleIds)) {
        this.roleIsExistsError()
        return
      }

      addRolesToUser(this.userId, roleIds)
        .then(this.onAddRoleSuccess)
        .then(this.getRoles)
        .catch(this.onAddRoleError)
    },

    // #endregion

    // #region Notify

    onAddRoleSuccess() {
      this.$q.notify({ icon: 'check', color: 'positive', message: 'Ролі успішно додані' })
    },

    onAddRoleError() {
      this.$q.notify({ color: 'negative', message: 'Помилка при додаванні ролі' })
    },

    onDeleteRoleSuccess() {
      this.$q.notify({ icon: 'check', color: 'positive', message: 'Роль успішно видалена' })
    },

    onDeleteRoleError() {
      this.$q.notify({ color: 'negative', message: 'Помилка під час видалення ролі' })
    },

    roleIsExistsError() {
      this.$q.notify({ color: 'negative', message: 'Користувач вже має цю роль' })
    },

    // #endregion

    // #region Checks

    checkIfExistsRole(roleIds) {
      if (!Array.isArray(this.userRoleList) || !this.userRoleList.length) return false

      return this.userRoleList.map(p => p.id).some(p => roleIds.includes(p))
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
          fn: row => this.onDeleteRoleFromUser(row),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.getRoles()
  },
}
