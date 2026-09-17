import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TransactionModal } from '../../components/transaction-modal/transaction-modal';

@Component({
  selector: 'app-dashboard',
  imports: [
    RouterLink,
    TransactionModal
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  showTransactionForm = false;

  openTransactionForm() {
    this.showTransactionForm = true;
  }

  closeTransactionForm() {
    this.showTransactionForm = false;
  }
}