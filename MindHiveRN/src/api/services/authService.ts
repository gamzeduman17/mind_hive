import apiClient from "../apiClient";

export async function login(userName:string, password:string) {
    try {
        const response = await apiClient.post("auth/login", {
            userName, password
        });
        return response.data;
    }
    catch (error:any) {
        if (error.response) {
            throw new Error(error.response.data.message || "Login failed");
        }
        throw new Error("Network error");
    }
}