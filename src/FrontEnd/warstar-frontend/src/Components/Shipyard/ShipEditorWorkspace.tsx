import React, { useState, useEffect, useCallback } from "react";
import axios from "axios";
import ShipGrid from "./ShipGrid";
import ShipDetails from "./ShipDetails";
import { UserModel } from "../../Models/UserModel";
import { GridComponent } from "../../Models/GridComponent";
import { Ship } from "../../Models/Ship";

type ShipEditorWorkspaceProps = {
    spaceShipId: string;
    loggedInUser: UserModel | null;
};

const ShipEditorWorkspace: React.FC<ShipEditorWorkspaceProps> = ({ spaceShipId, loggedInUser }) => {
    const [ship, setShip] = useState<Ship | null>(null);
    const [shipGrid, setShipGrid] = useState<(GridComponent | null)[][]>([]);
    const apiBaseUrl = process.env.REACT_APP_SPACESHIP_API_BASE_URL;

    // Fetch ship data when spaceShipId changes
    useEffect(() => {
        fetchShip();
    }, [spaceShipId]);

    const fetchShip = useCallback(async () => {
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

                setShipGrid(expandGrid(mappedGrid));
            } else {
                console.error("Invalid grid data format:", fetchedShip.shipLayout);
                setShipGrid([]);
            }
        } catch (error) {
            console.error("Error fetching ship:", error);
            setShipGrid([]);
        }
    }, [spaceShipId, loggedInUser, apiBaseUrl]);

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
            lifeSupport: cell.lifeSupport || false,
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
        lifeSupport: cell.lifeSupport || false,
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
        setShipGrid(updatedGrid);
        console.log("Updated grid:", updatedGrid);
        fetchShip();
    };

    return (
        <div style={{ display: 'flex', flexDirection: 'row', height: '100%' }}>
            {/* Left: Ship Details */}
            <ShipDetails ship={ship} />

            {/* Right: Ship Grid */}
            <div style={{ flex: 1, overflow: 'auto' }}>
                <ShipGrid grid={shipGrid} spaceShipId={spaceShipId} onUpdate={handleGridUpdate} />
            </div>
        </div>
    );
};

export default ShipEditorWorkspace;
