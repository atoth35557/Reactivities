export interface IProfile {
  displayName: string;
  userName: string;
  image: string;
  bio: string;
  following: boolean;
  followersCount: number;
  followingsCount: number;
  photos: IPhoto[];
}

export interface IPhoto {
  id: string;
  url: string;
  isMain: boolean;
}
