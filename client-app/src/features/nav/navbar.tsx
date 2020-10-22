import React from "react";
import { Button, Container, Menu } from "semantic-ui-react";
import { useContext } from 'react';
import ActivityStore from '../../app/stores/activityStore'
import {observer} from 'mobx-react-lite';

const NavBar: React.FC = () => {
  const activityStore = useContext(ActivityStore)
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item>
          <img
            src="/assets/logo.png"
            alt="logo"
            style={{ marginRight: "10px" }}
          />
          Socialnetwork
        </Menu.Item>
        <Menu.Item>Aktivities</Menu.Item>
        <Menu.Item>
          <Button onClick={activityStore.openCreateForm} positive content="Create Activity" />
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);