import prjWorkScheduleStageRoutes from './prjWorkScheduleStageRoutes'
import prjWorkScheduleSubTypeRoutes from './prjWorkScheduleSubTypeRoutes'

export default [
  {
    path: 'details/:id/prjWorkSchedule/create',
    component: () => import('../../pages/project/tabs/prjWorkSchedule'),
    children: [
      {
        name: 'WorkSchedule', // enum group - docType
        path: 'workSchedule',
        meta: {
          title: 'Календарний план',
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
              name: 'Створення календарного плану',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Edit.vue'),
      },
      {
        name: 'ChangesToWS', // enum group - docType
        path: 'changesToWS',
        meta: {
          title: 'Зміни календарного плану',
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
              name: 'Створення змін календарного плану',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Edit.vue'),
      },
    ],
  },
  {
    path: 'details/:id/prjWorkSchedule/edit/:prjDocId',
    component: () => import('../../pages/project/tabs/prjWorkSchedule'),
    children: [
      {
        name: 'WorkSchedule', // enum group - docType
        path: 'workSchedule',
        meta: {
          title: 'Календарний план',
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
              name: 'Редагування календарного плану',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Edit.vue'),
      },
      {
        name: 'ChangesToWS', // enum group - docType
        path: 'changesToWS',
        meta: {
          title: 'Зміни календарного плану',
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
              name: 'Редагування змін календарного плану',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Edit.vue'),
      },
    ],
  },
  {
    path: 'details/:id/prjWorkSchedule/details/:prjDocId',
    redirect: 'details/:id/prjWorkSchedule/details/:prjDocId/info',
    component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Details.vue'),
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
              name: 'Документи проєкту',
              path: '/projects/details/:id/prjWorkSchedule',
            },
            {
              name: 'Перегляд документу',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/tabs/DetailCard.vue'),
      },
      {
        name: 'stageList',
        path: 'stage',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: 'projects',
            },
            {
              name: 'Документи проєкту',
              path: '/projects/details/:id/prjWorkSchedule',
            },
            {
              name: 'Етапи календарного плану',
            },
          ],
        },
        component: () =>
          import(
            '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleStage/subpages/List.vue'
          ),
      },
      {
        name: 'subTypeList',
        path: 'subType',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: 'projects',
            },
            {
              name: 'Документи проєкту',
              path: '/projects/details/:id/prjWorkSchedule',
            },
            {
              name: 'Види робіт календарного плану',
            },
          ],
        },
        component: () =>
          import(
            '../../pages/project/tabs/prjWorkSchedule/tabs/prjWorkScheduleSubType/subpages/List.vue'
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
              name: 'Документи проєкту',
              path: '/projects/details/:id/prjWorkSchedule',
            },
            {
              name: 'Вміст',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjWorkSchedule/tabs/prjFile'),
      },
    ],
  },
  ...prjWorkScheduleStageRoutes,
  ...prjWorkScheduleSubTypeRoutes,
]
