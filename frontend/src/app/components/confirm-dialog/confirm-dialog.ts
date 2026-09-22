import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.html',
  styleUrl: './confirm-dialog.scss',
})
export class ConfirmDialog {
  title = input('Confirmar ação');
  message = input('Deseja realmente continuar?');

  confirmText = input('Confirmar');
  cancelText = input('Cancelar');

  confirmed = output<void>();
  cancelled = output<void>();
}