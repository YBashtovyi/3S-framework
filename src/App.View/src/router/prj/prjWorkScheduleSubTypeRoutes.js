export default [
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/subType/create',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleSubType/subpages/Edit.vue'
      ),
    meta: {
      title: 'Створення виду робіт',
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
          name: 'Види робіт календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/subType',
        },
        {
          name: 'Створення виду робіт',
        },
      ],
    },
  },
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/subType/edit/:prjDocSubTypeId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleSubType/subpages/Edit.vue'
      ),
    meta: {
      title: 'Редагування виду робіт',
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
          name: 'Види робіт календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/subType',
        },
        {
          name: 'Редагування виду робіт',
        },
      ],
    },
  },
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId/subType/details/:prjDocSubTypeId',
    component: () =>
      import(
        '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleSubType/subpages/Details.vue'
      ),
    meta: {
      title: 'Перегляд виду робіт',
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
          name: 'Види робіт календарного плану',
          path: '/projects/details/:id/prjWorkSchedule/details/:prjDocId/subType',
        },
        {
          name: 'Перегляд виду робіт',
        },
      ],
    },
  },
]
