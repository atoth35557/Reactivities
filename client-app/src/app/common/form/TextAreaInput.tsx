import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label } from "semantic-ui-react";

interface IProps extends FieldRenderProps<string, HTMLElement>, FormFieldProps {}

const TextAreaInput: React.FC<IProps> = ({
  input,
  rows,
  placeholder,
  width,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} width={width}>
      <textarea rows={rows} {...input} placeholder={placeholder} />
      {touched && error && (
        <Label color="red" basic>
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default TextAreaInput;
