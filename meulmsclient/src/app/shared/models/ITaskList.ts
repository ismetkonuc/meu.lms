import { IAssignmentList } from "./IAssignmentList";
import { Status } from "./Status";

export interface ITaskList {
    id: number;
    title: string;
    detail: string;
    creationDate: string;
    expirationDate: string;
    status: Status;
    courseId: number;
    courseName: string;
    assignments: IAssignmentList[];
}