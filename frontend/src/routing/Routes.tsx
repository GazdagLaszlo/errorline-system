import Login from "../pages/Login.tsx";
import ForgotPassword from "../pages/ForgotPassword.tsx";
import Dashboard from "../pages/Dashboard.tsx";
import Foods from "../pages/Foods.tsx";
import CreateUpdateFoods from "../pages/CreateUpdateFoods.tsx";
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
    },
]