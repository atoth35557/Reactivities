import React, { useEffect, Fragment, useContext } from "react";
import { Container } from "semantic-ui-react";
import ActivityDashboar from "../../features/Activities/dashboard/ActivityDashboar";
import NavBar from "../../features/nav/NavBar";
import { LoadingComponent } from "./LoadingComponent";
import ActivityStore from "../stores/activityStore";
import { observer } from "mobx-react-lite";

const App = () => {
  const activityStore = useContext(ActivityStore);

  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]);

  if (activityStore.loadingInitial) {
    return (
      <Fragment>
        <NavBar />
        <Container style={{ marginTyp: "7em" }}>
          <LoadingComponent content="Loading activities..." />
        </Container>
      </Fragment>
    );
  }

  return (
    <Fragment>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboar />
      </Container>
    </Fragment>
  );
};

export default observer(App);
