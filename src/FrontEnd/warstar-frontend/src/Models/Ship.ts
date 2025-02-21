import { GridComponent } from "./GridComponent";

export interface Ship {
    id: string;
    name: string;
    ownerId?: string;
    mass?: number;
    backgroundPowerDraw?: number;
    shipSize?: number;
    shipLayout?: (GridComponent | null)[][]; // 2D grid layout of the ship
}