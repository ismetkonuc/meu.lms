import { IAssignment } from "./IAssignment";

export interface ITask {
    id: number;
    title: string;
    detail: string;
    creationDate: string;
    expirationDate: string;
    status: boolean;
    courseId: number;
    assignment: IAssignment;
    assignments: IAssignment[];
}

