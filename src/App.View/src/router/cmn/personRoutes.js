export default [
  {
    path: '/person',
    component: () => import('../../pages/person/'),
    children: [
      {
        name: 'personList',
        path: '',
        meta: {
          title: 'Персони',
          breadcrumb: [
            {
              name: 'Персони',
            },
          ],
        },
        component: () => import('../../pages/person/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        title: 'Перегляд персони',
        meta: {
          breadcrumb: [
            {
              name: 'Персони',
              path: '/person',
            },
            {
              name: 'Перегляд персони',
            },
          ],
        },
        component: () => import('../../pages/person/subpages/Details.vue'),
      },
      {
        path: 'create',
        title: 'Створення персони',
        meta: {
          breadcrumb: [
            {
              name: 'Персони',
              path: '/person',
            },
            {
              name: 'Створення персони',
            },
          ],
        },
        component: () => import('../../pages/person/subpages/Edit.vue'),
      },
      {
        path: 'edit/:personId',
        title: 'Редагування персони',
        meta: {
          breadcrumb: [
            {
              name: 'Персони',
              path: '/person',
            },
            {
              name: 'Редагування персони',
            },
          ],
        },
        component: () => import('../../pages/person/subpages/Edit.vue'),
      },
    ],
  },
]
