export interface IssueDetailsDto{
  title: string;
  project: string;
  projectId: string;
  priority: string;
  state: string;
  creator: string;
  assignee: string;
  spent: string;
  estimate: string;
  description: string;
  issueType: string;
  created: Date;
  idVisible: string;
}
