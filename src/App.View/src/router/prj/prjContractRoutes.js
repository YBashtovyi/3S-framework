import prjAdditionalAgreementRoutes from './prjAdditionalAgreementRoutes'

export default [
  {
    path: 'details/:id/prjContract/create',
    meta: {
      title: 'Створення договору',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Деталі контрактів',
          path: '/projects/details/:id/prjContract',
        },
        {
          name: 'Створення договору',
        },
      ],
    },
    component: () => import('../../pages/project/tabs/prjContract/subpages/Edit.vue'),
  },
  {
    path: 'details/:id/prjContract/edit/:prjContractId',
    meta: {
      title: 'Редагування договору',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Деталі контрактів',
          path: '/projects/details/:id/prjContract',
        },
        {
          name: 'Редагування договору',
        },
      ],
    },
    component: () => import('../../pages/project/tabs/prjContract/subpages/Edit.vue'),
  },
  {
    path: 'details/:id/prjContract/details/:prjContractId',
    redirect: 'details/:id/prjContract/details/:prjContractId/info',
    component: () => import('../../pages/project/tabs/prjContract/subpages/Details.vue'),
    children: [
      {
        path: 'info',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Деталі контрактів',
              path: '/projects/details/:id/prjContract',
            },
            {
              name: 'Перегляд договору',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjContract/tabs/DetailCard.vue'),
      },
      {
        name: 'prjAdditionalAgreementList',
        path: 'prjAdditionalAgreement',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: 'projects',
            },
            {
              name: 'Деталі контрактів',
              path: '/projects/details/:id/prjContract',
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
              name: 'Проєкти',
              path: 'projects',
            },
            {
              name: 'Деталі контрактів',
              path: '/projects/details/:id/prjContract',
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
]
