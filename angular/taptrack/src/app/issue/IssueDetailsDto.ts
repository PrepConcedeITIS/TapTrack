export interface IssueDetailsDto{
  title: string;
  project: string;
  projectId: string;
  priority: string;
  state: string;
  creator: string;
  assignee: string;
  estimationHours: number;
  estimationMinutes: number;
  spentHours: number;
  spentMinutes: number;
  description: string;
  issueType: string;
  created: string;
  idVisible: string;
  id: string;
}
