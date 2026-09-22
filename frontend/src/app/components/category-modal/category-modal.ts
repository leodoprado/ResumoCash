import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges
} from '@angular/core';

import { FormsModule } from '@angular/forms';

import {
  Category,
  CategoryStatus,
  CreateCategoryRequest,
  TransactionType,
  UpdateCategoryRequest
} from '../../models/category.model';

@Component({
  selector: 'app-category-modal',
  imports: [
    FormsModule
  ],
  templateUrl: './category-modal.html',
  styleUrl: './category-modal.scss',
})
export class CategoryModal implements OnChanges {

  @Input()
  category: Category | null = null;

  @Output()
  close = new EventEmitter<void>();

  @Output()
  create = new EventEmitter<CreateCategoryRequest>();

  @Output()
  update = new EventEmitter<UpdateCategoryRequest>();

  name = '';

  type: TransactionType = 'Expense';

  status: CategoryStatus = 'Active';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['category']) {
      this.fillForm();
    }
  }

  private fillForm(): void {
    if (!this.category) {
      this.name = '';
      this.type = 'Expense';
      this.status = 'Active';

      return;
    }

    this.name = this.category.name;
    this.type = this.category.type;
    this.status = this.category.status;
  }

  closeModal(): void {
    this.close.emit();
  }

  saveCategory(): void {
    const name = this.name.trim();

    if (!name) {
      return;
    }

    if (this.category) {
      this.update.emit({
        name,
        type: this.type,
        status: this.status
      });

      return;
    }

    this.create.emit({
      name,
      type: this.type
    });
  }
}