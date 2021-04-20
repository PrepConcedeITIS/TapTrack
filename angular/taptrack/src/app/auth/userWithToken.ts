import {User} from './user';

export interface UserWithToken {
  token: string;
  user: User;
}
