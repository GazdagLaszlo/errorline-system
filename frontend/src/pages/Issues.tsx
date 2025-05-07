import {Button, Card, Table} from "@mantine/core";
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import api from "../api/api.ts";
import {IssueResponseDto} from "../../generated-sources/openapi";

const Issues = () => {
    const [issues, setIssues] = useState<IssueResponseDto[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        api.Issue.apiIssueGetAllIssueGet().then(res => {
            console.log(res);
            setIssues(res.data);
        }).catch(error => {
            console.error("Nem sikerült betölteni a hibajegyeket:", error);
        });
    }, []);

    const rows = issues.map((issue) => (
        <Table.Tr key={issue.id}>
            <Table.Td>{issue.description}</Table.Td>
            <Table.Td>{issue.facilityId}</Table.Td>
            <Table.Td>{issue.state}</Table.Td>
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