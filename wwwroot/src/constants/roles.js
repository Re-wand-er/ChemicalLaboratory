const Roles = Object.freeze({
  USER: 1, //'Пользователь'
  ADMIN: 2, //'Администратор'
  SUPER_ADMIN: 3, //'Супер-администратор'
});

const systemRoles = [
  { id: 1, name: 'Пользователь' },
  { id: 2, name: 'Администратор' },
  { id: 3, name: 'Супер-администратор' }
];

const ADMIN_LEVEL_ROLES = [Roles.ADMIN, Roles.SUPER_ADMIN];

export {Roles, systemRoles, ADMIN_LEVEL_ROLES};