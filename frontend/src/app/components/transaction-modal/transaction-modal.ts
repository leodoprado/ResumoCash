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
      return;
    }

    const request: CreateTransactionRequest = {
      categoryId: this.categoryId,
      description,
      amount: this.amount,
      competenceMonth: this.dueDate
        ? `${this.dueDate.substring(0, 7)}-01`
        : `${new Date().getFullYear()}-${String(new Date().getMonth() + 1).padStart(2, '0')}-01`,
      dueDate: this.dueDate,
    };

    this.create.emit(request);
  }
}
