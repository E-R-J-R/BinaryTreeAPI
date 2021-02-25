export interface Adapter<User> {
    adapt(item: User): User;
}