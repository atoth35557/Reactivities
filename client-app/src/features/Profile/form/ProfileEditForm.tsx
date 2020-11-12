import React from "react";
import { IProfile } from "../../../app/models/profile";
import { combineValidators, isRequired } from "revalidate";
import { Field, Form as FinalForm } from "react-final-form";
import { Button, Form } from "semantic-ui-react";
import TextInput from "../../../app/common/form/TextInput";
import TextAreaInput from "../../../app/common/form/TextAreaInput";

const validate = combineValidators({
  displayName: isRequired("displayName"),
});

interface IProps {
  updateProfile: (profile: IProfile) => void;
  profile: IProfile;
}

const ProfileEditForm: React.FC<IProps> = ({ updateProfile, profile }) => {
  return (
    <FinalForm
      onSubmit={updateProfile}
      validate={validate}
      initialValues={profile}
      render={({ handleSubmit, invalid, pristine, submitting }) => (
        <Form onSubmit={handleSubmit} error>
          <Field
            name="displayName"
            value={profile.displayName}
            placeholder="Display Name"
            component={TextInput}
          />
          <Field
            name="bio"
            value={profile.bio}
            placeholder="Bio"
            component={TextAreaInput}
          />
          <Button
            loading={submitting}
            floated="right"
            positive
            content="Update Profile"
            disabled={invalid || pristine}
          />
        </Form>
      )}
    ></FinalForm>
  );
};

export default ProfileEditForm;
