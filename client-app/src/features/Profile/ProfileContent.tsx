import { observer } from "mobx-react-lite";
import React from "react";
import { Tab } from "semantic-ui-react";
import { IProfile } from "../../app/models/profile";
import Photos from "./Content/Photos";

interface IProps {
  profile: IProfile;
}

const panes = [
  { menuItem: "About", render: () => <Tab.Pane>About content</Tab.Pane> },
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
      activeIndex={1}
    />
  );
};

export default observer(ProfileContent);
