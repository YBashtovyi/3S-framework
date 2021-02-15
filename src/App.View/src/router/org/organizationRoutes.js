export default [
  {
    path: '/organization',
    component: () => import('../../pages/organization/'),
    children: [
      {
        name: 'OrganizationList',
        path: '',
        meta: {
          title: 'Організації',
          breadcrumb: [
            {
              name: 'Організації',
            },
          ],
        },
        component: () => import('../../pages/organization/subpages/List.vue'),
      },
      {
        path: 'details/:id',
        redirect: 'details/:id/info',
        title: 'Перегляд організації',
        meta: {
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Перегляд організації',
            },
          ],
        },
        component: () => import('../../pages/organization/subpages/Details.vue'),
        children: [
          {
            path: 'info',
            meta: {
              title: 'Організація',
              breadcrumb: [
                {
                  name: 'Організації',
                  path: '/organization',
                },
                {
                  name: 'Інформація про організацію',
                },
              ],
            },
            component: () => import('../../pages/organization/tabs/DetailCard.vue'),
          },
          {
            name: 'orgUnitStaffList',
            path: 'orgUnitStaff',
            meta: {
              title: 'Співробітники',
              breadcrumb: [
                {
                  name: 'Організації',
                  path: '/organization',
                },
                {
                  name: 'Співробітники',
                },
              ],
            },
            component: () => import('../../pages/organization/tabs/orgUnitStaff/subpages/List.vue'),
          },
          {
            path: 'file',
            title: 'Вміст',
            meta: {
              breadcrumb: [
                {
                  name: 'Організації',
                  path: '/organization',
                },
                {
                  name: 'Вміст',
                },
              ],
            },
            component: () => import('../../pages/organization/tabs/orgFile'),
          },
        ],
      },
      {
        path: 'create',
        title: 'Створення організації',
        meta: {
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Створення організації',
            },
          ],
        },
        component: () => import('../../pages/organization/subpages/Edit.vue'),
      },
      {
        path: 'edit/:id',
        title: 'Редагування організації',
        meta: {
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Редагування організації',
            },
          ],
        },
        component: () => import('../../pages/organization/subpages/Edit.vue'),
      },
      {
        path: ':id/orgUnitStaff/create',
        meta: {
          title: 'Посада',
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Співробітники',
              path: '/organization/details/:id/orgUnitStaff',
            },
            {
              name: 'Створення співробітника',
            },
          ],
        },
        component: () => import('../../pages/organization/tabs/orgUnitStaff/subpages/Edit.vue'),
      },
      {
        path: 'details/:id/orgUnitStaff/edit/:orgUnitStaffId',
        meta: {
          title: 'Посада',
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Співробітники',
              path: '/organization/details/:id/orgUnitStaff',
            },
            {
              name: 'Редагування співробітника',
            },
          ],
        },
        component: () => import('../../pages/organization/tabs/orgUnitStaff/subpages/Edit.vue'),
      },
      {
        path: 'details/:id/orgUnitStaff/details/:orgUnitStaffId',
        meta: {
          title: 'Посада',
          breadcrumb: [
            {
              name: 'Організації',
              path: '/organization',
            },
            {
              name: 'Співробітники',
              path: '/organization/details/:id/orgUnitStaff',
            },
            {
              name: 'Інформація про співробітника',
            },
          ],
        },
        component: () => import('../../pages/organization/tabs/orgUnitStaff/subpages/Details.vue'),
      },
    ],
  },
]
