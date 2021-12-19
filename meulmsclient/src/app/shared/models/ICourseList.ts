import { ITaskList } from "./ITaskList";

export interface ICourseList {
    id: number;
    name: string;
    code: string;
    tasks: ITaskList[];
}