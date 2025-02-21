import React, { useState } from 'react';
import { Box } from '@mui/material';
import { GridComponent } from '../../Models/GridComponent';
import ShipComponent from './ShipComponent';

type ShipGridProps = {
    grid: (GridComponent | null)[][]; // Grid of nulls or components
    spaceShipId: string;
    onUpdate: (updatedGrid: (GridComponent | null)[][]) => void;
};

const ShipGrid: React.FC<ShipGridProps> = ({ grid, spaceShipId, onUpdate }) => {

    const [selectedCell, setSelectedCell] = useState<{ row: number; col: number } | null>(null);

    
    const handleCellClick = (rowIndex: number, colIndex: number) => {
        // Toggle the selection state
        if (selectedCell?.row === rowIndex && selectedCell?.col === colIndex) {
            setSelectedCell(null); // Deselect if already selected
        } else {
            setSelectedCell({ row: rowIndex, col: colIndex });
        }

        // Example of updating the grid (future use)
        const updatedGrid = [...grid];
        onUpdate(updatedGrid);
    };

    const handleDrop = async (e: React.DragEvent<HTMLDivElement>, rowIndex: number, colIndex: number) => {
        e.preventDefault();

        // Retrieve the component type from the drag event
        const componentTypeData = e.dataTransfer.getData('component');
        const componentType = JSON.parse(componentTypeData);

        // Check for neighboring components
        const hasNeighbor =
        (grid[rowIndex - 1]?.[colIndex] !== undefined && grid[rowIndex - 1]?.[colIndex] !== null) || // Top
        (grid[rowIndex + 1]?.[colIndex] !== undefined && grid[rowIndex + 1]?.[colIndex] !== null) || // Bottom
        (grid[rowIndex]?.[colIndex - 1] !== undefined && grid[rowIndex]?.[colIndex - 1] !== null) || // Left
        (grid[rowIndex]?.[colIndex + 1] !== undefined && grid[rowIndex]?.[colIndex + 1] !== null);   // Right
    

        if (!hasNeighbor) {
            console.warn('Drop failed: No neighboring components');
            return;
        }

        // Create a new GridComponent
        const newComponent: GridComponent = {
            id: crypto.randomUUID(), // Generate a unique ID
            componentType: componentType,
            componentTypeId: componentType.id,
            spaceShipId: spaceShipId,
            connections: 15,
            powerCouplings: 15,
            properties: '',
            topComponentId: grid[rowIndex - 1]?.[colIndex]?.id,
            bottomComponentId: grid[rowIndex + 1]?.[colIndex]?.id,
            leftComponentId: grid[rowIndex]?.[colIndex - 1]?.id,
            rightComponentId: grid[rowIndex]?.[colIndex + 1]?.id,
            armour: 0,
            structuralIntegrity: 0,
            lifeSupport: 0,
            minPowerDraw: 0,
            maxPowerDraw: 0,
            mass: 0,
            price: 0,
        };

        try {
            const apiBaseUrl = process.env.REACT_APP_API_BASE_URL || 'https://localhost:7151/api/';
            const response = await fetch(`${apiBaseUrl}Shipyard/AddComponent`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newComponent),
            });
    
            if (!response.ok) {
                console.error('Failed to add component:', response.statusText);
                return;
            }
    
            // Get the returned GUID from the response
            const newGuid = await response.text();
    
            // Update the grid with the returned GUID
            const updatedGrid = [...grid];
            updatedGrid[rowIndex][colIndex] = {
                ...newComponent,
                id: newGuid, // Set the returned GUID
            };
    
            updateConnections(updatedGrid, rowIndex, colIndex);
            onUpdate(updatedGrid);
        } catch (error) {
            console.error('Error adding component:', error);
        }
    };

    const handleDragOver = (e: React.DragEvent<HTMLDivElement>) => {
        e.preventDefault(); // Necessary to allow drop
    };

    const updateConnections = (grid: (GridComponent | null)[][], rowIndex: number, colIndex: number) => {
        const component = grid[rowIndex][colIndex];
        if (!component) return;

        // Update top neighbor
        if (grid[rowIndex - 1]?.[colIndex]) {
            component.topComponentId = grid[rowIndex - 1][colIndex]?.id;
            grid[rowIndex - 1][colIndex]!.bottomComponentId = component.id;
        }

        // Update bottom neighbor
        if (grid[rowIndex + 1]?.[colIndex]) {
            component.bottomComponentId = grid[rowIndex + 1][colIndex]?.id;
            grid[rowIndex + 1][colIndex]!.topComponentId = component.id;
        }

        // Update left neighbor
        if (grid[rowIndex]?.[colIndex - 1]) {
            component.leftComponentId = grid[rowIndex][colIndex - 1]?.id;
            grid[rowIndex][colIndex - 1]!.rightComponentId = component.id;
        }

        // Update right neighbor
        if (grid[rowIndex]?.[colIndex + 1]) {
            component.rightComponentId = grid[rowIndex][colIndex + 1]?.id;
            grid[rowIndex][colIndex + 1]!.leftComponentId = component.id;
        }
    };

    const columnCount = grid[0]?.length || 0;
    const rowCount = grid.length;
    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'left', // Center horizontally
                margin: '1.25rem', // Add 20px spacing around the grid
            }}
        >
            <Box
                sx={{
                    display: 'grid',
                    gridTemplateColumns: `repeat(${columnCount}, 33px)`, // Fixed column width
                    gridTemplateRows: `repeat(${rowCount}, 45px)`, // Fixed row height
                    gap: 0, // No gaps between cells
                    width: `${columnCount * 33}px`, // Grid width based on columns
                    height: `${rowCount * 45}px`, // Grid height based on rows
                }}
            >
                {grid.map((row, rowIndex) =>
                    row.map((cell, colIndex) => (
                        <Box
                            key={`${rowIndex}-${colIndex}`}
                            onDrop={(e) => handleDrop(e, rowIndex, colIndex)}
                            onDragOver={handleDragOver}
                            sx={{
                                width: 33,
                                height: 45,
                                border: selectedCell?.row === rowIndex && selectedCell?.col === colIndex
                                    ? '1px solid red'
                                    : '1px solid black', // Toggle border color
                                cursor: 'pointer', // Show pointer cursor on hover
                            }}
                            onClick={() => handleCellClick(rowIndex, colIndex)}
                        >
                             {cell ? <ShipComponent gridComponentType={cell.componentType} cellSelected={selectedCell?.row === rowIndex && selectedCell?.col === colIndex ? true : false} /> : null}
                        </Box>
                    ))
                )}
            </Box>
        </Box>
    );
};

export default ShipGrid;
