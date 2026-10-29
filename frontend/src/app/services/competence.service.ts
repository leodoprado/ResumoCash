import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Competence } from '../models/competence.model';

@Injectable({
  providedIn: 'root',
})
export class CompetenceService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'http://localhost:5269/api/competences';

  getAll(): Observable<Competence[]> {
    return this.http.get<Competence[]>(this.apiUrl);
  }

  close(id: string): Observable<void> {
    return this.http.patch<void>(
      `${this.apiUrl}/${id}/close`,
      {},
    );
  }
}