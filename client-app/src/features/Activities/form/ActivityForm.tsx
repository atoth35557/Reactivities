import React, { useEffect } from "react";
import { Button, Form, Grid, Segment } from "semantic-ui-react";
import {
  IActivityValues,
  ActivityFormValues,
} from "../../../app/models/activity";
import { useState, useContext } from "react";
import ActivityStore from "../../../app/stores/activityStore";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { Form as FinalForm, Field } from "react-final-form";
import TextInput from "../../../app/common/form/TextInput";
import TextAreaInput from "../../../app/common/form/TextAreaInput";
import SelectInput from "../../../app/common/form/SelectInput";
import { category } from "../../../app/common/options/categoryOptions";
import DateInput from "../../../app/common/form/DateInput";
import { combinedDateAndTime } from "../../../app/common/util/util";

interface DetailParams {
  id: string;
}

const ActivityForm: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history,
}) => {
  const activityStore = useContext(ActivityStore);
  const { submitting, loadActivity, clearActivity } = activityStore;

  const [activity, setActivity] = useState(new ActivityFormValues());

  useEffect(() => {
    if (match.params.id) {
      loadActivity(match.params.id).then((activity) => {
        setActivity(new ActivityFormValues(activity));
      });
    }
  }, [loadActivity, match.params.id]);

  const handleFinalFormSubmit = (value: any) => {
    const dateAndTime = combinedDateAndTime(value.date, value.time);
    const { date, time, ...activity } = value;
    activity.date = dateAndTime;
  };

  return (
    <Grid>
      <Grid.Column width={10}>
        <Segment clearing>
          <FinalForm
            initialValues={activity}
            onSubmit={handleFinalFormSubmit}
            render={({ handleSubmit }) => (
              <Form onSubmit={handleSubmit}>
                <Field
                  name="title"
                  placeholder="Title"
                  value={activity.title}
                  component={TextInput}
                />
                <Field
                  name="description"
                  rows={3}
                  component={TextAreaInput}
                  placeholder="Description"
                  value={activity.description}
                />
                <Field
                  name="category"
                  component={SelectInput}
                  options={category}
                  placeholder="Category"
                  value={activity.category}
                />
                <Form.Group widths="equal">
                  <Field<Date>
                    name="date"
                    date={true}
                    placeholder="Date"
                    value={activity.date}
                    component={DateInput}
                  />
                  <Field<Date>
                    name="time"
                    time={true}
                    placeholder="Time"
                    value={activity.time}
                    component={DateInput}
                  />
                </Form.Group>
                <Field
                  name="city"
                  component={TextInput}
                  placeholder="City"
                  value={activity.city}
                />
                <Field
                  name="venue"
                  component={TextInput}
                  placeholder="Venue"
                  value={activity.venue}
                />
                <Button
                  loading={submitting}
                  floated="right"
                  color="green"
                  type="submit"
                  content="Submit"
                />
                <Button
                  onClick={() => history.push("/activities")}
                  floated="right"
                  color="grey"
                  content="Cancel"
                />
              </Form>
            )}
          />
        </Segment>
      </Grid.Column>
    </Grid>
  );
};

export default observer(ActivityForm);
