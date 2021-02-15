// if entityAccessLevel = 0, then access denied
// 1 - read
// 2 - partial
// 3 - write
// params:
//     userRights - user application rights
//     entityData - entities to check (almost in all cases these are from v-entity-right value, for example "Person" or "Person&Employee")
export const getEntityAccessLevel = (userRights, entityData) => {
  if (!userRights) {
    console.log(
      'Error when getting access level to entity. UserData or UserRights are undefined. Check that user is in vuex',
    )
    return 0
  }

  if (userRights.hasFullRights) {
    return 3
  }
  if (!userRights.entityRights) {
    return 0
  }

  if (typeof entityData !== 'string') {
    return 0
  }

  // enityName - can be a list of entities separated by | or &
  let mode = 'or'
  let entities
  if (entityData.indexOf('|') >= 0) {
    entities = entityData.split('|')
  } else {
    mode = 'and'
    entities = entityData.split('&')
  }

  let rights = userRights.entityRights
  let access
  for (let index = 0; index < entities.length; index++) {
    let entityName = entities[index]
    // iterate rights object to find proper right
    let entityAccessLevel = 0
    for (let right in rights) {
      if (rights.hasOwnProperty(right) && rights[right].entityName === entityName) {
        entityAccessLevel = rights[right].entityAccessLevel
        // if mode = 'and' we give access when all entities have access
        // if mode = 'or' we give access when any enity has access
        if (
          (entityAccessLevel === 0 && mode === 'and') ||
          (entityAccessLevel === 3 && mode === 'or')
        ) {
          return entityAccessLevel
        }
        // exit inner loop
        break
      }
    }

    // setting access first time
    if (access === undefined) {
      access = entityAccessLevel
      continue
    }
    // access level will be set to minimum access from the list
    if (mode === 'and') {
      access = Math.min(access, entityAccessLevel)
    }
    // access level will be set to maximum access from the list
    else {
      access = Math.max(access, entityAccessLevel)
    }
  }

  return access
}

// if entityAccessLevel = 0, then access denied
// 1 - visible
// 2 - clickable
// params:
//    userData - user instance from vuex or local storage
//    operationName - almost in all cases it comes from v-operation-right value, for example "SignDeclaration"
export const getOperationAccessLevel = (rights, operationName) => {
  if (rights) {
    if (rights.hasFullRights) {
      return 2
    }

    if (rights.operationRights && rights.operationRights.hasOwnProperty(operationName)) {
      return rights.operationRights[operationName]
    }
  }
  return 0
}

export const getInterfaceAccessLevel = (rights, interfaceRightName) => {
  if (!rights) {
    return false
  }

  if (rights.interfaceRights && rights.interfaceRights.includes(interfaceRightName)) {
    return true
  }

  return false
}
