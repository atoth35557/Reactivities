import React, { useContext } from "react";
import { Tab, Header, Card, Image, Button, Grid } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { observer } from "mobx-react-lite";
import { useState } from "react";
import PhotoUploadWidget from "../../../app/common/photoUpload/PhotoUploadWidget";

const Photos = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    profile,
    isCurrentUser,
    uploadPhoto,
    uploadingPhoto,
    loading,
    setMain,
    deletePhoto,
  } = rootStore.profileStore;
  const [addPhotoMode, setAddPhotoMode] = useState(false);
  const [target, setTarget] = useState<string | undefined>(undefined);
  const [deleteTarget, setDeleteTarget] = useState<string | undefined>(
    undefined
  );

  const handleUploadPhoto = (photo: Blob) => {
    uploadPhoto(photo).then(() => {
      setAddPhotoMode(false);
    });
  };
  return (
    <Tab.Pane>
      <Grid>
        <Grid.Column width={16} style={{ paddingBottom: 0 }}>
          <Header floated="left" icon="image" content="Photos" />
          {isCurrentUser && (
            <Button
              floated="right"
              basic
              content={addPhotoMode ? "Cancel" : "Add photo"}
              onClick={() => setAddPhotoMode(!addPhotoMode)}
            />
          )}
        </Grid.Column>
        <Grid.Column width={16}>
          {addPhotoMode ? (
            <PhotoUploadWidget
              uploadPhoto={handleUploadPhoto}
              loading={uploadingPhoto}
            />
          ) : (
            <Card.Group itemsPerRow={4}>
              {profile &&
                profile.photos.map((photo) => (
                  <Card key={photo.id}>
                    <Image src={photo.url} />
                    {isCurrentUser && (
                      <Button.Group
                        fluid
                        widths={2}
                        style={{
                          paddingRight: "1px",
                          paddingLeft: "1px",
                        }}
                      >
                        <Button
                          style={{ borderTopLeftRadius: 0 }}
                          basic
                          positive
                          loading={loading && target === photo.id}
                          disabled={photo.isMain}
                          name={photo.id}
                          onClick={(e) => {
                            setMain(photo);
                            setTarget(e.currentTarget.name);
                          }}
                          content="Main"
                        />
                        <Button
                          name={photo.id}
                          disabled={photo.isMain}
                          onClick={(e) => {
                            deletePhoto(photo);
                            setDeleteTarget(e.currentTarget.name);
                          }}
                          loading={loading && deleteTarget === photo.id}
                          style={{ borderTopRightRadius: 0 }}
                          basic
                          negative
                          icon="trash"
                        />
                      </Button.Group>
                    )}
                  </Card>
                ))}
            </Card.Group>
          )}
        </Grid.Column>
      </Grid>
    </Tab.Pane>
  );
};

export default observer(Photos);
