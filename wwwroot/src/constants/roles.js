const Roles = Object.freeze({
  USER: 1, //'Пользователь'
  ADMIN: 2, //'Администратор'
  SUPER_ADMIN: 3, //'Супер-администратор'
});

const ADMIN_LEVEL_ROLES = [Roles.ADMIN, Roles.SUPER_ADMIN];

export {Roles, ADMIN_LEVEL_ROLES};