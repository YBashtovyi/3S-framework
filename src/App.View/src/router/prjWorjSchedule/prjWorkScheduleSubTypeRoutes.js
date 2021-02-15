export default [
  {
    path: 'details/:prjDocId/subType/details/:prjDocSubTypeId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleSubType/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд виду робіт',
      breadcrumb: [
        {
          name: 'Календарні плани',
          path: '/prjWorkSchedule',
        },
        {
          name: 'Етапи календарного плану',
          path: '/prjWorkSchedule/details/:prjDocId/stage',
        },
        {
          name: 'Види робіт календарного плану',
          path: '/prjWorkSchedule/details/:prjDocId/subType',
        },
        {
          name: 'Перегляд виду робіт',
        },
      ],
    },
  },
]
