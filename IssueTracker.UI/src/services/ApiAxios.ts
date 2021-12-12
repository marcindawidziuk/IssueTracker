// import axios from "axios";

// Creates a single global instance of axios used to connect you API

// TODO: Rm token from user store and baseurl from config
class ApiHelper{
    public baseUrl: string = ""
    public token: string = ""
    
}

export const apiHelper = new ApiHelper()
