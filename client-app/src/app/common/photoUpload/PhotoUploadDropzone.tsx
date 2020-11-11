import React, { useCallback } from "react";
import { useDropzone } from "react-dropzone";
import { Header, Icon } from "semantic-ui-react";

interface IProps {
  setFiles: (files: object[]) => void;
}
const PhotoWidgetDropzone: React.FC<IProps> = ({ setFiles }) => {
  const onDrop = useCallback((acceptedFiles) => {
    setFiles(
      acceptedFiles.map((file: object) =>
        Object.assign(file, { preview: URL.createObjectURL(file) })
      )
    );
  }, [setFiles]);
  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  const styles = {
    border: "dashed 3px #eee",
    borderColor: "#eee",
    borderRadius: "5px",
    paddingTop: "30px",
    textAlign: "center" as "center",
    height: "200px",
  };

  const stylesActive = {
    borderColor: "teal",
  };

  return (
    <div
      style={isDragActive ? { ...styles, ...stylesActive } : styles}
      {...getRootProps()}
    >
      <input {...getInputProps()} />
      <Icon size="huge" name="upload"/>
      <Header content="Drop image here"/>
    </div>
  );
};

export default PhotoWidgetDropzone;
