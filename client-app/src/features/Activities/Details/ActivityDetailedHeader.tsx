import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header, Image, Item, Segment } from "semantic-ui-react";
import { IActivity } from '../../../app/models/activity';

const activityImageStyle = {
    filter: 'brightness(30%)'
}

const ActivityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
}
const ActivityDetailedHeader:React.FC<{activity:IActivity}> = ({activity}) => {
  return (
    <Segment.Group>
      <Segment basic attached="top" style={{ padding: "0" }}>
        <Image style={activityImageStyle} fluid src={`/assets/categoryImages/${activity.category}.jpg`} />
        <Segment basic style={ActivityImageTextStyle}>
          <Item.Group>
            <Item>
              <Item.Content>
                <Header
                  size="huge"
                  content={activity.title}
                  style={{ color: "white" }}
                />
                <p>{activity.date}</p>
                <p>Hosted by Bob</p>
              </Item.Content>
            </Item>
          </Item.Group>
        </Segment>
      </Segment>
      <Segment clearing attached="bottom">
        <Button color="teal">Join Activity</Button>
        <Button>Cancel attendance</Button>
        <Button color="orange" floated="right">
          Manage event
        </Button>
      </Segment>
    </Segment.Group>
  );
};

export default observer(ActivityDetailedHeader);
