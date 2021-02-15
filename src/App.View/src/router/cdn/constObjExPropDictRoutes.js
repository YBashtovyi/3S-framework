export default [
  {
    path: '/constObjExPropDict',
    component: () => import('../../pages/constObjExPropDict'),
    children: [
      {
        name: 'constObjExPropDictList',
        path: '',
        meta: {
          title: `Додаткові характеристики об'єктів`,
          breadcrumb: [
            {
              name: `Додаткові характеристики об'єктів`,
            },
          ],
        },
        component: () => import('../../pages/constObjExPropDict/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        title: 'Перегляд характеристики',
        meta: {
          breadcrumb: [
            {
              name: `Додаткові характеристики об'єктів`,
              path: '/constObjExPropDict',
            },
            {
              name: 'Перегляд характеристики',
            },
          ],
        },
        component: () => import('../../pages/constObjExPropDict/subpages/Details.vue'),
      },
      {
        path: 'create',
        title: 'Створення характеристики',
        meta: {
          breadcrumb: [
            {
              name: `Додаткові характеристики об'єктів`,
              path: '/constObjExPropDict',
            },
            {
              name: 'Створення характеристики',
            },
          ],
        },
        component: () => import('../../pages/constObjExPropDict/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування характеристики',
        meta: {
          breadcrumb: [
            {
              name: `Додаткові характеристики об'єктів`,
              path: '/constObjExPropDict',
            },
            {
              name: 'Редагування характеристики',
            },
          ],
        },
        component: () => import('../../pages/constObjExPropDict/subpages/Edit.vue'),
      },
    ],
  },
]
