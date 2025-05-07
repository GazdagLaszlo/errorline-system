export interface IEquipmentOrder {
    id?: number,
    issueDescription?: string;
    equipmentName?: string,
    quantity?: number,
    state?: string | null
}