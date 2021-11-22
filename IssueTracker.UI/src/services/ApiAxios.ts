import axios from "axios";
import appConfig from "~/appconfig";
import {userStore} from "~/stores/userStore";

function createAxiosInstance(){
    const instance =  axios.create({
        timeout: 60_000,
    })

    instance.defaults.baseURL = appConfig.apiUrl

    instance.interceptors.request.use(req => {
        req.headers["AUTH-TOKEN"] = userStore.getState().token;
        // if (userStore.getState().token && typeof req.headers === 'object' && req.headers !== null)
        //     req.headers["AUTH-TOKEN"] = userStore.getState().token;
        // else if (userStore.getState().token)
        //     req.headers =
        //         {
        //             "AUTH-TOKEN":  userStore.getState().token,
        //             "Accept": "application/json"
        //         };

        return req;
    });
    
    return instance
}



// Creates a single global instance of axios used to connect you API
export const apiAxios = createAxiosInstance()
