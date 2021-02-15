export default [
  {
    path: '/users',
    component: () => import('../../pages/admin/user/'),
    children: [
      {
        name: 'userList',
        path: '',
        meta: {
          title: 'Користувачі системи',
          breadcrumb: [
            {
              name: 'Користувачі системи',
            },
          ],
        },
        component: () => import('../../pages/admin/user/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд користувача',
        meta: {
          breadcrumb: [
            {
              name: 'Користувачі системи',
              path: '/users',
            },
            {
              name: 'Перегляд користувача',
            },
          ],
        },
        component: () => import('../../pages/admin/user/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              breadcrumb: [
                {
                  name: 'Користувачі системи',
                  path: '/users',
                },
                {
                  name: 'Інформація про користувача',
                },
              ],
            },
            component: () => import('../../pages/admin/user/tabs/DetailCard.vue'),
          },
          {
            path: 'userRls',
            meta: {
              breadcrumb: [
                {
                  name: 'Користувачі системи',
                  path: '/users',
                },
                {
                  name: 'Налаштування доступу до даних',
                },
              ],
            },
            component: () => import('../../pages/admin/user/tabs/userRls/UserRls.vue'),
          },
        ],
      },
    ],
  },
]
