import { Injectable } from "@angular/core";
import { Adapter } from "../shared/adapter";

export class UserObj {
  constructor(
    public userId: number,
    public userName: string,
    public firstName: string,
    public lastName: string,
    public joinDate: Date
  ) {}
}

@Injectable()
export class UserObjAdapter implements Adapter<UserObj> {
  adapt(item: any): UserObj {
    return new UserObj(item.userId, item.userName, item.firstName, item.lastName, new Date(item.joinDate))
  }
}