import React, { useContext, useEffect } from "react";
import { Grid } from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader";
import ProfileContent from "./ProfileContent";
import { RootStoreContext } from "../../app/stores/rootStore";
import { RouteComponentProps } from "react-router-dom";
import { LoadingComponent } from "../../app/layout/LoadingComponent";
import { observer } from "mobx-react-lite";

interface RouteParams {
  username: string;
}

interface IProps extends RouteComponentProps<RouteParams> {}

const ProfilePage: React.FC<IProps> = ({ match }) => {
  const rootStore = useContext(RootStoreContext);
  const {
    loadingProfile,
    loadProfile,
    profile,
    isCurrentUser,
    follow,
    unfollow,
    loading,
    setActiveTab
  } = rootStore.profileStore;

  useEffect(() => {
    loadProfile(match.params.username);
  }, [match, loadProfile]);

  if (loadingProfile) return <LoadingComponent content="loading profile..." />;

  return (
    <Grid>
      <Grid.Column width={16}>
        <ProfileHeader
          profile={profile!}
          isCurrentUser={isCurrentUser}
          follow={follow}
          unfollow={unfollow}
          loading={loading}
        />
        <ProfileContent profile={profile!} setActiveTab={setActiveTab}/>
      </Grid.Column>
    </Grid>
  );
};

export default observer(ProfilePage);
