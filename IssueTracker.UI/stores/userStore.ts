import {Store} from "./Store";
import {AccountClient, AccountInfoDto, IAccountInfoDto} from "~/src/services/api.generated.clients";

interface UserData{
    user: IAccountInfoDto | null;
    token: string | null;
}

const tokenLocalStorageKey = "issue-tracker-token"

export class UserStore extends Store<UserData>{
    constructor() {
        super();
    }
    protected data(): UserData {
        if (process.client){
            const token = localStorage.getItem(tokenLocalStorageKey)
            return {
                user: null,
                token: token
            }
        }
        return {
            user: null,
            token: null
        }
    }

    async refreshUser() {
        const client = new AccountClient();
        this.state.user = await client.getInfo()
    }

    logout(){
        this.state.user = null
        this.setToken(null)
    }

    setUser(user: IAccountInfoDto){
        this.state.user = user
    }

    getUser(): IAccountInfoDto | null{
        return this.state.user
    }

    isAuthenticated(): boolean{
        return !!this.state.token;
    }


    setToken(token: string | null){
        this.state.token = token
        console.log('update', token)
        console.log('update', process)
        if (process.client) {
            localStorage.setItem(tokenLocalStorageKey, token ?? "");
        }
    }

}

export const userStore = new UserStore()
