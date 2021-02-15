import prjContractRoutes from './prjContractRoutes'
import prjPhotoRepotyRoutes from './prjPhotoRepotyRoutes'
import prjWorkScheduleRoutes from './prjWorkScheduleRoutes'

export default [
  {
    path: '/projects',
    component: () => import('../../pages/project/'),
    children: [
      {
        name: 'projectList',
        path: '',
        meta: {
          title: 'Проєкти',
          breadcrumb: [
            {
              name: 'Проєкти',
            },
          ],
        },
        component: () => import('../../pages/project/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд проєкту',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Перегляд проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/subpages/Details.vue'),
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
                  name: 'Перегляд проєкту',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/DetailCard.vue'),
          },
          {
            name: 'prjParticipant',
            path: 'prjParticipant',
            title: 'Учасники проєкту',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Учасники проєкту',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjParticipant/subpages/List.vue'),
          },
          {
            name: 'prjProjectContractList',
            path: 'prjContract',
            title: 'Деталі контрактів',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Деталі контрактів',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjContract/subpages/List.vue'),
          },
          {
            name: 'prjProjectWorkScheduleList',
            path: 'prjWorkSchedule',
            title: 'Документи проєкту',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Документи проєкту',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjWorkSchedule/subpages/List.vue'),
          },
          {
            name: 'prjPhotoReportList',
            path: 'prjPhotoReport',
            title: 'Фотозвіти',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Фотозвіти',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjPhotoReport/subpages/List.vue'),
          },
          {
            path: 'map',
            title: 'Мапа',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Мапа',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/Map.vue'),
          },
          {
            path: 'file',
            title: 'Вміст',
            meta: {
              breadcrumb: [
                {
                  name: 'Проєкти',
                  path: '/projects',
                },
                {
                  name: 'Вміст',
                },
              ],
            },
            component: () => import('../../pages/project/tabs/prjFile'),
          },
        ],
      },
      {
        path: 'create',
        title: 'Створення проєкту',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Створення проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування проєкту',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Редагування проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/subpages/Edit.vue'),
      },
      {
        path: 'details/:id/prjParticipant/create',
        meta: {
          title: 'Учасник проєкту',
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Перелік учасників',
              path: '/projects/details/:id/prjParticipant',
            },
            {
              name: 'Створення учасника проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjParticipant/subpages/Edit.vue'),
      },
      {
        path: 'details/:id/prjParticipant/edit/:prjParticipantId',
        meta: {
          title: 'Учасник проєкту',
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Учасники проєкту',
              path: '/projects/details/:id/prjParticipant',
            },
            {
              name: 'Редагування учасника проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjParticipant/subpages/Edit.vue'),
      },
      {
        path: 'details/:id/prjParticipant/details/:prjParticipantId',
        meta: {
          title: 'Посада',
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Учасники проєкту',
              path: '/projects/details/:id/prjParticipant',
            },
            {
              name: 'Інформація про учасника проєкту',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjParticipant/subpages/Details.vue'),
      },
      ...prjWorkScheduleRoutes,
      ...prjContractRoutes,
      ...prjPhotoRepotyRoutes,
    ],
  },
]
