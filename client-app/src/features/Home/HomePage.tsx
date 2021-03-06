import React, { Fragment, useContext } from "react";
import { Link } from "react-router-dom";
import { Button, Container, Header, Segment, Image } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import LoginForm from "../User/LoginForm";
import RegisterForm from "../User/RegisterForm";

const HomePage = () => {
  const token = window.localStorage.getItem("jwt");
  const rootStore = useContext(RootStoreContext);
  const { LoggedIn, user } = rootStore.userStore;
  const {openModal} = rootStore.modalStore;

  return (
    <Segment inverted textAlign="center" vertical className="masthead">
      <Container text>
        <Header as="h1" inverted>
          <Image
            size="massive"
            src="/assets/logo.png"
            alt="logo"
            style={{ marginBottom: 12 }}
          />
          DummyNetwork
        </Header>
        {LoggedIn && user && token ? (
          <Fragment>
            <Header
              as="h2"
              inverted
              content={`Welcome back ${user.displayName}`}
            />
            <Button as={Link} to="/activities" size="huge" inverted>
              Go to activities!
            </Button>
          </Fragment>
        ) : (
          <Fragment>
            <Header as="h2" inverted content="Welcome to DummyNetwork" />
            <Button onClick={()=> openModal(<LoginForm/>)} size="huge" inverted>
              Login
            </Button>
            <Button onClick={()=> openModal(<RegisterForm/>)} size="huge" inverted>
              Register
            </Button>
          </Fragment>
        )}
      </Container>
    </Segment>
  );
};

export default HomePage;
