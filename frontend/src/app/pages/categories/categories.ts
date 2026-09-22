import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';

import {
  LucidePencil,
  LucideTrash,
} from '@lucide/angular';

import { CategoryService } from '../../services/category.service';

import {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest,
} from '../../models/category.model';

import { CategoryModal } from '../../components/category-modal/category-modal';
import { ConfirmDialog } from '../../components/confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-categories',
  imports: [
    RouterLink,
    CategoryModal,
    ConfirmDialog,
    LucidePencil,
    LucideTrash,
  ],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class Categories implements OnInit {
  showForm = false;

  selectedCategory: Category | null = null;

  categories = signal<Category[]>([]);
  categoryToDelete = signal<Category | null>(null);

  isLoading = signal(true);

  constructor(private readonly categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.isLoading.set(true);

    this.categoryService
      .getAll()
      .pipe(
        finalize(() => {
          this.isLoading.set(false);
        }),
      )
      .subscribe({
        next: (response) => {
          this.categories.set(response);
        },

        error: (error) => {
          console.error('Erro ao carregar categorias:', error);
        },
      });
  }

  createCategory(request: CreateCategoryRequest): void {
    this.categoryService.create(request).subscribe({
      next: () => {
        this.closeForm();
        this.loadCategories();
      },

      error: (error) => {
        console.error('Erro ao cadastrar categoria:', error);
      },
    });
  }

  updateCategory(request: UpdateCategoryRequest): void {
    if (!this.selectedCategory) {
      return;
    }

    this.categoryService
      .update(this.selectedCategory.id, request)
      .subscribe({
        next: () => {
          this.closeForm();
          this.loadCategories();
        },

        error: (error) => {
          console.error('Erro ao atualizar categoria:', error);
        },
      });
  }

  requestDelete(category: Category): void {
    this.categoryToDelete.set(category);
  }

  cancelDelete(): void {
    this.categoryToDelete.set(null);
  }

  confirmDelete(): void {
    const category = this.categoryToDelete();

    if (!category) {
      return;
    }

    this.categoryService.delete(category.id).subscribe({
      next: () => {
        this.categoryToDelete.set(null);
        this.loadCategories();
      },

      error: (error) => {
        console.error('Erro ao excluir categoria:', error);
      },
    });
  }

  openForm(): void {
    this.selectedCategory = null;
    this.showForm = true;
  }

  openEditForm(category: Category): void {
    this.selectedCategory = category;
    this.showForm = true;
  }

  closeForm(): void {
    this.selectedCategory = null;
    this.showForm = false;
  }
}