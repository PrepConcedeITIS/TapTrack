export interface Comment {
  id: string;
  author: TeamMember;
  text: string;
  isDeleted: boolean;
  created: Date;
  lastUpdated: Date;
  isEditable: boolean;
  isDeletable: boolean;
  mode: 'preview' | 'editor';
}

interface TeamMember {
  username: string;
  email: string;
}
