import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-transaction-modal',
  imports: [],
  templateUrl: './transaction-modal.html',
  styleUrl: './transaction-modal.scss'
})
export class TransactionModal {
  @Output() close = new EventEmitter<void>();

  closeModal() {
    this.close.emit();
  }
}