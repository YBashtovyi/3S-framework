export default [{
    path: "/employees",
    children: [
      {
        path: "",
        name: "Doctors",
        meta: {
          title: "Реєстр співробітників"
        },
      },
      {
        path: "details/:id",
        redirect: "details/:id/info",
        meta: {
          title: "Перегляд картки співробітника",
          breadcrumb: [
            {
              name: "Реєстр співробітників",
              path: "/employees"
            },
            {
              name: "Перегляд картки співробітника"
            }
          ]
        },
        children: [
          {
            path: "info",
            meta: {
              title: "Картка співробітника",
              breadcrumb: [
                {
                  name: "Реєстр співробітників",
                  path: "/employees"
                },
                {
                  name: "Інформація про співробітника"
                }
              ]
            }
          },
        ]
      },
      {
        name: "createEmployee",
        path: "create",
        meta: {
          title: "Створення картки співробітника",
          breadcrumb: [
            {
              name: "Реєстр співробітників",
              path: "/employees"
            },
            {
              name: "Створення картки співробітника"
            }
          ]
        },
      },
      {
        path: "edit/:id",
        meta: {
          title: "Редагування картки співробітника",
          breadcrumb: [
            {
              name: "Реєстр співробітників",
              path: "/employees"
            },
            {
              name: "Редагування картки співробітника"
            }
          ]
        },
      },
      {
        path: "/employees/:misId/ehealth/details/:id",
        name: "detailEmployeeEhealth",
        meta: {
          breadcrumb: [
            {
              name: "Реєстр співробітників",
              path: "/employees"
            },
            {
              name: "Картка співробітника",
              path: "/employees/details/:misId/ehealth"
            }
          ]
        },
      },
      {
        name: "editEmployeeEhealth",
        path: "/employees/:misId/ehealth/edit/:id",
        meta: {
          breadcrumb: [
            {
              name: "Реєстр співробітників",
              path: "/employees"
            },
            {
              name: "Картка співробітника",
              path: "/employees/details/:misId/ehealth"
            },
          ]
        },
      },
    ]
  }
]