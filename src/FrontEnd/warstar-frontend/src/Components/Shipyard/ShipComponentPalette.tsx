import React, { useEffect, useState } from "react";
import axios from "axios";
import { Box } from "@mui/material";
import ShipComponent from "./ShipComponent";
import { GridComponentType } from "../../Models/GridComponentType";

type ShipComponentPaletteProps = {
    setSelectedComponent: (component: GridComponentType | null) => void;
    selectedComponent: GridComponentType | null;
};

const ShipComponentPalette: React.FC<ShipComponentPaletteProps> = ({ setSelectedComponent, selectedComponent }) => {
    const [componentTypes, setComponentTypes] = useState<GridComponentType[]>([]);
    const apiBaseUrl = process.env.REACT_APP_API_BASE_URL || "https://localhost:7151/api/";

    // Fetch component types from the API
    useEffect(() => {
        const fetchComponentTypes = async () => {
            try {
                const response = await axios.get<GridComponentType[]>(`${apiBaseUrl}Shipyard/GetComponentTypes`);
                setComponentTypes(response.data);
            } catch (error) {
                console.error("Error fetching component types:", error);
            }
        };

        fetchComponentTypes();
    }, [apiBaseUrl]);

    return (
            <Box sx={{ display: "flex", flexDirection: "row", gap: "0.25rem", border: "0.1rem solid black", padding: "0.25rem", height: "auto" }}>
                {componentTypes.map((type) => (
                    <Box
                        key={type.id}
                        draggable
                        onClick={() => setSelectedComponent(type)} // Set selected component on click
                        onDragStart={(e) => {
                            e.dataTransfer.setData("component", JSON.stringify(type));
                        }}
                        sx={{
                            border: "0.1rem solid black",
                            padding: "0.25rem",
                            cursor: "pointer",
                        }}
                    >
                        <ShipComponent
                            gridComponentType={type}
                            cellSelected={selectedComponent?.id === type.id} // ✅ Pass the selection state
                        />
                    </Box>
                ))}
            </Box>
    );
};

export default ShipComponentPalette;
