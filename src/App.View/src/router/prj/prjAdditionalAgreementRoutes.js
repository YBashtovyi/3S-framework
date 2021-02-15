export default [
  {
    path: 'details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement/create',
    component: () =>
      import('../../pages/project/tabs/prjContract/tabs/prjAdditionalAgreement/subpages/Edit.vue'),
    meta: {
      title: 'Створення додаткової угоди',
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
          name: 'Додаткові угоди',
          path: '/projects/details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement',
        },
        {
          name: 'Створення додаткової угоди',
        },
      ],
    },
  },
  {
    path:
      'details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement/edit/:prjAddAgreementId',
    component: () =>
      import('../../pages/project/tabs/prjContract/tabs/prjAdditionalAgreement/subpages/Edit.vue'),
    meta: {
      title: 'Редагування додаткової угоди',
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
          name: 'Додаткові угоди',
          path: '/projects/details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement',
        },
        {
          name: 'Редагування додаткової угоди',
        },
      ],
    },
  },
  {
    path:
      'details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement/details/:prjAddAgreementId',
    component: () =>
      import(
        '../../pages/project/tabs/prjContract/tabs/prjAdditionalAgreement/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд додаткової угоди',
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
          name: 'Додаткові угоди',
          path: '/projects/details/:id/prjContract/details/:prjContractId/prjAdditionalAgreement',
        },
        {
          name: 'Перегляд додаткової угоди',
        },
      ],
    },
  },
]
