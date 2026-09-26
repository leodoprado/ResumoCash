import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { TransactionModal } from '../../components/transaction-modal/transaction-modal';
import { ReferenceMonthService } from '../../services/reference-month.service';

@Component({
  selector: 'app-dashboard',
  imports: [
    RouterLink,
    TransactionModal
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
  showTransactionForm = false;

  constructor(
    public readonly referenceMonthService: ReferenceMonthService,
  ) {}

  changeReferenceMonth(event: Event): void {
    const select = event.target as HTMLSelectElement;

    this.referenceMonthService.setMonth(
      Number(select.value)
    );
  }

  openTransactionForm(): void {
    this.showTransactionForm = true;
  }

  closeTransactionForm(): void {
    this.showTransactionForm = false;
  }
}