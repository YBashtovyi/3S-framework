import prjWorkScheduleStageRoutes from './prjWorkScheduleStageRoutes'
import prjWorkScheduleSubTypeRoutes from './prjWorkScheduleSubTypeRoutes'

export default [
  {
    path: '/prjWorkSchedule',
    component: () => import('../../pages/prjWorkSchedule'),
    children: [
      {
        name: 'prjWorkScheduleList',
        path: '',
        component: () => import('../../pages/prjWorkSchedule/subpages/List.vue'),
        meta: {
          title: 'Календарні плани',
          breadcrumb: [
            {
              name: 'Календарні плани',
            },
          ],
        },
      },
      {
        path: 'details/:prjDocId',
        redirect: 'details/:prjDocId/info',
        component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              breadcrumb: [
                {
                  name: 'Календарні плани',
                  path: '/prjWorkSchedule',
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
                  name: 'Календарні плани',
                  path: '/prjWorkSchedule',
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
            name: 'stageList',
            path: 'subType',
            meta: {
              breadcrumb: [
                {
                  name: 'Календарні плани',
                  path: '/prjWorkSchedule',
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
                  name: 'Календарні плани',
                  path: '/prjWorkSchedule',
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
      ...prjWorkScheduleSubTypeRoutes,
      ...prjWorkScheduleStageRoutes,
    ],
  },
]
