import {userStore} from "~/stores/userStore";
import {UsersClient} from "~/src/services/api.generated.clients";

export function avatarUrl(userName: string){
    if (userName)
        return `https://avatars.dicebear.com/api/initials/${userName}.svg`
    return null
}

export interface UserDropdownValue{
    id: number | null;
    name: string | null;
}

export async function loadUserDropdowns(projectId: number) {
    const usersClient = new UsersClient();
    const users = await usersClient.usersForProject(projectId);
    const userDropdowns = users as UserDropdownValue[]
    userDropdowns.unshift({id: null, name: 'Unassigned'})
    return userDropdowns
}