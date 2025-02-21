import React from 'react';
import { Box, Typography } from '@mui/material';
import { GridComponentType } from '../../Models/GridComponentType';

interface ComponentTypeDetailsProps {
    selectedComponent: GridComponentType | null;
}

const ComponentTypeDetails: React.FC<ComponentTypeDetailsProps> = ({ selectedComponent }) => {
    if (!selectedComponent) {
        return (
            <Box sx={{ border: '1px solid black', padding: '10px', width: '400px' }}>
                <Typography variant="h6" sx={{ backgroundColor: '#f0f0f0', padding: '5px' }}>
                    Component Details
                </Typography>
                <Typography>No component selected</Typography>
            </Box>
        );
    }

    return (
        <Box
            sx={{
                border: '0.1rem solid black',
                padding: '0.6rem',
                display: 'flex',
                flexDirection: 'row', // Ensures row layout (Header + Content side by side)
                gap: '0.4rem',
                height: '100%', // Ensure the container takes full available height
                alignItems: 'stretch', // Ensures all children stretch to same height
            }}
        >
            {/* Header (Stretch vertically to match content) */}
            <Box
                sx={{
                    display: 'flex',
                    alignItems: 'center', // Centers text vertically
                    justifyContent: 'center', // Centers text horizontally
                    backgroundColor: '#f0d0d0',
                    padding: '0.3rem',
                    textAlign: 'center',
                    border: '0.0625rem solid #ccc',
                    flex: 1, // Makes this box take equal vertical space as the others
                    minWidth: '10rem', // Optional, ensures header has a reasonable width
                }}
            >
                <Typography
                    sx={{
                        fontSize: "1.2rem",
                        display: 'flex',
                        alignItems: 'center', // Centers text inside Typography
                        justifyContent: 'center', // Centers text horizontally
                        width: '100%',
                        whiteSpace: 'wrap',
                    }}
                >
                    {selectedComponent.name}
                </Typography>
            </Box>
    
            {/* Content (Columns) */}
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    gap: '1rem',
                    flexGrow: 2, // Allows content to take more space than the header
                    alignItems: 'stretch', // Ensures columns stretch to same height as header
                }}
            >
                {/* Column 1 */}
                <Box
                    sx={{
                        flex: 1,
                        display: 'flex',
                        flexDirection: 'column',
                        gap: '0.5rem',
                    }}
                >
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Price:</strong> {selectedComponent.price}</Typography>
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Armour:</strong> {selectedComponent.armour}</Typography>
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Structural Integrity:</strong> {selectedComponent.structuralIntegrity}</Typography>
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Mass:</strong> {selectedComponent.mass}</Typography>
                </Box>
    
                {/* Column 2 */}
                <Box
                    sx={{
                        flex: 1,
                        display: 'flex',
                        flexDirection: 'column',
                        gap: '0.5rem',
                    }}
                >
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Life Support:</strong> {selectedComponent.lifeSupport || 'N/A'}</Typography>
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Min Power:</strong> {selectedComponent.minPowerDraw}</Typography>
                    <Typography sx={{ fontSize: "0.8rem", whiteSpace: 'nowrap' }}><strong>Max Power:</strong> {selectedComponent.maxPowerDraw}</Typography>
                </Box>
            </Box>
        </Box>
    );
    
}
export default ComponentTypeDetails;
