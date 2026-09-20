import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';

import { CategoryService } from '../../services/category.service';

import { Category, CreateCategoryRequest } from '../../models/category.model';

import { CategoryModal } from '../../components/category-modal/category-modal';

@Component({
  selector: 'app-categories',
  imports: [RouterLink, CategoryModal],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class Categories implements OnInit {
  showForm = false;

  categories = signal<Category[]>([]);
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

  openForm(): void {
    this.showForm = true;
  }

  closeForm(): void {
    this.showForm = false;
  }
}
