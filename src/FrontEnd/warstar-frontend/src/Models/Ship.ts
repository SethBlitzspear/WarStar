import { GridComponent } from "./GridComponent";

export interface Ship {
    id: string;
    name: string;
    ownerId?: string;
    shipLayout?: (GridComponent | null)[][]; // 2D grid layout of the ship
}