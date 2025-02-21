import { GridComponentType } from "./GridComponentType";

export interface GridComponent {
    id: string; // Unique identifier for the component
    componentType: GridComponentType; // Component type (e.g., "Weapon", "Engine")
    componentTypeId: string; // Component type ID
    spaceShipId: string; // ID of the parent spaceship
    connections: number; // Number of connections
    powerCouplings: number; // Number of power couplings
    properties: string,
    armour: number;
    structuralIntegrity: number;
    lifeSupport: boolean;
    minPowerDraw: number;
    maxPowerDraw: number;
    mass: number;
    price: number;
    topComponentId?: string
    bottomComponentId?: string
    leftComponentId?: string
    rightComponentId?: string
}
