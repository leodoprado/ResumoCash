import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-categories',
  imports: [RouterLink],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class Categories implements OnInit {

  showForm = false;

  categories = signal<Category[]>([]);

  constructor(
    private readonly categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (response) => {

        this.categories.set(response);

        console.log(
          'Categorias recebidas:',
          response
        );
      },

      error: (error) => {
        console.error(
          'Erro ao carregar categorias:',
          error
        );
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