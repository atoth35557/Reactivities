import React from "react";
import { Button, Header, Icon, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";

const NotFound = () => {
  return (
    <Segment placeholder>
      <Header icon>
        <Icon name="search" />
        Oops - we've looked everywhere but could't fide this.
      </Header>
      <Segment.Inline>
        <Button as={Link} to="/activities" primary>
          Return to activities
        </Button>
      </Segment.Inline>
    </Segment>
  );
};

export default NotFound;
