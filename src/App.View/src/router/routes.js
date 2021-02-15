import cityRoutes from './atu/cityRoutes'
import employeeRoutes from './employeeRoutes'
import adminRoutes from './adminRoutes'
import enumRoutes from './enumRoutes'
import projectRoutes from './prj/projectRoutes'
import personRoutes from './cmn/personRoutes'
import organizationRoutes from './org/organizationRoutes'
import positionRoutes from './positionRoutes'
import userRoutes from './adm/userRoutes'
import roleRoutes from './adm/roleRoutes'
import rightRoutes from './adm/rightRoutes'
import constructionObjectRoutes from './cmn/constructionObjectRoutes'
import departmentRoutes from './org/departmentRoutes'
import accountRoutes from './adm/accountRoutes'
import prjContractRoutes from './prjContract/prjContractRoutes'
import prjWorkScheduleRoutes from './prjWorjSchedule/prjWorkScheduleRoutes'
import constObjExPropDictRoutes from './cdn/constObjExPropDictRoutes'
import workSubTypeRoutes from './cdn/workSubTypeRoutes'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/baseLayout/BaseLayout.vue'),
    redirect: '/dashboard',
    name: 'Homepage',
    children: [
      {
        path: '/dashboard',
        name: 'dashboard',
        component: () => import('@/pages/dashboard/dashboard.vue'),
      },
      ...cityRoutes,
      ...organizationRoutes,
      ...employeeRoutes,
      ...adminRoutes,
      ...userRoutes,
      ...roleRoutes,
      ...rightRoutes,
      ...accountRoutes,
      ...enumRoutes,
      ...positionRoutes,
      ...personRoutes,
      ...constructionObjectRoutes,
      ...projectRoutes,
      ...departmentRoutes,
      ...prjContractRoutes,
      ...prjWorkScheduleRoutes,
      ...constObjExPropDictRoutes,
      ...workSubTypeRoutes,
    ],
  },
  {
    path: '/',
    name: 'login',
    component: () => import('@/layouts/LoginPage.vue'),
    children: [
      {
        path: '/callback',
        name: 'callback',
        component: () => import('@/pages/auth/callback'),
      },
      {
        path: '/callbackIdGovUa',
        name: 'callbackIdGovUa',
        component: () => import('@/pages/auth/callbackIdGovUa'),
      },
      {
        path: '/accessdenied',
        name: 'accessdenied',
        component: () => import('@/pages/auth/accessDenied'),
      },
      {
        path: '/authSelect',
        name: 'authSelect',
        component: () => import('../pages/auth/authSelect'),
      },
    ],
  },
]

// Always leave this as last one
routes.push({
  path: '*',
  component: () => import('pages/Error404.vue'),
})

export default routes
