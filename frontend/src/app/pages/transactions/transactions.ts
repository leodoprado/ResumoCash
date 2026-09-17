import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TransactionModal } from '../../components/transaction-modal/transaction-modal';

@Component({
  selector: 'app-transactions',
  imports: [
    RouterLink,
    TransactionModal
  ],
  templateUrl: './transactions.html',
  styleUrl: './transactions.scss'
})
export class Transactions {
  showForm = false;

  openForm() {
    this.showForm = true;
  }

  closeForm() {
    this.showForm = false;
  }
}