export default [
  {
    path: '/accounts',
    component: () => import('../../pages/account'),
    children: [
      {
        name: 'accountList',
        path: '',
        meta: {
          title: 'Права',
          breadcrumb: [
            {
              name: 'Права',
            },
          ],
        },
        component: () => import('../../pages/account/subpages/List.vue'),
      },
    ],
  },
]
