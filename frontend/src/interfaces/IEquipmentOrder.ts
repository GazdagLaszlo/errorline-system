export interface IEquipmentOrder {
    id?: number,
    issueId?: number;
    equipmentId?: number,
    quantity?: number,
    state?: string | null
}