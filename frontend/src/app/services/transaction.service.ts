import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  Transaction,
  CreateTransactionRequest,
  UpdateTransactionRequest
} from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private readonly apiUrl =
    'http://localhost:5269/api/transactions';

  constructor(
    private readonly http: HttpClient
  ) {}

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(
      this.apiUrl
    );
  }

  getById(id: string): Observable<Transaction> {
    return this.http.get<Transaction>(
      `${this.apiUrl}/${id}`
    );
  }

  create(
    request: CreateTransactionRequest
  ): Observable<Transaction> {
    return this.http.post<Transaction>(
      this.apiUrl,
      request
    );
  }

  update(
    id: string,
    request: UpdateTransactionRequest
  ): Observable<Transaction> {
    return this.http.put<Transaction>(
      `${this.apiUrl}/${id}`,
      request
    );
  }

  complete(id: string): Observable<void> {
    return this.http.patch<void>(
      `${this.apiUrl}/${id}/completed`,
      {}
    );
  }

  reopen(id: string): Observable<void> {
    return this.http.patch<void>(
      `${this.apiUrl}/${id}/reopen`,
      {}
    );
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }
}