export interface IssueOnBoardDto{
  id: string;
  title: string;
  project: string;
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
}
