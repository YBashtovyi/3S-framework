export const NONE = { name: 'Не назначено', shortName: 'N', code: 0 }
export const CREATE = { name: 'Створення', shortName: 'C', code: 1 }
export const READ = { name: 'Редагування', shortName: 'R', code: 2 }
export const UPDATE = { name: 'Оновлення', shortName: 'U', code: 4 }
export const DELETE = { name: 'Видалення', shortName: 'D', code: 8 }
export const ALL = { name: 'Повний доступ', shortName: 'A', code: 16 }
export const BAN = { name: 'Заборона на буль-яку дію', shortName: 'B', code: 32 }

export const CRUD_OPERATION = {
  NONE,
  CREATE,
  READ,
  UPDATE,
  DELETE,
  ALL,
  BAN,
}

export const CRUD_OPERATION_LIST = [NONE, CREATE, READ, UPDATE, DELETE, ALL, BAN]
