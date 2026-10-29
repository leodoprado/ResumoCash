import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  signal,
} from '@angular/core';

import { FormsModule } from '@angular/forms';

import {
  Transaction,
  CreateTransactionRequest,
  UpdateTransactionRequest,
} from '../../models/transaction.model';

import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-transaction-modal',
  imports: [FormsModule],
  templateUrl: './transaction-modal.html',
  styleUrl: './transaction-modal.scss',
})
export class TransactionModal implements OnInit, OnChanges {
  @Input()
  transaction: Transaction | null = null;

  
  @Input()
  competenceId: string = '';

  @Output()
  close = new EventEmitter<void>();

  @Output()
  create = new EventEmitter<CreateTransactionRequest>();

  @Output()
  update = new EventEmitter<UpdateTransactionRequest>();

  categories = signal<Category[]>([]);

  categoryId = '';
  description = '';
  amount = 0;
  dueDate = '';

  constructor(private readonly categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['transaction']) {
      this.fillForm();
    }
  }

  private fillForm(): void {
    if (!this.transaction) {
      this.categoryId = '';
      this.description = '';
      this.amount = 0;
      this.dueDate = '';
      return;
    }

    this.categoryId = this.transaction.categoryId;
    this.description = this.transaction.description;
    this.amount = this.transaction.amount;
    this.dueDate = this.transaction.dueDate.substring(0, 10);
  }

  private loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (response) => {
        this.categories.set(response.filter((category) => category.status === 'Active'));
      },

      error: (error) => {
        console.error('Erro ao carregar categorias:', error);
      },
    });
  }

  closeModal(): void {
    this.close.emit();
  }

  saveTransaction(): void {
    const description = this.description.trim();

    if (!this.categoryId || !description || this.amount <= 0) {
      console.error('Formulário inválido.');
      return;
    }

    if (this.transaction) {
      const request: UpdateTransactionRequest = {
        categoryId: this.categoryId,
        description,
        amount: this.amount,
        competenceMonth: this.transaction.competenceMonth,
        dueDate: this.dueDate,
        processStatus: this.transaction.processStatus,
        status: this.transaction.status,
      };

      this.update.emit(request);
      return;
    }

    if (!this.competenceId) {
      console.error('Competência não informada.');
      return;
    }

    const request: CreateTransactionRequest = {
      categoryId: this.categoryId,
      competenceId: this.competenceId,
      description,
      amount: this.amount,
      dueDate: this.dueDate || null,
    };

    console.log('CRIANDO TRANSAÇÃO:', request);

    this.create.emit(request);
  }
}
