import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label, Select } from "semantic-ui-react";

interface IProps extends FieldRenderProps<string, any>, FormFieldProps {}

const SelectInput: React.FC<IProps> = ({
  input,
  options,
  placeholder,
  width,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} width={width}>
      <Select
        search
        value={input.value}
        onChange={(e, data) => {
          input.onChange(data.value);
        }}
        placeholder={placeholder}
        options={options}
      />
      {touched && error && (
        <Label color="red" basic>
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default SelectInput;
