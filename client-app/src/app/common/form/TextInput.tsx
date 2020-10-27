import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label } from "semantic-ui-react";

interface IProps
  extends FieldRenderProps<string, any>,
    FormFieldProps {}

const TextInput: React.FC<IProps> = ({
  input,
  type,
  placeholder,
  width,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} type={type} width={width}>
      <input {...input} placeholder={placeholder} />
      {touched && error && (
        <Label color="red" basic>
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default TextInput;
