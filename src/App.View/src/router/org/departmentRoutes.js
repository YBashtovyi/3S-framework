export default [
  {
    path: '/department',
    component: () => import('../../pages/department/'),
    children: [
      {
        name: 'departmentList',
        path: '',
        meta: {
          title: 'Підрозділи',
          breadcrumb: [
            {
              name: 'Підрозділи',
            },
          ],
        },
        component: () => import('../../pages/department/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд Підрозділу',
        meta: {
          breadcrumb: [
            {
              name: 'Підрозділи',
              path: '/department',
            },
            {
              name: 'Перегляд Підрозділу',
            },
          ],
        },
        component: () => import('../../pages/department/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              title: 'Підрозділ',
              breadcrumb: [
                {
                  name: 'Підрозділи',
                  path: '/department',
                },
                {
                  name: 'Інформація про Підрозділ',
                },
              ],
            },
            component: () => import('../../pages/department/tabs/DetailCard.vue'),
          },
        ],
      },
      {
        path: 'create',
        title: 'Створення Підрозділу',
        meta: {
          breadcrumb: [
            {
              name: 'Підрозділи',
              path: '/department',
            },
            {
              name: 'Створення Підрозділу',
            },
          ],
        },
        component: () => import('../../pages/department/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування Підрозділу',
        meta: {
          breadcrumb: [
            {
              name: 'Підрозділи',
              path: '/department',
            },
            {
              name: 'Редагування Підрозділу',
            },
          ],
        },
        component: () => import('../../pages/department/subpages/Edit.vue'),
      },
    ],
  },
]
