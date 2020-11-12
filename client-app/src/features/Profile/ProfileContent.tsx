import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { IProfile } from "../../app/models/profile";
import Photos from "./Content/Photos";
import Description from "./Content/Description";

interface IProps {
  profile: IProfile;
}

const panes = [
  { menuItem: "About", render: () => <Description/> },
  { menuItem: "Photos", render: () => <Photos/> },
  {
    menuItem: "Activities",
    render: () => <Tab.Pane>Activities content</Tab.Pane>,
  },
  {
    menuItem: "Followers",
    render: () => <Tab.Pane>Followers content</Tab.Pane>,
  },
  {
    menuItem: "Followings",
    render: () => <Tab.Pane>Followings content</Tab.Pane>,
  },
];

const ProfileContent: React.FC<IProps> = ({ profile }) => {
  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
    />
  );
};

export default observer(ProfileContent);
