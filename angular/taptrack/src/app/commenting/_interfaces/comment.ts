export interface Comment {
  id: string;
  author: TeamMember;
  text: string;
  created: Date;
  lastUpdated: Date;
  isEditable: boolean;
  mode: 'preview' | 'editor';
}

interface TeamMember {
  username: string;
  email: string;
}
