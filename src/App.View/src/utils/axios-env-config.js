const defaultHost = process.env.API
let currentHost = location.origin + '/api'
if (currentHost.indexOf('localhost') > 0) {
  currentHost = 'http://localhost:8086' // back end medinet dev
}

module.exports = {
  BASE_API: {
    defaultHost,
  },

  AUTH: {
    AUTH_ID_GOV_UA: `${defaultHost}/Auth/authenticate-id-gov-ua`,
    ID_GOV_UA_CODE: `${defaultHost}/Auth/id-gov-ua-code`,
    AUTH_IDENTITY: defaultHost + '/Auth/authenticate',
    AUTH_TRY_IDENTITY: defaultHost + '/Auth/try-authenticate',
    LOG_OUT: defaultHost + '/Auth/logout',
    IDENTITY_USERS: `${defaultHost}/Auth/getIdentityUsers`,
    INFO: defaultHost + '/Auth/info',
  },

  ACCOUNT: {
    CHANGE_CREDENTIALS: defaultHost + '/Account/change-credentials',
  },

  USER: {
    PATH: defaultHost + '/user/',
    EXT: defaultHost + '/user/ext',
    GET_ROLES: `${defaultHost}/user/get-user-roles`,
    ADD_ROLES: `${defaultHost}/user/add-role-to-user`,
    DELETE_ROLE: `${defaultHost}/user/delete-role-from-user`,
    GET_RLS: `${defaultHost}/user/get-user-rls`,
    ADD_RLS: `${defaultHost}/user/add-rls`,
    EDIT_RLS: `${defaultHost}/user/edit-rls`,
    DELETE_RLS: `${defaultHost}/user/delete-rls`,
  },

  ROLE: {
    PATH: `${defaultHost}/role`,
    EDIT: `${defaultHost}/role/edit-page`,
    EXT: `${defaultHost}/role/ext`,
    RIGHT: {
      ROLE_RIGHT_PATH: `${defaultHost}/role/role-right`,
    },
    RLS: {
      GET_RLS: `${defaultHost}/role/get-user-rls`,
      ADD_RLS: `${defaultHost}/role/add-rls`,
      EDIT_RLS: `${defaultHost}/role/edit-rls`,
      DELETE_RLS: `${defaultHost}/role/delete-rls`,
    },
  },

  RIGHT: {
    PATH: `${defaultHost}/right`,
    EDIT: `${defaultHost}/right/edit-page`,
    EXT: `${defaultHost}/right/ext`,
  },

  PROFILES: {
    PATH: defaultHost + '/Profiles',
  },

  COMMON: {
    DATA: defaultHost + '/data/collect',
    ENUM: defaultHost + '/data/enum',
    ENUMS: defaultHost + '/data/enums',
  },

  EMPLOYEES: {
    PATH: `${defaultHost}/Employees`,
    LIST_WITH_USER_ID: `${defaultHost}/Employees/select-with-user-id`,
  },

  NOTIFICATION: {
    BY_AUTHOR: `${defaultHost}/Notification/items-by-author`,
    BY_RECEIVER: `${defaultHost}/Notification/items-by-receiver`,
    PATH: `${defaultHost}/Notification`,
    EXT: `${defaultHost}/Notification/ext`,
    EDIT_PAGE_DATA: `${defaultHost}/Notification/edit-page-data`,
    ONE_SIGNAL: {
      CREATE: `${defaultHost}/Notification/create-in-oneSignal`,
    },
  },

  ENUMRECORD: {
    PATH: defaultHost + '/EnumRecord',
  },

  FILESTORE: {
    PATH: defaultHost + '/FileStore',
    FORM_UPLOAD: defaultHost + '/FileStore/form-upload',
    UPLOAD: defaultHost + '/FileStore/upload',
    DOWNLOAD: defaultHost + '/FileStore/download',
  },

  POSITION: {
    PATH: `${defaultHost}/Position`,
    EXT: `${defaultHost}/Position/ext`,
  },

  ORGANIZATION: {
    PATH: defaultHost + '/Organization',
    EXT: defaultHost + '/Organization/ext',
    EDIT: defaultHost + '/Organization/edit-page',
    EXTENDED_PROPERTY: {
      PATH: `${defaultHost}/Organization/get-extended-property`,
      GET_BY_ID: `${defaultHost}/Organization/get-extended-property-by-id`,
      ADD: `${defaultHost}/Organization/add-extended-property`,
      EDIT: `${defaultHost}/Organization/edit-extended-property`,
      DELETE: `${defaultHost}/Organization/delete-extended-property`,
    },
  },

  DEPARTMENT: {
    PATH: defaultHost + '/Department',
    EXT: defaultHost + '/Department/ext',
    EDIT: defaultHost + '/Department/edit-page',
  },

  ORG_UNIT_POSITION: {
    PATH: `${defaultHost}/OrgUnitPosition`,
    EXT: `${defaultHost}/OrgUnitPosition/ext`,
    EDIT: `${defaultHost}/OrgUnitPosition/edit-page`,
  },

  ORG_UNIT_STAFF: {
    PATH: `${defaultHost}/OrgUnitStaff`,
    EXT: `${defaultHost}/OrgUnitStaff/ext`,
    EDIT: `${defaultHost}/OrgUnitStaff/edit-page`,
  },

  ORG_EMPLOYEE: {
    PATH: defaultHost + '/OrgEmployee',
    EXT: `${defaultHost}/OrgEmployee/ext`,
  },

  PERSON: {
    PATH: `${defaultHost}/Person`,
    EXT: `${defaultHost}/Person/ext`,
    EXTENDED_PROPERTY: {
      PATH: `${defaultHost}/Person/get-extended-property`,
      GET_BY_ID: `${defaultHost}/Person/get-extended-property-by-id`,
      ADD: `${defaultHost}/Person/add-extended-property`,
      EDIT: `${defaultHost}/Person/edit-extended-property`,
      DELETE: `${defaultHost}/Person/delete-extended-property`,
    },
  },

  ATU: {
    REGION: {
      ITEMS: `${defaultHost}/Atu/get-region-list`,
    },
  },

  CITY: {
    PATH: defaultHost + '/City',
    EDIT: defaultHost + '/City/edit-page',
  },

  CONSTRUCTION_OBJECT: {
    PATH: `${defaultHost}/ConstructionObject`,
    EXT: `${defaultHost}/ConstructionObject/ext`,
    EDIT: `${defaultHost}/ConstructionObject/edit-page`,
    CHANGE_OBJECT_STATUS: `${defaultHost}/ConstructionObject/change-object-status`,
    ATU_COORDINATES: {
      ADD: `${defaultHost}/ConstructionObject/add-atu-coordinate`,
      GET: `${defaultHost}/ConstructionObject/get-atu-coordinate`,
    },
    EXTENDED_PROPERTY: {
      GET: `${defaultHost}/ConstructionObject/get-extended-property`,
      ADD: `${defaultHost}/ConstructionObject/add-extended-property`,
      DELETE: `${defaultHost}/ConstructionObject/delete-extended-property`,
    },
    PROJECT_OBJECT_PATH: `${defaultHost}/ConstructionObject/get-project-construction-object`,
  },

  CONSTRUCTION_OBJECT_EX_PROPERTY_DICT: {
    PATH: `${defaultHost}/ConstructionObjectExPropertyDictionary`,
    EXT: `${defaultHost}/ConstructionObjectExPropertyDictionary/ext`,
    EDIT: `${defaultHost}/ConstructionObjectExPropertyDictionary/edit-page`,
    GET_TYPE_OF_OBJECT_LIST: `${defaultHost}/ConstructionObjectExPropertyDictionary/get-type-of-object-list`,
    EXPORT: {
      XLSX: 'ConstructionObjectExPropertyDictionary/export/xlsx',
    },
  },

  WORK_SUB_TYPE: {
    PATH: `${defaultHost}/WorkSubType`,
    EXT: `${defaultHost}/WorkSubType/ext`,
    EDIT: `${defaultHost}/WorkSubType/edit-page`,
    EXPORT: {
      XLSX: `${defaultHost}/WorkSubType/export/xlsx`,
    },
  },

  PROJECT: {
    PATH: `${defaultHost}/Project`,
    EXT: `${defaultHost}/Project/ext`,
    EDIT: `${defaultHost}/Project/edit-page`,
    TYPE_OF_PROJECT_WORK_PATH: `${defaultHost}/Project/get-type-of-project-work-list`,
    PARTICIPANT_EMPLOYEE_PATH: `${defaultHost}/Project/get-project-participant-employee-list`,
    ATU_COORDINATES: {
      ADD: `${defaultHost}/Project/add-atu-coordinate`,
      GET: `${defaultHost}/Project/get-atu-coordinate`,
    },
    EXPORT: {
      XLSX: 'Project/export/xlsx',
    },
  },

  PROJECT_WORK_SCHEDULE: {
    PATH: `${defaultHost}/ProjectWorkSchedule`,
    EXT: `${defaultHost}/ProjectWorkSchedule/ext`,
    EDIT: `${defaultHost}/ProjectWorkSchedule/edit-page`,
  },

  PROJECT_WORK_SCHEDULE_STAGE: {
    PATH: `${defaultHost}/ProjectWorkScheduleStage`,
    EXT: `${defaultHost}/ProjectWorkScheduleStage/ext`,
    EDIT: `${defaultHost}/ProjectWorkScheduleStage/edit-page`,
  },

  PROJECT_WORK_SCHEDULE_SUB_TYPE: {
    PATH: `${defaultHost}/ProjectWorkScheduleSubType`,
    EXT: `${defaultHost}/ProjectWorkScheduleSubType/ext`,
    EDIT: `${defaultHost}/ProjectWorkScheduleSubType/edit-page`,
  },

  PROJECT_CONTRACT: {
    PATH: `${defaultHost}/ProjectContract`,
    EXT: `${defaultHost}/ProjectContract/ext`,
    EDIT: `${defaultHost}/ProjectContract/edit-page`,
  },

  PROJECT_ADDITIONAL_AGREEMENT: {
    PATH: `${defaultHost}/ProjectAdditionalAgreement`,
    EXT: `${defaultHost}/ProjectAdditionalAgreement/ext`,
    EDIT: `${defaultHost}/ProjectAdditionalAgreement/edit-page`,
  },

  PROJECT_PHOTO_REPORT: {
    PATH: `${defaultHost}/ProjectPhotoReport`,
    EXT: `${defaultHost}/ProjectPhotoReport/ext`,
    EDIT: `${defaultHost}/ProjectPhotoReport/edit-page`,
    ATU_COORDINATES: {
      ADD: `${defaultHost}/ProjectPhotoReport/add-atu-coordinate`,
      GET: `${defaultHost}/ProjectPhotoReport/get-atu-coordinate`,
    },
  },

  PRJ_PARTICIPANT: {
    PATH: `${defaultHost}/ProjectParticipant`,
    EXT: `${defaultHost}/ProjectParticipant/ext`,
    EDIT: `${defaultHost}/ProjectParticipant/edit-page`,
  },

  DOCUMENT: {
    PATH: `${defaultHost}/Document`,
  },
}
