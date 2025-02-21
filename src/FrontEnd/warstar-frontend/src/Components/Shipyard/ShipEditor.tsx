import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { GridComponent } from '../../Models/GridComponent';
import ShipComponentPalette from './ShipComponentPalette';
import ShipGrid from './ShipGrid';
import { UserModel } from '../../Models/UserModel';
import { Ship } from '../../Models/Ship';

type ShipEditorProps = {
    spaceShipId: string;
    loggedInUser: UserModel | null;
};

const ShipEditor: React.FC<ShipEditorProps> = ({ spaceShipId, loggedInUser }) => {
    const [ship, setShip] = useState<Ship | null>(null);
    const [localGrid, setLocalGrid] = useState<(GridComponent | null)[][]>([]);
    const apiBaseUrl = process.env.REACT_APP_SPACESHIP_API_BASE_URL;

    useEffect(() => {
        fetchShip();
    }, [spaceShipId]);

    const fetchShip = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}ShipYard/GetSpaceShip/${spaceShipId}`, {
                headers: { Authorization: `Bearer ${loggedInUser?.token}` },
            });

            const fetchedShip: Ship = response.data;
            setShip(fetchedShip);

            if (Array.isArray(fetchedShip.shipLayout) && fetchedShip.shipLayout.every(row => Array.isArray(row))) {
                const mappedGrid = fetchedShip.shipLayout.map(row =>
                    row.map(cell => (cell ? mapGridComponent(cell) : null))
                );

                setLocalGrid(expandGrid(mappedGrid));
            } else {
                console.error('Invalid grid data format:', fetchedShip.shipLayout);
                setLocalGrid([]);
            }
        } catch (error) {
            console.error('Error fetching ship:', error);
            setLocalGrid([]);
        }
    };

    const mapGridComponent = (cell: any): GridComponent => ({
        id: cell.id || '',
        componentType: {
            id: cell.componentType.id || '',
            type: cell.componentType.type || 'Unknown',
            name: cell.componentType.name || 'Unknown',
            shortName: cell.componentType.shortName || 'Unknown',
            selected: cell.componentType.selected || false,
            armour: cell.armour || 0,
            structuralIntegrity: cell.structuralIntegrity || 0,
            lifeSupport: cell.lifeSupport || 0,
            minPowerDraw: cell.minPowerDraw || 0,
            maxPowerDraw: cell.maxPowerDraw || 0,
            mass: cell.mass || 0,
            price: cell.price || 0,
        },
        spaceShipId: cell.spaceShipId || '',
        connections: cell.connections || 0,
        componentTypeId: cell.componentTypeId || '',
        powerCouplings: cell.powerCouplings || 0,
        properties: cell.properties || '',
        topComponentId: cell.topComponentId || undefined,
        bottomComponentId: cell.bottomComponentId || undefined,
        leftComponentId: cell.leftComponentId || undefined,
        rightComponentId: cell.rightComponentId || undefined,
        armour: cell.armour || 0,
        structuralIntegrity: cell.structuralIntegrity || 0,
        lifeSupport: cell.lifeSupport || 0,
        minPowerDraw: cell.minPowerDraw || 0,
        maxPowerDraw: cell.maxPowerDraw || 0,
        mass: cell.mass || 0,
        price: cell.price || 0,
    });

    const expandGrid = (originalGrid: (GridComponent | null)[][]): (GridComponent | null)[][] => {
        const originalRows = originalGrid.length;
        const originalCols = originalGrid[0]?.length || 0;
        const targetRows = Math.ceil(originalRows / 10) * 10;
        const targetCols = Math.ceil(originalCols / 10) * 10;

        const expandedGrid: (GridComponent | null)[][] = Array.from({ length: targetRows }, () =>
            Array.from({ length: targetCols }, () => null)
        );

        const rowOffset = Math.floor((targetRows - originalRows) / 2);
        const colOffset = Math.floor((targetCols - originalCols) / 2);

        for (let i = 0; i < originalRows; i++) {
            for (let j = 0; j < originalCols; j++) {
                expandedGrid[i + rowOffset][j + colOffset] = originalGrid[i][j];
            }
        }

        return expandedGrid;
    };

    const handleGridUpdate = (updatedGrid: (GridComponent | null)[][]) => {
        setLocalGrid(updatedGrid);
        console.log('Updated grid:', updatedGrid);
    };

    return (
        <div style={{ display: 'flex', flexDirection: 'column', height: '100vh' }}>
            {/* Palette Section */}
            <div
                style={{
                    display: 'flex',
                    flexDirection: 'column',
                    width: '100%',
                    backgroundColor: '#f0f0f0',
                    padding: '10px',
                    flex: '0 1 auto',
                }}
            >
                <ShipComponentPalette />
            </div>

            {/* Separator Line */}
            <div style={{ height: '2px', width: '100%', backgroundColor: '#000' }} />

            {/* Ship Grid Section */}
            <div style={{ flex: '1 1 0', overflow: 'auto' }}>
                <ShipGrid grid={localGrid} spaceShipId={spaceShipId} onUpdate={handleGridUpdate} />
            </div>
        </div>
    );
};

export default ShipEditor;
