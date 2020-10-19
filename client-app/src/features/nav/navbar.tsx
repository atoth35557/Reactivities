import React from "react";
import { Button, Container, Menu } from "semantic-ui-react";

export default function Navbar() {
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item>
            <img src="/assets/logo.png" alt="logo" style={{marginRight:'10px'}}/>
            Socialnetwork
        </Menu.Item>
        <Menu.Item >Aktivities</Menu.Item>
        <Menu.Item>
            <Button positive content='Create Activity'/>
        </Menu.Item>
      </Container>
    </Menu>
  );
}
