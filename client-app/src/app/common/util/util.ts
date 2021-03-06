import { IActivity, IAttendee } from "../../models/activity";
import { IUser } from "../../models/user";
export const combinedDateAndTime = (date: Date, time: Date) => {
  const dateString = date.toISOString().split("T")[0];
  const timeString = date.toISOString().split("T")[1];
  return new Date(dateString + "T" + timeString);
};

export const setActivityProps = (activity: IActivity, user: IUser) => {
  activity.date = new Date(activity.date);
  activity.isGoing = activity.attendees.some(
    (a) => a.username === user.userName
  );
  activity.isHost = activity.attendees.some(
    (a) => a.username === user.userName && a.isHost
  );
  return activity;
};

export const createAttendee = (user: IUser): IAttendee => {
  return {
    displayName: user.displayName,
    username: user.userName,
    image: user.image!,
    isHost: false,
  };
};
