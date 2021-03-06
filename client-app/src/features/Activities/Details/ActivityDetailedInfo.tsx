import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React from "react";
import { Grid, Segment, Icon } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";

const ActivityDetailedInfo: React.FC<{ activity: IActivity }> = ({
  activity,
}) => {
  return (
    <Segment.Group>
      <Segment attached="top">
        <Grid>
          <Grid.Column width={1}>
            <Icon size="large" color="teal" name="info" />
          </Grid.Column>
          <Grid.Column width={15}>
            <p>{activity.description}</p>
          </Grid.Column>
        </Grid>
      </Segment>
      <Segment attached>
        <Grid verticalAlign="middle">
          <Grid.Column width={1}>
            <Icon size="large" color="teal" name="calendar" />
          </Grid.Column>
          <Grid.Column width={15}>
            <span>{format(activity.date,"eeee do MMMM")} at {format(activity.date,"h:mm a")}</span>
          </Grid.Column>
        </Grid>
      </Segment>
      <Segment attached>
        <Grid verticalAlign="middle">
          <Grid.Column width={1}>
            <Icon size="large" color="teal" name="marker" />
          </Grid.Column>
          <Grid.Column width={11}>
            <span>{activity.venue},</span>
            <span>{activity.city}</span>
          </Grid.Column>
        </Grid>
      </Segment>
    </Segment.Group>
  );
};

export default observer(ActivityDetailedInfo);
