export interface IProfile {
  DisplayName: string;
  UserName: string;
  image: string;
  bio: string;
  photos: IPhoto[];
}

export interface IPhoto {
  id: string;
  Url: string;
  IsMain: boolean;
}
