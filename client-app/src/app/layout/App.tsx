import React, { useState, useEffect, Fragment } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import { IActivity } from "../models/activity";
import Navbar from "../../features/nav/navbar";
import { ActivityDashboar } from '../../features/Activities/dashboard/ActivityDashboar';

const App = () => {
  const [activities, setActivities] = useState<IActivity[]>([]);

  useEffect(() => {
    axios
      .get<IActivity[]>("http://localhost:5000/api/activities")
      .then((response) => {
        setActivities(response.data);
      });
  }, []);

  return (
    <Fragment>
      <Navbar />
      <Container style={{marginTop:'7em'}}>
       <ActivityDashboar activities={activities}/>
      </Container>
    </Fragment>
  );
};

export default App;
