import {
  Component,
  EventEmitter,
  Output
} from '@angular/core';

import { FormsModule } from '@angular/forms';

import {
  CreateCategoryRequest
} from '../../models/category.model';

@Component({
  selector: 'app-category-modal',
  imports: [
    FormsModule
  ],
  templateUrl: './category-modal.html',
  styleUrl: './category-modal.scss',
})
export class CategoryModal {

  name = '';

  type: 'Expense' | 'Income' = 'Expense';

  @Output()
  close = new EventEmitter<void>();

  @Output()
  save = new EventEmitter<CreateCategoryRequest>();

  closeModal(): void {
    this.close.emit();
  }

  saveCategory(): void {
    const name = this.name.trim();

    if (!name) {
      return;
    }

    const request: CreateCategoryRequest = {
      name,
      type: this.type
    };

    this.save.emit(request);
  }
}