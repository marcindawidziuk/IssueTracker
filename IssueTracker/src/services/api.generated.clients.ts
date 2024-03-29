/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.10.9.0 (NJsonSchema v10.4.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import axios, { AxiosError, AxiosStatic, AxiosRequestConfig, AxiosResponse, CancelToken } from 'axios';

export class AccountClient {
    private instance: AxiosStatic = axios;
    // @ts-ignore
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    getInfo(  cancelToken?: CancelToken | undefined): Promise<AccountInfoDto> {
        let url_ = this.baseUrl + "/api/Account/get-info";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetInfo(_response);
        });
    }

    protected processGetInfo(response: AxiosResponse): Promise<AccountInfoDto> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = AccountInfoDto.fromJS(resultData200);
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<AccountInfoDto>(<any>null);
    }
}

export class AuthenticationClient {
    private instance: AxiosStatic = axios;
    // @ts-ignore
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    login(request: LoginRequest , cancelToken?: CancelToken | undefined): Promise<string> {
        let url_ = this.baseUrl + "/Authentication/login";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processLogin(_response);
        });
    }

    protected processLogin(response: AxiosResponse): Promise<string> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<string>(<any>null);
    }

    register(request: RegisterRequest , cancelToken?: CancelToken | undefined): Promise<ServiceReponse> {
        let url_ = this.baseUrl + "/Authentication/register";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processRegister(_response);
        });
    }

    protected processRegister(response: AxiosResponse): Promise<ServiceReponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = ServiceReponse.fromJS(resultData200);
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<ServiceReponse>(<any>null);
    }
}

export class IssuesClient {
    private instance: AxiosStatic = axios;
    // @ts-ignore
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    search(projectId?: number | undefined , cancelToken?: CancelToken | undefined): Promise<IssueDto[]> {
        let url_ = this.baseUrl + "/api/Issues/search?";
        if (projectId === null)
            throw new Error("The parameter 'projectId' cannot be null.");
        else if (projectId !== undefined)
            url_ += "projectId=" + encodeURIComponent("" + projectId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processSearch(_response);
        });
    }

    protected processSearch(response: AxiosResponse): Promise<IssueDto[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(IssueDto.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<IssueDto[]>(<any>null);
    }

    add(dto: AddIssueDto , cancelToken?: CancelToken | undefined): Promise<number> {
        let url_ = this.baseUrl + "/api/Issues/add";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAdd(_response);
        });
    }

    protected processAdd(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(<any>null);
    }

    update(dto: UpdateIssueDto , cancelToken?: CancelToken | undefined): Promise<void> {
        let url_ = this.baseUrl + "/api/Issues/update";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processUpdate(_response);
        });
    }

    protected processUpdate(response: AxiosResponse): Promise<void> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            return Promise.resolve<void>(<any>null);
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<void>(<any>null);
    }
}

export class ProjectsControllersClient {
    private instance: AxiosStatic = axios;
    // @ts-ignore
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    getAll(  cancelToken?: CancelToken | undefined): Promise<ProjectDto[]> {
        let url_ = this.baseUrl + "/api/ProjectsControllers/get-all";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: AxiosResponse): Promise<ProjectDto[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ProjectDto.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<ProjectDto[]>(<any>null);
    }

    add(dto: AddProjectDto, userId?: number | undefined , cancelToken?: CancelToken | undefined): Promise<number> {
        let url_ = this.baseUrl + "/api/ProjectsControllers/add?";
        if (userId === null)
            throw new Error("The parameter 'userId' cannot be null.");
        else if (userId !== undefined)
            url_ += "userId=" + encodeURIComponent("" + userId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAdd(_response);
        });
    }

    protected processAdd(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(<any>null);
    }

    update(dto: EditProjectDto, userId?: number | undefined , cancelToken?: CancelToken | undefined): Promise<number> {
        let url_ = this.baseUrl + "/api/ProjectsControllers/update?";
        if (userId === null)
            throw new Error("The parameter 'userId' cannot be null.");
        else if (userId !== undefined)
            url_ += "userId=" + encodeURIComponent("" + userId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(dto);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processUpdate(_response);
        });
    }

    protected processUpdate(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(<any>null);
    }
}

