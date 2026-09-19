import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  Category,
  CreateCategoryRequest,
  UpdateCategoryRequest
} from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly apiUrl =
    'http://localhost:5269/api/categories';

  constructor(
    private readonly http: HttpClient
  ) {}

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(
      this.apiUrl
    );
  }

  getById(id: string): Observable<Category> {
    return this.http.get<Category>(
      `${this.apiUrl}/${id}`
    );
  }

  create(
    request: CreateCategoryRequest
  ): Observable<Category> {
    return this.http.post<Category>(
      this.apiUrl,
      request
    );
  }

  update(
    id: string,
    request: UpdateCategoryRequest
  ): Observable<Category> {
    return this.http.put<Category>(
      `${this.apiUrl}/${id}`,
      request
    );
  }
}