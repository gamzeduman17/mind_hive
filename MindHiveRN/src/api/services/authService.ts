import { LoginRequestModel, LoginResponseModel } from "../../models/authModels/Login";
import apiClient from "../apiClient";

export async function login(req: { Username: string, Password: string }) {
    try {
      const response = await apiClient.post("auth/login", req);
      // backend BaseResponseModel<LoginResponseModel> dönüyor
      if (!response.data.Success) {
        throw new Error(response.data.Message || "Login failed");
      }
      return response.data.Data; // LoginResponseModel
    } catch (error: any) {
      if (error.response?.data?.Message) {
        throw new Error(error.response.data.Message);
      }
      throw new Error("Network error");
    }
  }