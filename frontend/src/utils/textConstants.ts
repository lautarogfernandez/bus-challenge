export const API_ROUTES = {
  BUS: 'bus',
  DRIVER: 'driver',
  KID: 'kid',
};

export const ENTITIES = {
  BUS: 'Micro',
  DRIVER: 'Chofer',
  KID: 'Chico',
};

export const FORM_VALIDATION_MESSAGES = {
  required: 'Este campo es obligatorio',
  maxLength: (n: number) => `Máximo ${n} caracteres`,
};

export const ACTION_MESSAGES = {
  CREATED: 'creado',
  UPDATED: 'actualizado',
  DELETED: 'eliminado',
};
