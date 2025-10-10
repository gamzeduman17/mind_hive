import { BaseResponseModel } from "../../models/BaseModels";
import { LoginRequestModel, LoginResponseModel } from "../../models/LoginModels";
import { UserRegisterRequestModel } from "../../models/UserModels";
import apiClient from "../apiClient";

export async function login(req: LoginRequestModel) {
    try {
        const response = await apiClient.post("Auth/login", req);
        if (!response.data.Success) {
            console.log("response",response);
            throw new Error(response.data.Message || "Login failed");
          
        }
        return response.data.Data;
    } catch (error: any) {
        console.log("response",error);
        if (error.response?.data?.Message) {
            throw new Error(error.response.data.Message);
        }
        throw new Error("Network error");
    }
}
export function logout(): void {
    // Örnek: AsyncStorage veya Redux ile token temizleme
    // AsyncStorage.removeItem("token");
    console.log("User logged out");
}
export async function register(req: UserRegisterRequestModel): Promise<LoginResponseModel> {
    try {
        const response = await apiClient.post<BaseResponseModel<LoginResponseModel>>("auth/register", req);
        if (!response.data.Data) {
            throw new Error("Registration succeeded but no data returned");
        }
        return response.data.Data;
    } catch (error: any) {
        if (error.response?.data?.message) {
            throw new Error(error.response.data.message);
        }
        throw new Error("Network error");
    }
}