export default [
  {
    path: '/roles',
    component: () => import('../../pages/admin/role'),
    children: [
      {
        name: 'roleList',
        path: '',
        meta: {
          title: 'Ролі',
          breadcrumb: [
            {
              name: 'Ролі',
            },
          ],
        },
        component: () => import('../../pages/admin/role/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд ролі',
        meta: {
          breadcrumb: [
            {
              name: 'Ролі',
              path: '/roles',
            },
            {
              name: 'Перегляд ролі',
            },
          ],
        },
        component: () => import('../../pages/admin/role/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              breadcrumb: [
                {
                  name: 'Ролі',
                  path: '/roles',
                },
                {
                  name: 'Інформація про роль',
                },
              ],
            },
            component: () => import('../../pages/admin/role/tabs/DetailCard.vue'),
          },
          {
            path: 'roleRls',
            meta: {
              breadcrumb: [
                {
                  name: 'Ролі',
                  path: '/roles',
                },
                {
                  name: 'Налаштування доступу до даних',
                },
              ],
            },
            component: () => import('../../pages/admin/role/tabs/roleRls/RoleRls.vue'),
          },
        ],
      },
      {
        path: 'create',
        title: 'Створення ролі',
        meta: {
          breadcrumb: [
            {
              name: 'Ролі',
              path: '/roles',
            },
            {
              name: 'Створення ролі',
            },
          ],
        },
        component: () => import('../../pages/admin/role/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування ролі',
        meta: {
          breadcrumb: [
            {
              name: 'Ролі',
              path: '/roles',
            },
            {
              name: 'Редагування ролі',
            },
          ],
        },
        component: () => import('../../pages/admin/role/subpages/Edit.vue'),
      },
    ],
  },
]