export class Client {
    private instance: AxiosStatic = axios;
    // @ts-ignore
    private baseUrl: string = import.meta.env.VITE_API_URL as string;

    weatherForecast(  cancelToken?: CancelToken | undefined): Promise<WeatherForecast[]> {
        let url_ = this.baseUrl + "/WeatherForecast";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processWeatherForecast(_response);
        });
    }

    protected processWeatherForecast(response: AxiosResponse): Promise<WeatherForecast[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(WeatherForecast.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<WeatherForecast[]>(<any>null);
    }
}

export class AccountInfoDto implements IAccountInfoDto {
    id!: number;
    email!: string | undefined;
    name!: string | undefined;

    constructor(data?: IAccountInfoDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.email = _data["email"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): AccountInfoDto {
        data = typeof data === 'object' ? data : {};
        let result = new AccountInfoDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["email"] = this.email;
        data["name"] = this.name;
        return data; 
    }
}

export interface IAccountInfoDto {
    id: number;
    email: string | undefined;
    name: string | undefined;
}

export class LoginRequest implements ILoginRequest {
    email!: string | undefined;
    password!: string | undefined;

    constructor(data?: ILoginRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): LoginRequest {
        data = typeof data === 'object' ? data : {};
        let result = new LoginRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        data["password"] = this.password;
        return data; 
    }
}

export interface ILoginRequest {
    email: string | undefined;
    password: string | undefined;
}

export class ServiceReponse implements IServiceReponse {
    isSuccessful!: boolean;
    errorMessage!: string | undefined;

    constructor(data?: IServiceReponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.isSuccessful = _data["isSuccessful"];
            this.errorMessage = _data["errorMessage"];
        }
    }

    static fromJS(data: any): ServiceReponse {
        data = typeof data === 'object' ? data : {};
        let result = new ServiceReponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["isSuccessful"] = this.isSuccessful;
        data["errorMessage"] = this.errorMessage;
        return data; 
    }
}

export interface IServiceReponse {
    isSuccessful: boolean;
    errorMessage: string | undefined;
}

export class RegisterRequest implements IRegisterRequest {
    name!: string | undefined;
    email!: string | undefined;
    password!: string | undefined;

    constructor(data?: IRegisterRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.email = _data["email"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): RegisterRequest {
        data = typeof data === 'object' ? data : {};
        let result = new RegisterRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["email"] = this.email;
        data["password"] = this.password;
        return data; 
    }
}

export interface IRegisterRequest {
    name: string | undefined;
    email: string | undefined;
    password: string | undefined;
}

export class IssueDto implements IIssueDto {
    id!: number;
    caseReference!: string | undefined;
    title!: string | undefined;
    statusId!: number;
    statusName!: string | undefined;

    constructor(data?: IIssueDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.caseReference = _data["caseReference"];
            this.title = _data["title"];
            this.statusId = _data["statusId"];
            this.statusName = _data["statusName"];
        }
    }

    static fromJS(data: any): IssueDto {
        data = typeof data === 'object' ? data : {};
        let result = new IssueDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["caseReference"] = this.caseReference;
        data["title"] = this.title;
        data["statusId"] = this.statusId;
        data["statusName"] = this.statusName;
        return data; 
    }
}

export interface IIssueDto {
    id: number;
    caseReference: string | undefined;
    title: string | undefined;
    statusId: number;
    statusName: string | undefined;
}

export class AddIssueDto implements IAddIssueDto {
    projectId!: number;
    title!: string | undefined;
    text!: string | undefined;
    assignedUserId!: number | undefined;
    statusId!: number;

    constructor(data?: IAddIssueDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.projectId = _data["projectId"];
            this.title = _data["title"];
            this.text = _data["text"];
            this.assignedUserId = _data["assignedUserId"];
            this.statusId = _data["statusId"];
        }
    }

    static fromJS(data: any): AddIssueDto {
        data = typeof data === 'object' ? data : {};
        let result = new AddIssueDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["projectId"] = this.projectId;
        data["title"] = this.title;
        data["text"] = this.text;
        data["assignedUserId"] = this.assignedUserId;
        data["statusId"] = this.statusId;
        return data; 
    }
}

export interface IAddIssueDto {
    projectId: number;
    title: string | undefined;
    text: string | undefined;
    assignedUserId: number | undefined;
    statusId: number;
}

