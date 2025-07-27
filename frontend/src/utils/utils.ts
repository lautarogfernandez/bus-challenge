import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

export function getDetailPageTitle(entity: string, isEdition: boolean): string {
  return `${isEdition ? 'Edición' : 'Creación'} de ${entity}`;
}

export function handleSuccessOnSave(
  snackBar: MatSnackBar,
  entity: string,
  action: string,
  router: Router,
  url: string
) {
  snackBar.open(`${entity} ${action} con éxito`, 'Cerrar', {
    duration: 3000,
  });

  router.navigate([url]);
}

export function handleErrorOnSave(
  snackBar: MatSnackBar,
  entity: string,
  action: string,
  err: any
) {
  snackBar.open(`Error al intentar ${action} el ${entity}`, 'Cerrar', {
    duration: 3000,
  });

  console.error(err);
}
