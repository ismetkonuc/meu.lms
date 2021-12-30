import {Status} from './Status'

export interface IAddTask {
    title: string;
    detail: string;
    expirationDateAsString: string;
    status: Status;
    courseId: number;
}