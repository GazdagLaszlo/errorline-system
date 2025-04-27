import axiosInstance from "./axios.config.ts";
import {
    Configuration,
    EquipmentApi,
    EquipmentOrderApi,
    FacilityApi,
    IssueApi,
    IssueTypeApi,
    MaintenanceManagerApi,
    UserApi
} from "../../generated-sources/openapi";

const baseURL = `${import.meta.env.VITE_REST_API_URL}`;

const User = new UserApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const Equipment = new EquipmentApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const EquipmentOrder = new EquipmentOrderApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const Facility = new FacilityApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const Issue = new IssueApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const IssueType = new IssueTypeApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);
const MaintenanceManager = new MaintenanceManagerApi(new Configuration({basePath: baseURL}), undefined, axiosInstance);

const api = {
    User,
    Equipment,
    EquipmentOrder,
    Facility,
    Issue,
    IssueType,
    MaintenanceManager
};

export default api;