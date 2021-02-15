export default [
  {
    path: '/position',
    component: () => import('../pages/position/'),
    children: [
      {
        name: 'positionList',
        path: '',
        meta: {
          title: 'Посади',
          breadcrumb: [
            {
              name: 'Посади',
            },
          ],
        },
        component: () => import('../pages/position/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        title: 'Перегляд посади',
        meta: {
          breadcrumb: [
            {
              name: 'Посади',
              path: '/position',
            },
            {
              name: 'Перегляд посади',
            },
          ],
        },
        component: () => import('../pages/position/subpages/Details.vue'),
      },
      {
        path: 'create',
        title: 'Створення посади',
        meta: {
          breadcrumb: [
            {
              name: 'Посади',
              path: '/position',
            },
            {
              name: 'Створення посади',
            },
          ],
        },
        component: () => import('../pages/position/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування посади',
        meta: {
          breadcrumb: [
            {
              name: 'Посади',
              path: '/position',
            },
            {
              name: 'Редагування посади',
            },
          ],
        },
        component: () => import('../pages/position/subpages/Edit.vue'),
      },
    ],
  },
]
