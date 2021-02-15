export default [
  {
    path: 'details/:prjDocId/stage/details/:prjDocStageId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleStage/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд етапу',
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
          name: 'Перегляд етапу',
        },
      ],
    },
  },
]
