export default [
  {
    path: 'admin',
    component: () => import('@/pages/admin/'),
    children: [
      {
        path: 'notifications',
        component: () => import('../pages/admin/notifications/Notifications'),
        children: [
          {
            path: 'by-authors',
            name: 'listByAuthor',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/ListByAuthor'),
          },
          {
            path: 'by-receivers',
            name: 'listByReceiver',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/ListByReceiver'),
          },
          {
            path: 'create',
            name: 'notificationCreate',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                  path: '/admin/notifications/by-authors',
                },
                {
                  name: 'Створення сповіщення',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/Edit'),
          },
          {
            path: 'details/:id',
            name: 'notificationDetails',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                  path: '/admin/notifications/by-authors',
                },
                {
                  name: 'Перегляд картки сповіщення',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/Details'),
          },
          {
            path: 'details/my/:id',
            name: 'notificationMyDetails',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                  path: '/admin/notifications/by-receivers',
                },
                {
                  name: 'Перегляд картки сповіщення',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/Details'),
          },
          {
            path: 'edit/:id',
            name: 'notificationEdit',
            meta: {
              breadcrumb: [
                {
                  name: 'Реєстр сповіщень',
                  path: '/admin/notifications/by-authors',
                },
                {
                  name: 'Перегляд сповіщення',
                  path: '/admin/notifications/details/:id',
                },
                {
                  name: 'Редагування сповіщення',
                },
              ],
            },
            component: () => import('../pages/admin/notifications/subpages/Edit'),
          },
        ],
      },
    ],
  },
]
