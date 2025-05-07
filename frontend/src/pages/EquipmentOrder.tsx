import {
    Table,
    Card,
    Button,
} from "@mantine/core";
import { useEffect, useState } from "react";
import api from "../api/api.ts";
import {useNavigate} from "react-router-dom";
import {IEquipmentOrder} from "../interfaces/IEquipmentOrder.ts";


const EquipmentOrders = () => {
    const [orders, setOrders] = useState<IEquipmentOrder[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        api.EquipmentOrder.apiEquipmentOrderGetAllOrdersGet().then(res =>{
            console.log('orders: '+res.data);
            setOrders(res.data);
        });
    }, []);

    
    const rows = orders.map((orders) => (
        <Table.Tr key={orders.id}>
            <Table.Td>{orders.issueId}</Table.Td>
            <Table.Td>{orders.equipmentId}</Table.Td>
            <Table.Td>{orders.quantity}</Table.Td>
            <Table.Td>{orders.state}</Table.Td>
            {/*<Table.Td><Button onClick={() => navigate(`${orders.id}`)}>Módosítás</Button></Table.Td>*/}
        </Table.Tr>
    ));

    return <>
        <Card shadow="sm" padding="lg" radius="md" withBorder>
            <Button onClick={() => navigate('create')}>Új eszköz rendelés hozzáadása</Button>
            <br />
            <Table>
                <Table.Thead>
                    <Table.Tr>
                        <Table.Th>Hiba azonosító</Table.Th>
                        <Table.Th>Eszköz azonosító</Table.Th>
                        <Table.Th>Mennyiség</Table.Th>
                        <Table.Th>Állapot</Table.Th>
                    </Table.Tr>
                </Table.Thead>
                <Table.Tbody>{rows}</Table.Tbody>
            </Table>
        </Card>
    </>
}

export default EquipmentOrders;