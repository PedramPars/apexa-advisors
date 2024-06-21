export interface AdvisorDetails {
  id: string;
  name: string;
  sin: string;
  address?: string;
  phone?: string;
  healthStatus: HealthStatus;
}

export enum HealthStatus {
  Green = 'Green',
  Yellow = 'Yellow',
  Red = 'Red',
}
