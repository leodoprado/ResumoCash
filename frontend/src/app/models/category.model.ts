export interface Category {
  id: string;
  name: string;
  type: TransactionType;
  status: CategoryStatus;
}

export interface CreateCategoryRequest {
  name: string;
  type: TransactionType;
}

export interface UpdateCategoryRequest {
  name: string;
  type: TransactionType;
  status: CategoryStatus;
}

export type TransactionType =
  | 'Expense'
  | 'Income';

export type CategoryStatus =
  | 'Active'
  | 'Inactive';