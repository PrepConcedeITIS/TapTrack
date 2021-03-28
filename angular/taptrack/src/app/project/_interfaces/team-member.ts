import {User} from './user';

export interface TeamMember {
  isAdmin: boolean;
  role: string;

  user: User;
}
