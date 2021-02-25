import { Injectable } from "@angular/core";
import { Adapter } from "../shared/adapter";

export class User {
  constructor(
    public userId: number,
    public userName: string,
    public firstName: string,
    public lastName: string,
    public joinDate: Date
  ) {}
}

@Injectable()
export class UserViewModel implements Adapter<User> {
  adapt(item: User): User {
    return new User(item.userId, item.userName, item.firstName, item.lastName, new Date(item.joinDate))
  }
}