import {Button, Card, Table} from "@mantine/core";
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import api from "../api/api.ts";
import {FacilityDto, IssueResponseDto} from "../../generated-sources/openapi";

const Issues = () => {
    const [facilities, setFacilities] = useState<FacilityDto[]>([]);
    const [issues, setIssues] = useState<IssueResponseDto[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        api.Facility.apiFacilityListGet().then(res => setFacilities(res.data as FacilityDto[]));
        
        api.Issue.apiIssueGetAllIssueGet().then(res => {
            setIssues(res.data);
        }).catch(error => {
            console.error("Nem sikerült betölteni a hibajegyeket:", error);
        });
    }, []);

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

    const rows = issues.map((issue) => (
        <Table.Tr key={issue.id}>
            <Table.Td>{issue.description}</Table.Td>
            <Table.Td>{facilities.find(facility => facility.id == issue.facilityId)?.name ?? issue.facilityId}</Table.Td>
            <Table.Td>{states[issue.state!.toString()]}</Table.Td>
            <Table.Td>
                <Button onClick={() => navigate(`${issue.id}`)}>Módosítás</Button>
            </Table.Td>
        </Table.Tr>
    ));

    return (
        <Card shadow="sm" padding="lg" radius="md" withBorder>
            <Table>
                <Table.Thead>
                    <Table.Tr>
                        <Table.Th>Leírás</Table.Th>
                        <Table.Th>Hely</Table.Th>
                        <Table.Th>Állapot</Table.Th>
                        <Table.Th>Műveletek</Table.Th>
                    </Table.Tr>
                </Table.Thead>
                <Table.Tbody>{rows}</Table.Tbody>
            </Table>
            <Button onClick={() => navigate("create")}>Új hibajegy</Button>
        </Card>
    );
};

export default Issues;