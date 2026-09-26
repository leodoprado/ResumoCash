import {
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

import {
  Transaction,
  CreateTransactionRequest,
  UpdateTransactionRequest
} from '../../models/transaction.model';

@Component({
  selector: 'app-transaction-modal',
  imports: [],
  templateUrl: './transaction-modal.html',
  styleUrl: './transaction-modal.scss'
})
export class TransactionModal {

  @Input()
  transaction: Transaction | null = null;

  @Output()
  close = new EventEmitter<void>();

  @Output()
  create = new EventEmitter<CreateTransactionRequest>();

  @Output()
  update = new EventEmitter<UpdateTransactionRequest>();

  closeModal(): void {
    this.close.emit();
  }
}