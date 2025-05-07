import Login from "../pages/Login.tsx";
import ForgotPassword from "../pages/ForgotPassword.tsx";
import Dashboard from "../pages/Dashboard.tsx";
import UserCreate from "../pages/UserCreate.tsx";
import EquipmentOrder from "../pages/EquipmentOrder.tsx";

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
/*    {
        path: "foods",
        component: <Foods/>,
        isPrivate: true
    },
    {
        path: "foods/create",
        component: <CreateUpdateFoods isCreate={true}/>,
        isPrivate: true
    },
    {
        path: "foods/:id",
        component: <CreateUpdateFoods isCreate={false}/>,
        isPrivate: true
    },*/
]