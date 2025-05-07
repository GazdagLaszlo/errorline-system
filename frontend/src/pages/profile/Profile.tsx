import {Card, NativeSelect, TextInput,} from "@mantine/core";
import {useForm} from "@mantine/form";
import {useEffect} from "react";
import api from "../../api/api.ts";

const ShowProfile = () => {
    const form = useForm({
        mode: 'uncontrolled',
        initialValues: {
            id: 0,
            name: '',
            email: '',
            roleType: 0,
        },
    });

    useEffect(() => {
        api.User.apiUserMeGet().then(res => {
            form.initialize({
                id: res.data.id!,
                name: res.data.name!,
                email: res.data.email!,
                roleType: res.data.roleType!,
            });
        })
    }, []);

    const roleTypes = [
        { value: '0', label: 'Resident' },
        { value: '1', label: 'MaintenanceWorker' },
        { value: '2', label: 'MaintenanceManager' },
        { value: '3', label: 'Administrator' },
    ]

    return <>
        <Card>
            <form>
                <TextInput
                    withAsterisk
                    label="Azonosító"
                    placeholder="Azonosító"
                    key={form.key('id')}
                    {...form.getInputProps('id')}
                />

                <TextInput
                    withAsterisk
                    label="Név"
                    placeholder="Név"
                    key={form.key('name')}
                    {...form.getInputProps('name')}
                />

                <TextInput
                    withAsterisk
                    label="Email"
                    placeholder="Email"
                    key={form.key('email')}
                    {...form.getInputProps('email')}
                />

                <NativeSelect
                    label="Szerepkör"
                    description="Szerepkör"
                    key={form.key('roleType')}
                    {...form.getInputProps('roleType')}
                    data={roleTypes}
                />
            </form>
        </Card>

    </>
}

export default ShowProfile;
