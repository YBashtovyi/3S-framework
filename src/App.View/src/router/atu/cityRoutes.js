export default [
  {
    path: '/city',
    component: () => import('../../pages/city/'),
    children: [
      {
        path: '',
        meta: {
          title: 'Населені пункти',
          breadcrumb: [
            {
              name: 'Населені пункти',
            },
          ],
        },
        component: () => import('../../pages/city/subpages/List.vue'),
      }
    ],
  },
]
