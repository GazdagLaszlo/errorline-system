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

const CreateUpdateIssue = ({ isCreate }: ICreateUpdateIssue) => {
    const form = useForm({
        mode: 'uncontrolled',
        initialValues: {
            description: "",
            issueTypeId: 0,
            item: "",
            state: 0,
            parentIssueId: 0,
            username: "",
            modifierUserName: "",
            equipments: [
                0
            ],
            equipmentOrders: [
                0
            ],
            facilityId: 0
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
                    issueTypeId: res.data.issueType?.id ?? 0,
                    item: res.data.item ?? "",
                    state: parseInt(res.data.state ?? "0"),
                    parentIssueId: 0,
                    username: res.data.username,
                    modifierUserName: "", // TODO: ezt meg kell oldani
                    equipments: (res.data.equipments ?? []).map(e => e.id!),
                    equipmentOrders: (res.data.equipmentOrders ?? []).map(o => o.id!),
                    facilityId: res.data.facilityId ?? 0
                });
            });
        }
    }, [id, isCreate]);

    const handleSubmit = (values: typeof form.values) => {
        const payload = {
            description: values.description,
            issueTypeId: values.issueTypeId,
            item: values.item,
            state: values.state,
            parentIssueId: values.parentIssueId,
            username: values.username,
            modifierUserName: values.modifierUserName,
            equipments: values.equipments,
            equipmentOrders: values.equipmentOrders,
            facilityId: values.facilityId
        };

        if (isCreate) {
            api.Issue.apiIssueCreateIssuePost( payload ).then();
        } else {
            api.Issue.apiIssueModifyIssueIdPut(parseInt(id!), payload ).then();
        }
    };

    return (
        <Card>
            <form onSubmit={form.onSubmit(handleSubmit)}>

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
                    label="Bejelentő felhasználónév"
                    key={form.key('username')}
                    {...form.getInputProps('username')}
                />

                <TextInput
                    label="Módosító felhasználónév"
                    key={form.key('modifierUserName')}
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
                        value: String(facility.id),
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
