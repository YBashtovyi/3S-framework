export default [
  {
    path: '/enum',
    component: () => import('@/pages/enum/index.vue'),
    children: [
      {
        path: '',
        name: 'records',
        meta: {
          title: 'Переліки',
          breadcrumb: [
            {
              name: 'Переліки',
            },
          ],
        },
        component: () => import('../pages/enum/subpages/List.vue'),
      },
      {
        path: 'create',
        title: 'Створення Переліки',
        meta: {
          breadcrumb: [
            {
              name: 'Переліки',
              path: '/enum',
            },
            {
              name: 'Створення Переліки',
            },
          ],
        },
        component: () => import('../pages/enum/subpages/Edit.vue'),
      },
      {
        path: 'details/:id',
        title: 'Перегляд Переліки',
        meta: {
          breadcrumb: [
            {
              name: 'Переліки',
              path: '/enum',
            },
            {
              name: 'Перегляд Переліки',
            },
          ],
        },
        component: () => import('../pages/enum/subpages/Details.vue'),
      },
    ],
  },
]
