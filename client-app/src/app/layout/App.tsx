import React, { Fragment, useContext, useEffect } from "react";
import { Container } from "semantic-ui-react";
import ActivityDashboar from "../../features/Activities/dashboard/ActivityDashboar";
import NavBar from "../../features/nav/NavBar";
import { observer } from "mobx-react-lite";
import {
  Route,
  withRouter,
  RouteComponentProps,
  Switch,
} from "react-router-dom";
import HomePage from "../../features/Home/HomePage";
import ActivityForm from "../../features/Activities/form/ActivityForm";
import ActivityDetails from "../../features/Activities/Details/ActivityDetails";
import NotFound from "./NotFound";
import { ToastContainer } from "react-toastify";
import LoginForm from "../../features/User/LoginForm";
import { RootStoreContext } from "../stores/rootStore";
import { LoadingComponent } from "./LoadingComponent";
import ModalContainer from "../../app/common/modals/ModalContainer";

const App: React.FC<RouteComponentProps> = ({ location }) => {
  const rootStore = useContext(RootStoreContext);
  const { setAploaded, token, appLoaded } = rootStore.commonStore;
  const { getCurrentUser } = rootStore.userStore;

  useEffect(() => {
    if (token) {
      getCurrentUser().finally(() => setAploaded());
    } else {    
      setAploaded();
    }
  }, [getCurrentUser, setAploaded, token]);

  if (!appLoaded) {
    return <LoadingComponent content="Loading app..." />;
  }

  return (
    <Fragment>
      <ModalContainer/>
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={HomePage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <NavBar />
            <Container style={{ marginTop: "7em" }}>
              <Switch>
                <Route exact path="/activities" component={ActivityDashboar} />
                <Route path="/activities/:id" component={ActivityDetails} />
                <Route
                  key={location.key}
                  path={["/createActivity", "/manage/:id"]}
                  component={ActivityForm}
                />
                <Route path="/login" component={LoginForm} />
                <Route component={NotFound} />
              </Switch>
            </Container>
          </Fragment>
        )}
      />
    </Fragment>
  );
};

export default withRouter(observer(App));
