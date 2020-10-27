export interface IActivity {
  id: string;
  title: string;
  description: string;
  category: string;
  date: Date;
  city: string;
  venue: string;
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
