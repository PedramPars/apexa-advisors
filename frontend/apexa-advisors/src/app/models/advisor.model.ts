import { HealthStatus } from "./advisor-details.model";

export interface Advisor {
  id: string;
  name: string;
  sin: string;
  healthStatus: HealthStatus;
}
