const host = process.env['OIDC_AUTHORITY']
// const host =  "http://localhost:5000"

export const MIS_USERS = {
  GET_USERS: host + '/misUsers/get',
  UPDATE: host + '/misUsers/update',
}
