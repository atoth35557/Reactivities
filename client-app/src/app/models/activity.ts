export interface IActivityEnvelope {
  activities: IActivity [];
  activityCount: number;
}

export interface IActivity {
  id: string;
  title: string;
  description: string;
  category: string;
  date: Date;
  city: string;
  venue: string;
  isGoing: boolean;
  isHost: boolean;
  attendees: IAttendee[];
  comments: IComment[];
}

export interface IComment {
  id: string;
  body: string;
  createdAt: Date;
  username: string;
  displayName: string;
  image: string;
}

export interface IActivityValues extends Partial<IActivity> {
  time?: Date;
}

export class ActivityFormValues implements IActivityValues {
  id?: string = undefined;
  title: string = "";
  category: string = "";
  description: string = "";
  date?: Date = undefined;
  time?: Date = undefined;
  city: string = "";
  venue: string = "";

  constructor(init?: IActivityValues) {
    if (init && init.date) {
      init.time = init.date;
    }
    Object.assign(this, init);
  }
}

export interface IAttendee {
  username: string;
  displayName: string;
  image: string;
  isHost: boolean;
  following?: boolean;
}
