import {IssueDto, IssueStatusDto} from "~/src/services/api.generated.clients";

export interface IssuesByColumn
{
    status: IssueStatusDto,
    issues: IssueDto[]
}