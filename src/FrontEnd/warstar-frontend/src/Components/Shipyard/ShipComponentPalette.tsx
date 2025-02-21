import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Box } from '@mui/material';
import ShipComponent from './ShipComponent';
import ComponentTypeDetails from './ComponentTypeDetails'; // New component
import { GridComponentType } from '../../Models/GridComponentType';

const ShipComponentPalette: React.FC = () => {
    const [componentTypes, setComponentTypes] = useState<GridComponentType[]>([]);
    const [selectedComponent, setSelectedComponent] = useState<GridComponentType | null>(null);
    const apiBaseUrl = process.env.REACT_APP_API_BASE_URL || 'https://localhost:7151/api/';

    // Fetch component types from the API
    useEffect(() => {
        const fetchComponentTypes = async () => {
            try {
                const response = await axios.get<GridComponentType[]>(`${apiBaseUrl}Shipyard/GetComponentTypes`);
                setComponentTypes(response.data);
            } catch (error) {
                console.error('Error fetching component types:', error);
            }
        };

        fetchComponentTypes();
    }, [apiBaseUrl]);

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'row',
                alignItems: 'center', // Centers the entire palette bar vertically
                gap: '0.8rem',
                padding: '0.8rem',
                height: '100%',
            }}
        >
            {/* Component Bar */}
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    gap: '0.25rem',
                    border: '0.1rem solid black',
                    padding: '0.25rem',
                }}
            >
                {componentTypes.map((type) => (
                    <Box
                        key={type.id}
                        draggable
                        onClick={() => setSelectedComponent(type)} // Set selected component on click
                        onDragStart={(e) => {
                            e.dataTransfer.setData('component', JSON.stringify(type));
                        }}
                        sx={{
                            border: selectedComponent?.id === type.id ? '0.2rem solid blue' : '0.1rem solid black',
                            padding: '0.25rem',
                            cursor: 'pointer',
                        }}
                    >
                        <ShipComponent gridComponentType={type} cellSelected={selectedComponent?.id === type.id} />
                    </Box>
                ))}
            </Box>

            {/* Component Details */}
            <ComponentTypeDetails selectedComponent={selectedComponent} />
        </Box>
    );
};

export default ShipComponentPalette;
