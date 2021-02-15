export default [
  {
    path: '/rights',
    component: () => import('../../pages/admin/right'),
    children: [
      {
        name: 'rightList',
        path: '',
        meta: {
          title: 'Права',
          breadcrumb: [
            {
              name: 'Права',
            },
          ],
        },
        component: () => import('../../pages/admin/right/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд права',
        meta: {
          breadcrumb: [
            {
              name: 'Права',
              path: '/rights',
            },
            {
              name: 'Перегляд права',
            },
          ],
        },
        component: () => import('../../pages/admin/right/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              breadcrumb: [
                {
                  name: 'Права',
                  path: '/rights',
                },
                {
                  name: 'Інформація про право',
                },
              ],
            },
            component: () => import('../../pages/admin/right/tabs/DetailCard.vue'),
          },
          //   {
          //     path: 'roleRls',
          //     meta: {
          //       breadcrumb: [
          //         {
          //           name: 'Права',
          //           path: '/rights',
          //         },
          //         {
          //           name: 'Налаштування доступу до даних',
          //         },
          //       ],
          //     },
          //     component: () => import('../../pages/admin/right/tabs/roleRls/RoleRls.vue'),
          //   },
        ],
      },
      {
        path: 'create',
        title: 'Створення права',
        meta: {
          breadcrumb: [
            {
              name: 'Права',
              path: '/rights',
            },
            {
              name: 'Створення права',
            },
          ],
        },
        component: () => import('../../pages/admin/right/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування права',
        meta: {
          breadcrumb: [
            {
              name: 'Права',
              path: '/rights',
            },
            {
              name: 'Редагування права',
            },
          ],
        },
        component: () => import('../../pages/admin/right/subpages/Edit.vue'),
      },
    ],
  },
]
