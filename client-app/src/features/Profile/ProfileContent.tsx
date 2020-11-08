import React from "react";
import { Tab } from "semantic-ui-react";
import { IProfile } from "../../app/models/profile";

interface IProps {
  profile: IProfile;
}

const panes = [
  { menuItem: "About", render: () => <Tab.Pane>About content</Tab.Pane> },
  { menuItem: "Photos", render: () => <Tab.Pane>Photos content</Tab.Pane> },
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

export default ProfileContent;
