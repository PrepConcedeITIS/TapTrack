export interface InvitationDto {
  userName: string;
  status: string;
  projectName: string;
  role: string;
}

export interface InvitationDetailedDto extends InvitationDto {
  id: string;
}
