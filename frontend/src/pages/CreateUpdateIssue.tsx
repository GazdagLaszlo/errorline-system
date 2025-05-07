import {
    Button,
    Card,
    Group,
    NativeSelect,
    Table,
    TextInput,
    Textarea,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import {useEffect, useState} from "react";
import api from "../api/api.ts";
import { useParams } from "react-router-dom";
import {EquipmentDto, FacilityDto} from "../../generated-sources/openapi";

interface ICreateUpdateIssue {
    isCreate: boolean;
}

type FormValues = {
    description: string;
    issueTypeId: string | null;
    item: string;
    state: string | null;
    parentIssueId: string | null;
    username: string | null;
    modifierUserName: string | null;
    equipments: number[] | null; // vagy number[] | null, ha teljesen üres is lehet
    equipmentOrders: number[] | null;
    facilityId: string | null;
};

const CreateUpdateIssue = ({ isCreate }: ICreateUpdateIssue) => {
    const form = useForm<FormValues>({
        mode: 'uncontrolled',
        initialValues: {
            description: "",
            issueTypeId: null,
            item: "",
            state: null,
            parentIssueId: null,
            username: null,
            modifierUserName: null,
            equipments: [],
            equipmentOrders: [],
            facilityId: null
        },

        validate: {
            description: (value) => (value.length === 1 ? 'Leírást kötelező megadni!' : null),
        },
    });

    const [facilities, setFacilities] = useState<FacilityDto[]>([]);
    const [equipments, setEquipments] = useState<EquipmentDto[]>();
    const [equipmentOrders, setEquipmentOrders] = useState<EquipmentDto[]>();
    const { id } = useParams();

    useEffect(() => {
        api.Facility.apiFacilityListGet().then(res => setFacilities(res.data as FacilityDto[]));
        api.Equipment.apiEquipmentGetALlGet().then(res => setEquipments(res.data));
        api.EquipmentOrder.apiEquipmentOrderGetAllOrdersGet().then(res => setEquipmentOrders(res.data));
    }, []);

    useEffect(() => {
        if (id && !isCreate) {
            api.Issue.apiIssueGetIssueIdGet(parseInt(id)).then(res => {
                form.initialize({
                    description: res.data.description,
                    issueTypeId: res.data.issueType?.id?.toString() ?? "0",
                    item: res.data.item ?? "",
                    state: res.data.state ?? "0",
                    parentIssueId: null,
                    username: res.data.username, // assigned username (maitenence)
                    modifierUserName: null,
                    equipments: (res.data.equipments ?? []).map(e => e.id!),
                    equipmentOrders: (res.data.equipmentOrders ?? []).map(o => o.id!),
                    facilityId: res.data.facilityId?.toString() ?? "1"
                });
            });
        }
    }, [id, isCreate]);

    const handleSubmit = (values: typeof form.values) => {
        const payload = {
            description: values.description!,
            issueTypeId: Number(values.issueTypeId),
            item: values.item!,
            state: Number(values.state),
            parentIssueId: values.parentIssueId ? Number(values.parentIssueId) : null,
            username: values.username!, // assigned username (maitenence)
            equipments: values.equipments!,
            equipmentOrders: values.equipmentOrders!,
            facilityId: Number(values.facilityId)
        };

        if (isCreate) {
            api.Issue.apiIssueCreateIssuePost( payload ).then();
        } else {
            api.Issue.apiIssueModifyIssueIdPut(parseInt(id!), payload ).then();
        }
    };

    return (
        <Card>
            <form onSubmit={form.onSubmit(values => handleSubmit(values))}>

                <Textarea
                    withAsterisk
                    label="Leírás"
                    placeholder="Írd le a hibát részletesen"
                    minRows={4}
                    key={form.key('description')}
                    {...form.getInputProps('description')}
                />

                <TextInput
                    withAsterisk
                    label="Elem"
                    placeholder="Pl. Egyéb infó"
                    key={form.key('item')}
                    {...form.getInputProps('item')}
                />

                <NativeSelect
                    withAsterisk
                    label="Hibatípus"
                    key={form.key('issueTypeId')}
                    {...form.getInputProps('issueTypeId')}
                    data={[
                        { value: '1', label: 'Típus 1' },
                        { value: '2', label: 'Típus 2' },
                        { value: '3', label: 'Típus 3' },
                    ]}
                />

                <NativeSelect
                    withAsterisk
                    label="Súlyosság"
                    description="Válaszd ki a hiba prioritását"
                    key={form.key('state')}
                    {...form.getInputProps('state')}
                    data={[
                        { value: '0', label: 'Alacsony' },
                        { value: '1', label: 'Közepes' },
                        { value: '2', label: 'Magas' },
                    ]}
                />

                <TextInput
                    label="Szülő hiba ID"
                    placeholder="Ha van, add meg"
                    key={form.key('parentIssueId')}
                    {...form.getInputProps('parentIssueId')}
                    type="number"
                />

                <TextInput
                    withAsterisk
                    label="Hozzárendelt karbantartó felhasználónév"
                    key={form.key('username')}
                    {...form.getInputProps('username')}
                />

                <TextInput
                    label="Módosító felhasználónév"
                    key={form.key('modifierUserName')}
                    disabled
                    {...form.getInputProps('modifierUserName')}
                />

                <Table>
                    <Table.Thead>
                        <Table.Tr>
                            <Table.Th>ID</Table.Th>
                            <Table.Th>Név</Table.Th>
                            <Table.Th>Raktáron</Table.Th>
                            <Table.Th>Mennyiség</Table.Th>
                        </Table.Tr>
                    </Table.Thead>
                    <Table.Tbody>
                        {equipments?.map((equipment) => (
                            <Table.Tr key={equipment.id}>
                                <Table.Td>{equipment.id}</Table.Td>
                                <Table.Td>{equipment.name}</Table.Td>
                                <Table.Td>{equipment.isInStock ? 'Igen' : 'Nem'}</Table.Td>
                                <Table.Td>{equipment.quantity}</Table.Td>
                            </Table.Tr>
                        ))}
                    </Table.Tbody>
                </Table>

                <Table>
                    <Table.Thead>
                        <Table.Tr>
                            <Table.Th>ID</Table.Th>
                            <Table.Th>Név</Table.Th>
                            <Table.Th>Raktáron</Table.Th>
                            <Table.Th>Mennyiség</Table.Th>
                        </Table.Tr>
                    </Table.Thead>
                    <Table.Tbody>
                        {equipmentOrders?.map((equipment) => (
                            <Table.Tr key={equipment.id}>
                                <Table.Td>{equipment.id}</Table.Td>
                                <Table.Td>{equipment.name}</Table.Td>
                                <Table.Td>{equipment.isInStock ? 'Igen' : 'Nem'}</Table.Td>
                                <Table.Td>{equipment.quantity}</Table.Td>
                            </Table.Tr>
                        ))}
                    </Table.Tbody>
                </Table>

                <NativeSelect
                    withAsterisk
                    label="Létesítmény"
                    key={form.key('facilityId')}
                    {...form.getInputProps('facilityId')}
                    data={facilities.map(facility => ({
                        value: facility.id?.toString() ?? '',
                        label: facility.name ?? ''
                    }))}
                />

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Mentés</Button>
                </Group>
            </form>
        </Card>

    );
};

export default CreateUpdateIssue;
