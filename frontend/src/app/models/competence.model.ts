export type CompetenceStatus = 'Open' | 'Closed';

export interface Competence {
  id: string;
  userId: string;
  month: string;
  status: CompetenceStatus;
  closedAt: string | null;
  createdAt: string;
  updatedAt: string | null;
}