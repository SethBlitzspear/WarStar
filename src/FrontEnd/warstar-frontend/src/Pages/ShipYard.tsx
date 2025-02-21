import React, { useState } from 'react';
import { Box, Divider, Typography } from '@mui/material';
import { UserModel } from '../Models/UserModel';
import { BasePage } from './BasePage';
import ShipEditor from '../Components/Shipyard/ShipEditor';
import ShipSelector from '../Components/Shipyard/ShipSelector';

interface ShipYardProps {
    loggedInUser: UserModel | null;
}

const ShipYard: BasePage<ShipYardProps> = ({ loggedInUser }) => {
    const [selectedShipId, setSelectedShipId] = useState<string | null>(null);

    return (
        <Box sx={{ padding: 3, height: "100%", display: "flex", flexDirection: "row" }}>
            <ShipSelector loggedInUser={loggedInUser} onShipSelect={setSelectedShipId} />

            <Divider orientation="vertical" flexItem />

            <Box sx={{ flex: 1, paddingLeft: 2 }}>
                {selectedShipId ? (
                    <ShipEditor spaceShipId={selectedShipId} loggedInUser={loggedInUser} />
                ) : (
                    <Typography>Select a ship to edit</Typography>
                )}
            </Box>
        </Box>
    );
};

ShipYard.displayName = "ShipYard";
export default ShipYard;
