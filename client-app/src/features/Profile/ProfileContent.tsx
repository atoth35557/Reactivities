import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { IProfile } from "../../app/models/profile";
import Photos from "./Content/Photos";
import Description from "./Content/Description";
import ProfileFollowings from "./Content/Followings";
import Activities from "./Content/Activities";

interface IProps {
  profile: IProfile;
  setActiveTab: (activeTab: any) => void;
}

const panes = [
  { menuItem: "About", render: () => <Description /> },
  { menuItem: "Photos", render: () => <Photos /> },
  {
    menuItem: "Activities",
    render: () => <Activities />,
  },
  {
    menuItem: "Followers",
    render: () => <ProfileFollowings />,
  },
  {
    menuItem: "Followings",
    render: () => <ProfileFollowings />,
  },
];

const ProfileContent: React.FC<IProps> = ({ profile, setActiveTab }) => {
  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
      onTabChange={(e, data) => setActiveTab(data.activeIndex)}
    />
  );
};

export default observer(ProfileContent);
