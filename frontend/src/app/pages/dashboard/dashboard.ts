import { Component, OnInit, computed, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { finalize } from 'rxjs';

import { TransactionModal } from '../../components/transaction-modal/transaction-modal';
import { Loading } from '../../components/loading/loading';

import { ReferenceMonthService } from '../../services/reference-month.service';
import { TransactionService } from '../../services/transaction.service';

import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration } from 'chart.js';

import { Transaction } from '../../models/transaction.model';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, TransactionModal, CurrencyPipe, Loading, BaseChartDirective],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  showTransactionForm = false;

  transactions = signal<Transaction[]>([]);
  isLoading = signal(true);

  referenceMonthTransactions = computed(() =>
    this.transactions().filter((transaction) => {
      const [year, month] = transaction.competenceMonth.split('-').map(Number);

      return (
        year === this.referenceMonthService.referenceYear() &&
        month === this.referenceMonthService.referenceMonth()
      );
    }),
  );

  totalIncome = computed(() =>
    this.referenceMonthTransactions()
      .filter(
        (transaction) =>
          transaction.categoryType === 'Income' && transaction.processStatus === 'Completed',
      )
      .reduce((total, transaction) => total + transaction.amount, 0),
  );

  totalExpense = computed(() =>
    this.referenceMonthTransactions()
      .filter(
        (transaction) =>
          transaction.categoryType === 'Expense' && transaction.processStatus === 'Completed',
      )
      .reduce((total, transaction) => total + transaction.amount, 0),
  );

  monthBalance = computed(() => this.totalIncome() - this.totalExpense());

  monthlyChartData = computed<ChartConfiguration<'bar'>['data']>(() => {
    const year = this.referenceMonthService.referenceYear();

    const months = [
      'Jan',
      'Fev',
      'Mar',
      'Abr',
      'Mai',
      'Jun',
      'Jul',
      'Ago',
      'Set',
      'Out',
      'Nov',
      'Dez',
    ];

    const income = Array<number>(12).fill(0);
    const expense = Array<number>(12).fill(0);

    this.transactions()
      .filter((transaction) => {
        const [transactionYear] = transaction.competenceMonth.split('-').map(Number);

        return transactionYear === year && transaction.processStatus === 'Completed';
      })
      .forEach((transaction) => {
        const [, month] = transaction.competenceMonth.split('-').map(Number);

        const monthIndex = month - 1;
        const amount = Number(transaction.amount);

        if (transaction.categoryType === 'Income') {
          income[monthIndex] += amount;
        }

        if (transaction.categoryType === 'Expense') {
          expense[monthIndex] += amount;
        }
      });

    return {
      labels: months,

      datasets: [
        {
          label: 'Receitas',
          data: income,

          backgroundColor: 'rgba(34, 197, 94, 0.75)',
          borderColor: '#22c55e',
          borderWidth: 1,

          borderRadius: 6,
          borderSkipped: false,

          maxBarThickness: 34,
        },

        {
          label: 'Despesas',
          data: expense,

          backgroundColor: 'rgba(251, 113, 133, 0.75)',
          borderColor: '#fb7185',
          borderWidth: 1,

          borderRadius: 6,
          borderSkipped: false,

          maxBarThickness: 34,
        },
      ],
    };
  });

  monthlyChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    maintainAspectRatio: false,

    interaction: {
      mode: 'index',
      intersect: false,
    },

    plugins: {
      legend: {
        display: false,

        labels: {
          color: '#94a3b8',

          usePointStyle: true,
          pointStyle: 'circle',

          padding: 18,

          font: {
            family: 'inherit',
            size: 12,
            weight: 600,
          },
        },
      },

      tooltip: {
        backgroundColor: '#0f172a',

        titleColor: '#f8fafc',
        bodyColor: '#cbd5e1',

        borderColor: 'rgba(255, 255, 255, 0.08)',
        borderWidth: 1,

        padding: 12,

        cornerRadius: 10,

        callbacks: {
          label: (context) => {
            const value = Number(context.raw);

            return `${context.dataset.label}: ${value.toLocaleString('pt-BR', {
              style: 'currency',
              currency: 'BRL',
            })}`;
          },
        },
      },
    },

    scales: {
      x: {
        grid: {
          display: false,
        },

        border: {
          display: false,
        },

        ticks: {
          color: '#64748b',

          font: {
            size: 11,
            weight: 600,
          },
        },
      },

      y: {
        beginAtZero: true,

        border: {
          display: false,
        },

        grid: {
          color: 'rgba(255, 255, 255, 0.05)',
        },

        ticks: {
          color: '#64748b',

          padding: 10,

          font: {
            size: 11,
          },

          callback: (value) =>
            Number(value).toLocaleString('pt-BR', {
              style: 'currency',
              currency: 'BRL',
              maximumFractionDigits: 0,
            }),
        },
      },
    },
  };

  constructor(
    private readonly transactionService: TransactionService,
    public readonly referenceMonthService: ReferenceMonthService,
  ) {}

  ngOnInit(): void {
    this.loadTransactions();
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
          this.transactions.set(response);
        },

        error: (error) => {
          console.error('Erro ao carregar transações:', error);
        },
      });
  }

  changeReferenceMonth(event: Event): void {
    const select = event.target as HTMLSelectElement;

    this.referenceMonthService.setMonth(Number(select.value));
  }

  openTransactionForm(): void {
    this.showTransactionForm = true;
  }

  closeTransactionForm(): void {
    this.showTransactionForm = false;
  }
}
