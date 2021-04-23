export interface FullArticle {
  id: string;
  belongsToId: string;
  projectTitle: string;
  title: string;
  createdBy: TeamMember;
  createdAt: Date;
  updatedBy: TeamMember;
  updatedAt: Date;
  content: string;
}

interface TeamMember {
  username: string;
  email: string;
}
