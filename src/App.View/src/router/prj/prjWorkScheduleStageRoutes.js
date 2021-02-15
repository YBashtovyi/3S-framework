export default [
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/stage/create',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleStage/subpages/Edit.vue'
      ),
    meta: {
      title: 'Створення етапу',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Документи проєкту',
          path: '/projects/details/:id/prjWorkSchedule',
        },
        {
          name: 'Етапи календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/stage',
        },
        {
          name: 'Створення етапу',
        },
      ],
    },
  },
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/stage/edit/:prjDocStageId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleStage/subpages/Edit.vue'
      ),
    meta: {
      title: 'Редагування етапу',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Документи проєкту',
          path: '/projects/details/:id/prjWorkSchedule',
        },
        {
          name: 'Етапи календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/stage',
        },
        {
          name: 'Редагування етапу',
        },
      ],
    },
  },
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/stage/details/:prjDocStageId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleStage/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд етапу',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Документи проєкту',
          path: '/projects/details/:id/prjWorkSchedule',
        },
        {
          name: 'Етапи календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/stage',
        },
        {
          name: 'Перегляд етапу',
        },
      ],
    },
  },
]
