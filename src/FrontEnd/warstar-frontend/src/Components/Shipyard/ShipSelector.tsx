import { useState, useEffect, useCallback } from "react";
import { Box, Typography, List, ListItemButton, ListItemText } from "@mui/material";
import axios from "axios";
import { Ship } from "../../Models/Ship";
import { UserModel } from "../../Models/UserModel";

interface ShipSelectorProps {
  loggedInUser: UserModel | null;
  onShipSelect: (shipId: string) => void;
}

function ShipSelector({ loggedInUser, onShipSelect }: ShipSelectorProps) {
  const [ships, setShips] = useState<Ship[]>([]);
  const apiBaseUrl = process.env.REACT_APP_SPACESHIP_API_BASE_URL;

  // Memoize fetchShips to prevent re-creating the function on every render
  // This means it will only be re-created when the dependencies change
  // Without this it would be recreated on every render and the effect would run on every render
  const fetchShips = useCallback(async () => {
      if (!loggedInUser) return;

      try {
        const response = await axios.get(`${apiBaseUrl}ShipYard/GetOwnerSpaceShips/${loggedInUser.userId}`, {
          headers: {
            Authorization: `Bearer ${loggedInUser.token}`,
          },
        });
        setShips(response.data);
      } catch (error) {
        console.error("Error fetching ships:", error);
      }
  }, [loggedInUser, apiBaseUrl]); 

  useEffect(() => {
      fetchShips();
  }, [fetchShips]); 

  return (
    <Box sx={{ width: "25%", display: "flex", flexDirection: "column", paddingRight: 2 }}>
      <Typography variant="h6" sx={{ marginBottom: 1 }}>Ship List</Typography>
      <List sx={{ flex: 1, overflowY: "auto" }}>
        {ships.map((ship) => (
          <ListItemButton key={ship.id} onClick={() => onShipSelect(ship.id)}>
            <ListItemText primary={ship.name} />
          </ListItemButton>
        ))}
      </List>
    </Box>
  );
}

export default ShipSelector;
