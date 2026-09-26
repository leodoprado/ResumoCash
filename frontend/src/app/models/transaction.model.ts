export interface Transaction {
  id: string;
  categoryId: string;
  categoryName: string;
  categoryType: 'Expense' | 'Income';
  description: string;
  amount: number;
  competenceMonth: string;
  dueDate: string;
  processStatus: TransactionProcessStatus;
  status: TransactionStatus;
  completedAt: string | null;
  createdAt: string;
}

export interface CreateTransactionRequest {
  categoryId: string;
  description: string;
  amount: number;
  competenceMonth: string;
  dueDate: string;
}

export interface UpdateTransactionRequest {
  categoryId: string;
  description: string;
  amount: number;
  competenceMonth: string;
  dueDate: string;
  processStatus: TransactionProcessStatus;
  status: TransactionStatus;
}

export type TransactionProcessStatus =
  | 'Pending'
  | 'Completed';

export type TransactionStatus =
  | 'Active'
  | 'Inactive';