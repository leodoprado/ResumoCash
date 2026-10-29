import { Component, OnInit, computed, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { finalize } from 'rxjs';

import { TransactionModal } from '../../components/transaction-modal/transaction-modal';
import { Loading } from '../../components/loading/loading';
import { ConfirmDialog } from '../../components/confirm-dialog/confirm-dialog';

import { ReferenceMonthService } from '../../services/reference-month.service';
import { TransactionService } from '../../services/transaction.service';
import { CompetenceService } from '../../services/competence.service';

import { CreateTransactionRequest, Transaction } from '../../models/transaction.model';
import { Competence } from '../../models/competence.model';

import { LucideLock } from '@lucide/angular';

import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  imports: [
    RouterLink,
    TransactionModal,
    CurrencyPipe,
    Loading,
    BaseChartDirective,
    LucideLock,
    ConfirmDialog,
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  showTransactionForm = false;

  // =====================================================
  // Signals
  // =====================================================

  transactions = signal<Transaction[]>([]);
  competences = signal<Competence[]>([]);

  isLoading = signal(true);
  isLoadingCompetences = signal(true);
  isClosingCompetence = signal(false);

  showCloseCompetenceDialog = signal(false);
  closeCompetenceError = signal<string | null>(null);

  transactionWarning = signal<string | null>(null);

  showCompetenceWarning = signal(false);

  // =====================================================
  // Competência selecionada
  // =====================================================

  selectedCompetence = computed(() => {
    const year = this.referenceMonthService.referenceYear();
    const month = this.referenceMonthService.referenceMonth();

    return this.competences().find((competence) => {
      const [competenceYear, competenceMonth] = competence.month.split('-').map(Number);

      return competenceYear === year && competenceMonth === month;
    });
  });

  // =====================================================
  // Permissões da competência
  // =====================================================

  canCloseCompetence = computed(() => {
    const competence = this.selectedCompetence();

    return (
      !this.isLoadingCompetences() && !this.isClosingCompetence() && competence?.status === 'Open'
    );
  });

  canCreateTransaction = computed(() => {
    const competence = this.selectedCompetence();

    return (
      !this.isLoadingCompetences() && !this.isClosingCompetence() && competence?.status === 'Open'
    );
  });

  // =====================================================
  // Transações do mês selecionado
  // =====================================================

  referenceMonthTransactions = computed(() =>
  this.transactions().filter((transaction) => {
    if (!transaction.competenceMonth) {
      return false;
    }

    const [year, month] = transaction.competenceMonth
      .split('-')
      .map(Number);

    return (
      year === this.referenceMonthService.referenceYear() &&
      month === this.referenceMonthService.referenceMonth()
    );
  }),
);

  // =====================================================
  // Indicadores financeiros
  // =====================================================

  totalIncome = computed(() =>
    this.referenceMonthTransactions()
      .filter(
        (transaction) =>
          transaction.categoryType === 'Income' && transaction.processStatus === 'Completed',
      )
      .reduce((total, transaction) => total + Number(transaction.amount), 0),
  );

  totalExpense = computed(() =>
    this.referenceMonthTransactions()
      .filter(
        (transaction) =>
          transaction.categoryType === 'Expense' && transaction.processStatus === 'Completed',
      )
      .reduce((total, transaction) => total + Number(transaction.amount), 0),
  );

  monthBalance = computed(() => this.totalIncome() - this.totalExpense());

  // =====================================================
  // Gráfico anual
  // =====================================================

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

  // =====================================================
  // Constructor
  // =====================================================

  constructor(
    private readonly transactionService: TransactionService,
    private readonly competenceService: CompetenceService,
    public readonly referenceMonthService: ReferenceMonthService,
  ) {}

  // =====================================================
  // Inicialização
  // =====================================================

  ngOnInit(): void {
    this.loadTransactions();
    this.loadCompetences();
  }

  // =====================================================
  // Carregamento de dados
  // =====================================================

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

  loadCompetences(): void {
    this.isLoadingCompetences.set(true);

    this.competenceService
      .getAll()
      .pipe(
        finalize(() => {
          this.isLoadingCompetences.set(false);
        }),
      )
      .subscribe({
        next: (response) => {
          this.competences.set(response);
        },
        error: (error) => {
          console.error('Erro ao carregar competências:', error);
        },
      });
  }

  // =====================================================
  // Fechamento de competência
  // =====================================================

  requestCloseCompetence(): void {
    if (!this.canCloseCompetence()) {
      return;
    }

    this.closeCompetenceError.set(null);
    this.showCloseCompetenceDialog.set(true);
  }

  cancelCloseCompetence(): void {
    if (this.isClosingCompetence()) {
      return;
    }

    this.showCloseCompetenceDialog.set(false);
  }

  confirmCloseCompetence(): void {
    if (!this.canCloseCompetence()) {
      return;
    }

    const competence = this.selectedCompetence();

    if (!competence) {
      return;
    }

    this.isClosingCompetence.set(true);
    this.closeCompetenceError.set(null);

    this.competenceService
      .close(competence.id)
      .pipe(
        finalize(() => {
          this.isClosingCompetence.set(false);
        }),
      )
      .subscribe({
        next: () => {
          this.showCloseCompetenceDialog.set(false);

          // Atualiza as competências
          this.loadCompetences();

          // Atualiza os indicadores financeiros
          this.loadTransactions();
        },

        error: (error) => {
          console.error('Erro ao fechar competência:', error);

          this.closeCompetenceError.set('Não foi possível fechar a competência.');
        },
      });
  }

  // =====================================================
  // Filtro por mês
  // =====================================================

  changeReferenceMonth(event: Event): void {
    if (this.showCloseCompetenceDialog() || this.isClosingCompetence()) {
      return;
    }

    const select = event.target as HTMLSelectElement;

    this.referenceMonthService.setMonth(Number(select.value));
  }

  // =====================================================
  // Modal de transações
  // =====================================================

  openTransactionForm(): void {
    const competence = this.selectedCompetence();

    if (this.isLoadingCompetences()) {
      return;
    }

    if (!competence || competence.status !== 'Open') {
      this.showCompetenceWarning.set(true);
      return;
    }

    this.showTransactionForm = true;
  }

  closeCompetenceWarning(): void {
    this.showCompetenceWarning.set(false);
  }

  closeTransactionForm(): void {
    this.showTransactionForm = false;
  }

  createTransaction(request: CreateTransactionRequest): void {
    this.transactionService.create(request).subscribe({
      next: () => {
        // Fecha o modal
        this.closeTransactionForm();

        // Atualiza as transações do dashboard
        this.loadTransactions();
      },

      error: (error) => {
        console.error('Erro ao criar transação:', error);
      },
    });
  }
}
