import {
  action,
  makeObservable,
  observable,
  runInAction,
  computed,
} from "mobx";
import { RootStore } from "./rootStore";
import { IProfile } from "../models/profile";
import agent from "../api/agent";

export default class ProfileStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeObservable(this);
  }

  @observable profile: IProfile | null = null;
  @observable loadingProfile = true;

  @computed get isCurrentUser() {
    if (this.rootStore.userStore.user && this.profile) { 
      return this.rootStore.userStore.user.userName === this.profile.userName;
    } else {
      return false;
    }
  }

  @action loadProfile = async (username: string) => {
    this.loadingProfile = true;
    try {
      const profile = await agent.Profiles.get(username);
      runInAction(() => {
        this.profile = profile;
        this.loadingProfile = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingProfile = false;
      });
      console.log(error);
    }
  };
}
