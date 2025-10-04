export interface LoginRequestModel {
    Username: string;
    Password: string;
}
export interface LoginResponseModel {
    message: string;
    username: string;
    role: string;
}