export class UpdateIssueDto implements IUpdateIssueDto {
    projectId!: number;
    title!: string | undefined;
    text!: string | undefined;
    assignedUserId!: number | undefined;
    statusId!: number;

    constructor(data?: IUpdateIssueDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.projectId = _data["projectId"];
            this.title = _data["title"];
            this.text = _data["text"];
            this.assignedUserId = _data["assignedUserId"];
            this.statusId = _data["statusId"];
        }
    }

    static fromJS(data: any): UpdateIssueDto {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateIssueDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["projectId"] = this.projectId;
        data["title"] = this.title;
        data["text"] = this.text;
        data["assignedUserId"] = this.assignedUserId;
        data["statusId"] = this.statusId;
        return data; 
    }
}

export interface IUpdateIssueDto {
    projectId: number;
    title: string | undefined;
    text: string | undefined;
    assignedUserId: number | undefined;
    statusId: number;
}

export class ProjectDto implements IProjectDto {
    id!: number;
    name!: string | undefined;

    constructor(data?: IProjectDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): ProjectDto {
        data = typeof data === 'object' ? data : {};
        let result = new ProjectDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data; 
    }
}

export interface IProjectDto {
    id: number;
    name: string | undefined;
}

export class AddProjectDto implements IAddProjectDto {
    name!: string | undefined;
    abbreviation!: string | undefined;
    statuses!: CreateIssueStatusDto[] | undefined;

    constructor(data?: IAddProjectDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.abbreviation = _data["abbreviation"];
            if (Array.isArray(_data["statuses"])) {
                this.statuses = [] as any;
                for (let item of _data["statuses"])
                    this.statuses!.push(CreateIssueStatusDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): AddProjectDto {
        data = typeof data === 'object' ? data : {};
        let result = new AddProjectDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["abbreviation"] = this.abbreviation;
        if (Array.isArray(this.statuses)) {
            data["statuses"] = [];
            for (let item of this.statuses)
                data["statuses"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IAddProjectDto {
    name: string | undefined;
    abbreviation: string | undefined;
    statuses: CreateIssueStatusDto[] | undefined;
}

export class CreateIssueStatusDto implements ICreateIssueStatusDto {
    priority!: number;
    name!: string | undefined;

    constructor(data?: ICreateIssueStatusDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.priority = _data["priority"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): CreateIssueStatusDto {
        data = typeof data === 'object' ? data : {};
        let result = new CreateIssueStatusDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["priority"] = this.priority;
        data["name"] = this.name;
        return data; 
    }
}

export interface ICreateIssueStatusDto {
    priority: number;
    name: string | undefined;
}

export class EditProjectDto implements IEditProjectDto {
    id!: number;
    name!: string | undefined;
    abbreviation!: string | undefined;

    constructor(data?: IEditProjectDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.abbreviation = _data["abbreviation"];
        }
    }

    static fromJS(data: any): EditProjectDto {
        data = typeof data === 'object' ? data : {};
        let result = new EditProjectDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["abbreviation"] = this.abbreviation;
        return data; 
    }
}

export interface IEditProjectDto {
    id: number;
    name: string | undefined;
    abbreviation: string | undefined;
}

export class WeatherForecast implements IWeatherForecast {
    date!: Date;
    temperatureC!: number;
    temperatureF!: number;
    summary!: string | undefined;

    constructor(data?: IWeatherForecast) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>undefined;
            this.temperatureC = _data["temperatureC"];
            this.temperatureF = _data["temperatureF"];
            this.summary = _data["summary"];
        }
    }

    static fromJS(data: any): WeatherForecast {
        data = typeof data === 'object' ? data : {};
        let result = new WeatherForecast();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["date"] = this.date ? this.date.toISOString() : <any>undefined;
        data["temperatureC"] = this.temperatureC;
        data["temperatureF"] = this.temperatureF;
        data["summary"] = this.summary;
        return data; 
    }
}

export interface IWeatherForecast {
    date: Date;
    temperatureC: number;
    temperatureF: number;
    summary: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}

function isAxiosError(obj: any | undefined): obj is AxiosError {
    return obj && obj.isAxiosError === true;
}