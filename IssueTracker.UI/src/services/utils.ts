import {userStore} from "~/stores/userStore";

export function avatarUrl(userName: string){
    if (userName)
        return `https://avatars.dicebear.com/api/initials/${userName}.svg`
    return null
}