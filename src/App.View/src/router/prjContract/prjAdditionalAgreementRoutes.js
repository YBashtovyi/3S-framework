export default [
  {
    path: 'details/:prjContractId/prjAdditionalAgreement/details/:prjAddAgreementId',
    component: () =>
      import(
        '../../pages/project/tabs/prjContract/tabs/prjAdditionalAgreement/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд додаткової угоди',
      breadcrumb: [
        {
          name: 'Договори',
          path: '/prjContract',
        },
        {
          name: 'Додаткові угоди',
          path: '/prjContract/details/:prjContractId/prjAdditionalAgreement',
        },
        {
          name: 'Перегляд додаткової угоди',
        },
      ],
    },
  },
]
