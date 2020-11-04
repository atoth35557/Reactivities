import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Header, Image, Item, Segment } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { useContext } from "react";

const activityImageStyle = {
  filter: "brightness(30%)",
};

const ActivityImageTextStyle = {
  position: "absolute",
  bottom: "5%",
  width: "100%",
  height: "auto",
  color: "white",
};
const ActivityDetailedHeader: React.FC<{ activity: IActivity }> = ({
  activity,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { attendActivity, cancelAttendence, loading } = rootStore.activityStore;
  return (
    <Segment.Group>
      <Segment basic attached="top" style={{ padding: "0" }}>
        <Image
          style={activityImageStyle}
          fluid
          src={`/assets/categoryImages/${activity.category}.jpg`}
        />
        <Segment basic style={ActivityImageTextStyle}>
          <Item.Group>
            <Item>
              <Item.Content>
                <Header
                  size="huge"
                  content={activity.title}
                  style={{ color: "white" }}
                />
                <p>{format(activity.date, "eeee do MMMM")}</p>
                <p>Hosted by Bob</p>
              </Item.Content>
            </Item>
          </Item.Group>
        </Segment>
      </Segment>
      <Segment clearing attached="bottom">
        {activity.isHost ? (
          <Button
            as={Link}
            to={`/manage/${activity.id}`}
            color="orange"
            floated="right"
          >
            Manage event
          </Button>
        ) : activity.isGoing ? (
          <Button loading={loading} onClick={cancelAttendence}>
            Cancel attendance
          </Button>
        ) : (
          <Button loading={loading} onClick={attendActivity} color="teal">
            Join Activity
          </Button>
        )}
      </Segment>
    </Segment.Group>
  );
};

export default observer(ActivityDetailedHeader);
