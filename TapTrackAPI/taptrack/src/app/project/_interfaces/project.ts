import {TeamMember} from './team-member';

export interface Project {
  id: string;
  name: string;
  idVisible: string;
  description: string;
  logoUrl: string | undefined;

  team: TeamMember[];
}

