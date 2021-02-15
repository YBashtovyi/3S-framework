export default [
  {
    path: '/workSubType',
    component: () => import('../../pages/constObjExPropDict'),
    children: [
      {
        name: 'workSubTypeList',
        path: '',
        meta: {
          title: `Види робіт`,
          breadcrumb: [
            {
              name: `Види робіт`,
            },
          ],
        },
        component: () => import('../../pages/workSubType/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        title: 'Перегляд виду робіт',
        meta: {
          breadcrumb: [
            {
              name: `Види робіт`,
              path: '/workSubType',
            },
            {
              name: 'Перегляд виду робіт',
            },
          ],
        },
        component: () => import('../../pages/workSubType/subpages/Details.vue'),
      },
      {
        path: 'create',
        title: 'Створення виду робіт',
        meta: {
          breadcrumb: [
            {
              name: `Види робіт`,
              path: '/workSubType',
            },
            {
              name: 'Створення виду робіт',
            },
          ],
        },
        component: () => import('../../pages/workSubType/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування виду робіт',
        meta: {
          breadcrumb: [
            {
              name: `Види робіт`,
              path: '/workSubType',
            },
            {
              name: 'Редагування виду робіт',
            },
          ],
        },
        component: () => import('../../pages/workSubType/subpages/Edit.vue'),
      },
    ],
  },
]
