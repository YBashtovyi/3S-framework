export default [
  {
    path: '/objects',
    component: () => import('../../pages/constructionObject/'),
    children: [
      {
        name: 'objectList',
        path: '',
        meta: {
          title: `Об'єкти`,
          breadcrumb: [
            {
              name: `Об'єкти`,
            },
          ],
        },
        component: () => import('../../pages/constructionObject/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: `Перегляд об'єкту`,
        meta: {
          breadcrumb: [
            {
              name: `Об'єкти`,
              path: '/objects',
            },
            {
              name: `Перегляд об'єкту`,
            },
          ],
        },
        component: () => import('../../pages/constructionObject/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              breadcrumb: [
                {
                  name: `Об'єкти`,
                  path: '/objects',
                },
                {
                  name: `Перегляд об'єкту`,
                },
              ],
            },
            component: () => import('../../pages/constructionObject/tabs/DetailCard.vue'),
          },
          {
            name: 'objProjectList',
            path: 'projects',
            title: `Прооєкти`,
            meta: {
              breadcrumb: [
                {
                  name: `Об'єкти`,
                  path: '/objects',
                },
                {
                  name: `Проєкти`,
                },
              ],
            },
            component: () => import('../../pages/constructionObject/tabs/project/List.vue'),
          },
          {
            path: 'map',
            title: `Прооєкти`,
            meta: {
              breadcrumb: [
                {
                  name: `Об'єкти`,
                  path: '/objects',
                },
                {
                  name: `Мапа`,
                },
              ],
            },
            component: () => import('../../pages/constructionObject/tabs/Map.vue'),
          },
        ],
      },
      {
        path: 'create',
        title: `Створення об'єкту`,
        meta: {
          breadcrumb: [
            {
              name: `Об'єкти`,
              path: '/objects',
            },
            {
              name: `Створення об'єкту`,
            },
          ],
        },
        component: () => import('../../pages/constructionObject/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: `Редагування об'єкту`,
        meta: {
          breadcrumb: [
            {
              name: `Об'єкти`,
              path: '/objects',
            },
            {
              name: `Редагування об'єкту`,
            },
          ],
        },
        component: () => import('../../pages/constructionObject/subpages/Edit.vue'),
      },
    ],
  },
]
