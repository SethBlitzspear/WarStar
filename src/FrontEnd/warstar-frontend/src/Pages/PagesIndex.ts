import { BasePage } from './BasePage';
import ShipYard from './ShipYard';

// Define the PagesIndex class
export class PagesIndex {
    name: string = "";
    page!: BasePage<any>;
}

// Create an array of pages with their names and components
const pages: PagesIndex[] = [
    { name: ShipYard.displayName, page: ShipYard }
];

export default pages;