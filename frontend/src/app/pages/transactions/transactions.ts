import { Component, OnInit, signal, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { finalize } from 'rxjs';

import { LucidePencil, LucideTrash } from '@lucide/angular';

import { Loading } from '../../components/loading/loading';

import { TransactionService } from '../../services/transaction.service';
import { ReferenceMonthService } from '../../services/reference-month.service';

import {
  Transaction,
  CreateTransactionRequest,
  UpdateTransactionRequest,
} from '../../models/transaction.model';

import { TransactionModal } from '../../components/transaction-modal/transaction-modal';
import { ConfirmDialog } from '../../components/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-transactions',
  imports: [
    RouterLink,
    TransactionModal,
    ConfirmDialog,
    LucidePencil,
    LucideTrash,
    CurrencyPipe,
    DatePipe,
    Loading,
  ],
  templateUrl: './transactions.html',
  styleUrl: './transactions.scss',
})
export class Transactions implements OnInit {
  showForm = false;

  selectedTransaction: Transaction | null = null;

  transactions = signal<Transaction[]>([]);
  transactionToDelete = signal<Transaction | null>(null);

  isLoading = signal(true);

  pendingTransactions = computed(() =>
    this.transactions().filter(
      (transaction) =>
        transaction.processStatus === 'Pending' &&
        this.isReferenceMonth(transaction.competenceMonth),
    ),
  );

  completedTransactions = computed(() =>
    this.transactions().filter(
      (transaction) =>
        transaction.processStatus === 'Completed' &&
        this.isReferenceMonth(transaction.competenceMonth),
    ),
  );

  constructor(
    private readonly transactionService: TransactionService,
    public readonly referenceMonthService: ReferenceMonthService,
  ) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  private isReferenceMonth(competenceMonth: string): boolean {
    const [year, month] = competenceMonth.split('-').map(Number);

    const currentYear = new Date().getFullYear();
    const referenceMonth = this.referenceMonthService.referenceMonth();

    return year === currentYear && month === referenceMonth;
  }

  loadTransactions(): void {
    this.isLoading.set(true);

    this.transactionService
      .getAll()
      .pipe(
        finalize(() => {
          this.isLoading.set(false);
        }),
      )
      .subscribe({
        next: (response) => {
          console.log('TRANSAÇÕES:', response);

          response.forEach((transaction) => {
            console.log({
              description: transaction.description,
              processStatus: transaction.processStatus,
              competenceMonth: transaction.competenceMonth,
            });
          });

          this.transactions.set(response);
        },

        error: (error) => {
          console.error('Erro ao carregar transações:', error);
        },
      });
  }

  createTransaction(request: CreateTransactionRequest): void {
    this.transactionService.create(request).subscribe({
      next: () => {
        this.closeForm();
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao cadastrar transação:', error);
      },
    });
  }

  updateTransaction(request: UpdateTransactionRequest): void {
    if (!this.selectedTransaction) {
      return;
    }

    this.transactionService.update(this.selectedTransaction.id, request).subscribe({
      next: () => {
        this.closeForm();
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao atualizar transação:', error);
      },
    });
  }

  completedTransaction(transaction: Transaction): void {
    this.transactionService.complete(transaction.id).subscribe({
      next: () => {
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao concluir transação:', error);
      },
    });
  }

  reopenTransaction(transaction: Transaction): void {
    this.transactionService.reopen(transaction.id).subscribe({
      next: () => {
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao reabrir transação:', error);
      },
    });
  }

  requestDelete(transaction: Transaction): void {
    this.transactionToDelete.set(transaction);
  }

  cancelDelete(): void {
    this.transactionToDelete.set(null);
  }

  confirmDelete(): void {
    const transaction = this.transactionToDelete();

    if (!transaction) {
      return;
    }

    this.transactionService.delete(transaction.id).subscribe({
      next: () => {
        this.transactionToDelete.set(null);
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao excluir transação:', error);
      },
    });
  }

  openForm(): void {
    this.selectedTransaction = null;
    this.showForm = true;
  }

  openEditForm(transaction: Transaction): void {
    this.selectedTransaction = transaction;
    this.showForm = true;
  }

  closeForm(): void {
    this.selectedTransaction = null;
    this.showForm = false;
  }

  changeReferenceMonth(event: Event): void {
    const select = event.target as HTMLSelectElement;

    this.referenceMonthService.setMonth(Number(select.value));
  }
}
