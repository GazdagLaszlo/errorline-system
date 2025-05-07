    import {
        Stack,
        TextInput,
        PasswordInput,
        Button,
        Select
    } from "@mantine/core";
    import {useForm} from "@mantine/form";
    //import {useNavigate} from "react-router-dom";
    import useAuth from "../hooks/useAuth.tsx";

    const Register = () => {

        const { register } = useAuth();
        //const navigate = useNavigate();

        const form = useForm({
            initialValues: {
                name: '',
                email: '',
                password: '',
                roleType: '0',
            },

            validate: {
                name: () => null,
                email: (val: string) => (/^\S+@\S+$/.test(val) ? null : 'Érvénytelen e-mail cím'),
                password: (val: string) => (val.length <= 8 ? 'A jelszónak 8 karakter hosszúnak kell lennie.' : null),
                roleType: () => null
            },
        });

        const submit = () => {
            register(form.values.name, form.values.email, form.values.password, Number(form.values.roleType))
        }

        return (            
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', width: '100%' }}>
                <form onSubmit={form.onSubmit(submit)} style={{width: '30vw'}}>
                    <Stack>
                        <TextInput
                            required
                            label="Név"
                            placeholder="Név"
                            key={form.key('name')}
                            radius="md"
                            {...form.getInputProps('name')}
                        />

                        <TextInput
                            required
                            label="E-mail cím"
                            placeholder="Email cím"
                            key={form.key('email')}
                            radius="md"
                            {...form.getInputProps('email')}
                        />
                
                        <PasswordInput
                            required
                            label="Jelszó"
                            placeholder="Jelszó"
                            key={form.key('password')}
                            radius="md"
                            {...form.getInputProps('password')}
                        />
                        <Select
                            label="Szerepkör"
                            data={[
                                { value: '0', label: 'Resident' },
                                { value: '1', label: 'MaintenanceWorker' },
                                { value: '2', label: 'MaintenanceManager' },
                                { value: '3', label: 'Administrator' },
                            ]}
                            {...form.getInputProps('roleType')}
                        />
                    </Stack>
            
                    <Button type="submit" fullWidth mt="xl" radius="xl">
                        Létrehozás
                    </Button>
                </form>
            </div>            
        );
    };
        
    export default Register;