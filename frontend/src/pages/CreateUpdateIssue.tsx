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
import {useParams} from "react-router-dom";
import {
    EquipmentDto,
    EquipmentOrderResponseDto,
    FacilityDto,
    IssueTypeDto,
    UserDto
} from "../../generated-sources/openapi";
import {roleKeyName} from "../constants/constants.ts";

interface ICreateUpdateIssue {
    isCreate: boolean;
}

type FormValues = {
    description: string;
    issueTypeId: string | null;
    item: string;
    state: string | null;
    parentIssueId: string | null;
    userId: string | null | undefined;
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
            issueTypeId: "1",
            item: "",
            state: "Open",
            parentIssueId: null,
            userId: null,
            modifierUserName: null,
            equipments: [],
            equipmentOrders: [],
            facilityId: "1",
        },

        validate: {
            description: (value) => (value.length === 1 ? 'Leírást kötelező megadni!' : null),
        },
    });

    const [facilities, setFacilities] = useState<FacilityDto[]>([]);
    const [equipments, setEquipments] = useState<EquipmentDto[]>();
    const [equipmentOrders, setEquipmentOrders] = useState<EquipmentOrderResponseDto[]>();
    const [users, setUsers] = useState<UserDto[]>();
    const [issueTypes, setIssueTypes] = useState<IssueTypeDto[]>();
    const { id } = useParams();
    const role = localStorage.getItem(roleKeyName);

    useEffect(() => {
        api.Facility.apiFacilityListGet().then(res => setFacilities(res.data as FacilityDto[]));

        if (id && !isCreate) {
            api.Equipment.apiEquipmentGetALlGet().then(res => setEquipments(res.data));
            api.EquipmentOrder.apiEquipmentOrderGetOrdersByIssueIdIssueIdGet(id).then(res => setEquipmentOrders(res.data));
        }
        api.User.apiUserGetAllUsersGet().then(res => setUsers(res.data));
        api.IssueType.apiIssueTypeGetALlGet().then(res => setIssueTypes(res.data));

    }, []);

    useEffect(() => {
        if (id && !isCreate) {
            api.Issue.apiIssueGetIssueIdGet(parseInt(id)).then(res => {
                form.initialize({
                    description: res.data.description,
                    issueTypeId: res.data.issueType?.id?.toString() ?? "1",
                    item: res.data.item ?? "",
                    state: res.data.state?.toString() ?? "Open",
                    parentIssueId: null,
                    userId: res.data.userId?.toString(),
                    modifierUserName: null,
                    equipments: (res.data.equipments ?? []).map(e => e.id!),
                    equipmentOrders: (res.data.equipmentOrders ?? []).map(o => o.id!),
                    facilityId: res.data.facilityId?.toString() ?? "1"
                });
            });
        }
    }, [id, isCreate]);

    const states: {
        [key: string]: string,
    } = {
        Open: 'Nyitott',
        Blocked: 'Blokkolt',
        InProgress: 'Folyamatban',
        Fixed: 'Javítva',
        Verified: 'Visszajelezve',
        Closed: 'Zárt',
    };

    const handleSubmit = (values: typeof form.values) => {
        if (isCreate) {

                const createDto = {
                    description: values.description!,
                    issueTypeId: Number(values.issueTypeId),
                    item: values.item!,
                    state: values.state,
                    parentIssueId: values.parentIssueId ? Number(values.parentIssueId) : null,
                    equipments: values.equipments!,
                    equipmentOrders: values.equipmentOrders!,
                    facilityId: Number(values.facilityId)
                };

                api.Issue.apiIssueCreateIssuePost( createDto ).then(() => alert("Felvétel sikeres!"))
                    .catch(reason => {

                        if (reason?.response?.data?.errors?.Description) {
                            alert(reason?.response?.data?.errors?.Description.join(', '));
                        }
                        
                        if (reason?.response?.data?.error) {
                            alert(reason.response.data.error);
                        }
                    });
        } else {

                    const updateDto = {
                        description: values.description!,
                        issueTypeId: Number(values.issueTypeId),
                        item: values.item!,
                        state: values.state,
                        parentIssueId: values.parentIssueId ? Number(values.parentIssueId) : null,
                        userId: values.userId ? Number(values.userId) : null,
                        equipments: values.equipments!,
                        equipmentOrders: values.equipmentOrders!,
                        facilityId: Number(values.facilityId)
                    };

                    api.Issue.apiIssueModifyIssueIdPut(parseInt(id!), updateDto ).then(() => alert("Módosítás sikeres!"))
                        .catch(reason => {

                            if (reason?.response?.data?.errors?.Description) {
                                alert(reason?.response?.data?.errors?.Description.join(', '));
                            }

                            if (reason?.response?.data?.error) {
                                alert(reason.response.data.error);
                            }
                        });
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
                    data={issueTypes?.map(type => ({
                        value: type.id?.toString() ?? '',
                        label: type.name ?? ''
                    })) ?? []}
                />

                <NativeSelect
                    withAsterisk
                    label="Státusz"
                    description="Válaszd ki a hiba státuszát"
                    key={form.key('state')}

                    disabled={role !== "Administrator" && role !== "MaintenanceManager" && role !== "MaintenanceWorker"}
                    {...form.getInputProps('state')}
                    data={Object.keys(states).map(state => ({
                        value: state,
                        label: states[state]
                    }))}
                />

                <TextInput
                    label="Szülő hiba ID"
                    placeholder="Ha van, add meg"
                    key={form.key('parentIssueId')}
                    {...form.getInputProps('parentIssueId')}
                    type="number"
                />

                <NativeSelect
                    withAsterisk
                    label="Hozzárendelt felhasználó"
                    key={form.key('userId')}

                    disabled={role !== "Administrator" && role !== "MaintenanceManager"}
                    {...form.getInputProps('userId')}
                    data={(users ? [null, ...users] : [])?.map(user => ({
                        value: user?.id?.toString() ?? '',
                        label: user?.name ?? ''
                    }))}
                />

                <TextInput
                    label="Módosító felhasználónév"
                    key={form.key('modifierUserName')}
                    disabled
                    {...form.getInputProps('modifierUserName')}
                />

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
                            <Table.Th>Mennyiség</Table.Th>
                        </Table.Tr>
                    </Table.Thead>
                    <Table.Tbody>
                        {equipmentOrders?.map((equipment) => (
                            <Table.Tr key={equipment.id}>
                                <Table.Td>{equipment.id}</Table.Td>
                                <Table.Td>{equipment.equipmentName}</Table.Td>
                                <Table.Td>{equipment.quantity}</Table.Td>
                            </Table.Tr>
                        ))}
                    </Table.Tbody>
                </Table>

                

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Mentés</Button>
                </Group>
            </form>
        </Card>

    );
};

export default CreateUpdateIssue;
