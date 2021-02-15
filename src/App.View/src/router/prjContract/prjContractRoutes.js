import prjAdditionalAgreementRoutes from './prjAdditionalAgreementRoutes'

export default [
  {
    path: '/prjContract',
    component: () => import('../../pages/prjContract'),
    children: [
      {
        name: 'prjContractList',
        path: '',
        component: () => import('../../pages/prjContract/subpages/List.vue'),
        meta: {
          title: 'Договори',
          breadcrumb: [
            {
              name: 'Договори',
            },
          ],
        },
      },
      {
        path: 'details/:prjContractId',
        redirect: 'details/:prjContractId/info',
        component: () => import('../../pages/project/tabs/prjContract/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            component: () => import('../../pages/project/tabs/prjContract/tabs/DetailCard.vue'),
            meta: {
              breadcrumb: [
                {
                  name: 'Договори',
                  path: '/prjContract',
                },
                {
                  name: 'Перегляд договору',
                },
              ],
            },
          },
          {
            name: 'prjAdditionalAgreementlIST',
            path: 'prjAdditionalAgreement',
            meta: {
              breadcrumb: [
                {
                  name: 'Договори',
                  path: '/prjContract',
                },
                {
                  name: 'Додаткові угоди',
                },
              ],
            },
            component: () =>
              import(
                '../../pages/project/tabs/prjContract/tabs/prjAdditionalAgreement/subpages/List.vue'
              ),
          },
          {
            path: 'file',
            title: 'Вміст',
            meta: {
              breadcrumb: [
                {
                  name: 'Договори',
                  path: '/prjContract',
                },
                {
                  name: 'Вміст',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjContract/tabs/prjFile'),
          },
        ],
      },
      ...prjAdditionalAgreementRoutes,
    ],
  },
]
