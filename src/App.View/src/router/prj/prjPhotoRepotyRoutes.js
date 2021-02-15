export default [
  {
    path: 'details/:id/prjPhotoReport/create',
    meta: {
      title: 'Створення фотозвіту',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Фотозвіти',
          path: '/projects/details/:id/prjPhotoReport',
        },
        {
          name: 'Створення фотозвіту',
        },
      ],
    },
    component: () => import('../../pages/project/tabs/prjPhotoReport/subpages/Edit.vue'),
  },
  {
    path: 'details/:id/prjPhotoReport/edit/:prjPhotoReportId',
    meta: {
      title: 'Редагування фотозвіту',
      breadcrumb: [
        {
          name: 'Проєкти',
          path: '/projects',
        },
        {
          name: 'Фотозвіти',
          path: '/projects/details/:id/prjPhotoReport',
        },
        {
          name: 'Редагування фотозвіту',
        },
      ],
    },
    component: () => import('../../pages/project/tabs/prjPhotoReport/subpages/Edit.vue'),
  },
  {
    path: 'details/:id/prjPhotoReport/details/:prjPhotoReportId',
    redirect: 'details/:id/prjPhotoReport/details/:prjPhotoReportId/info',
    component: () => import('../../pages/project/tabs/prjPhotoReport/subpages/Details.vue'),
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
              name: 'Фотозвіти',
              path: '/projects/details/:id/prjPhotoReport',
            },
            {
              name: 'Перегляд договору',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjPhotoReport/tabs/DetailCard.vue'),
      },
      {
        path: 'map',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Фотозвіти',
              path: '/projects/details/:id/prjPhotoReport',
            },
            {
              name: 'Мапа',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjPhotoReport/tabs/Map.vue'),
      },
      {
        path: 'file',
        meta: {
          breadcrumb: [
            {
              name: 'Проєкти',
              path: '/projects',
            },
            {
              name: 'Фотозвіти',
              path: '/projects/details/:id/prjPhotoReport',
            },
            {
              name: 'Вміст',
            },
          ],
        },
        component: () => import('../../pages/project/tabs/prjPhotoReport/tabs/prjFile'),
      },
    ],
  },
]
