import React, { useState } from "react";
import { Box } from "@mui/material";
import ShipComponentPalette from "./ShipComponentPalette";
import ComponentTypeDetails from "./ComponentTypeDetails";
import { GridComponentType } from "../../Models/GridComponentType";

const ShipEditorTools: React.FC = () => {
    const [selectedComponent, setSelectedComponent] = useState<GridComponentType | null>(null);

    return (
        <Box sx={{ display: "flex", flexDirection: "row", width: "100%", padding: "0.8rem", gap: "0.25rem" }}>
            {/* Palette - Lets user pick a component */}
            <Box sx={{ alignSelf: "top" }}>
                <ShipComponentPalette
                    setSelectedComponent={setSelectedComponent}
                    selectedComponent={selectedComponent} // ✅ Pass selected component
                />
            </Box>
            {/* Details - Shows details of selected component */}
            <ComponentTypeDetails selectedComponent={selectedComponent} />
        </Box>
    );
};

export default ShipEditorTools;
