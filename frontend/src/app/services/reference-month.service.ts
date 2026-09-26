import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReferenceMonthService {
  private readonly today = new Date();

  readonly referenceYear = signal(
    this.today.getFullYear()
  );

  readonly referenceMonth = signal(
    this.today.getMonth() + 1
  );

  setMonth(month: number): void {
    this.referenceMonth.set(month);
  }

  setYear(year: number): void {
    this.referenceYear.set(year);
  }
}