import Login from "../pages/Login.tsx";
import ForgotPassword from "../pages/ForgotPassword.tsx";
import Dashboard from "../pages/Dashboard.tsx";
import UserCreate from "../pages/UserCreate.tsx";
import EquipmentOrder from "../pages/EquipmentOrder.tsx";
import Issues from "../pages/Issues.tsx";
import CreateUpdateIssue from "../pages/CreateUpdateIssue.tsx";
import CreateEquipmentOrder from "../pages/CreateEquipmentOrder.tsx";
import ShowProfile from "../pages/profile/Profile.tsx";

export const routes = [
    {
        path: "login",
        component: <Login/>,
        isPrivate: false
    },
    {
        path: "forgot",
        component: <ForgotPassword/>,
        isPrivate: false
    },
    {
        path: "dashboard",
        component: <Dashboard/>,
        isPrivate: true
    },
    
    {
        path: "equipmentorder",
        component: <EquipmentOrder/>,
        isPrivate: true
    },
    
    {
        path: "usercreate",
        component: <UserCreate/>,
        isPrivate: true
    },
    {
        path: "issues",
        component: <Issues/>,
        isPrivate: true
    },
    {
        path: "issues/create",
        component: <CreateUpdateIssue isCreate={true}/>,
        isPrivate: true
    },
    {
        path: "issues/:id",
        component: <CreateUpdateIssue isCreate={false}/>,
        isPrivate: true
    },
    {
        path: "equipmentorder/create",
        component: <CreateEquipmentOrder/>,
        isPrivate: true
    },
    {
        path: "profile",
        component: <ShowProfile/>,
        isPrivate: true
    }
]