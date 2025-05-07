import {
    Stack,
    TextInput,
    Button,
    Select
} from "@mantine/core";
import {useForm} from "@mantine/form";
import api from "../api/api.ts";
import { EquipmentDto} from "../../generated-sources/openapi/api.ts";
import { useEffect, useState } from "react";
import { IIssue } from "../interfaces/IIssue.ts";

const EquipmentOrder = () => {

    const [issues, setIssues] = useState<IIssue[]>([]);
    const [equipments, setEquipments] = useState<EquipmentDto[]>([]);

    const form = useForm({
        initialValues: {
            issueId: 0,
            equipmentId: 0,
            quantity: 0,
        },
        validate: {
            quantity: (val: number) => val > 0 ? null : 'A mennyiség nem lehet 0'
        }
    });

    const submit = () => {
        api.EquipmentOrder.apiEquipmentOrderCreateOrderPost({...form.values})
            .then(() => alert("Rendelés rögzítve!"))
    }

    useEffect(() => {
        api.Issue.apiIssueGetAllIssueGet().then(res => setIssues(res.data));
        api.Equipment.apiEquipmentGetALlGet().then(res => setEquipments(res.data))
    }, []);


    return (   
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', width: '100%' }}>
            <form onSubmit={form.onSubmit(submit)} style={{width: '30vw'}}>
                <Stack>
                    <Select
                        label="Hiba leírás"
                        key={form.key('issueId')}
                        {...form.getInputProps('issueId')}
                        data={issues.map(issue => ({
                            value: String(issue.id),
                            label: issue.description ?? ''
                        }))}
                    />

                    <Select
                        label="Eszköz neve"
                        key={form.key('equipmentId')}
                        {...form.getInputProps('equipmentId')}
                        data={equipments.map(equipment => ({
                            value: String(equipment.id),
                            label: equipment.name ?? ''
                        }))}
                    />

                    <TextInput
                        required
                        label="Mennyiség"
                        key={form.key('quantity')}
                        radius="md"
                        {...form.getInputProps('quantity')}
                    />                    
                </Stack>
        
                <Button type="submit" fullWidth mt="xl" radius="xl">
                    Rendelés leadása
                </Button>
            </form>
        </div>            
    );
}

export default EquipmentOrder;