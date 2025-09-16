const { default: axios } = require("axios");

const API_BASE_URL = "http://localhost:5000/api";
const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        "Content-Type": "application/json",
    }
})
export default apiClient;