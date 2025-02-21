import React from "react";
import { Ship } from "../../Models/Ship";

type ShipDetailsProps = {
    ship: Ship | null;
};

const ShipDetails: React.FC<ShipDetailsProps> = ({ ship }) => {
    if (!ship) {
        return <p>Loading ship details...</p>;
    }

    return (
        <div style={{ width: "250px", padding: "10px", backgroundColor: "#f4f4f4", borderRight: "1px solid #ccc" }}>
            <h3>Ship Details</h3>
            <p><strong>Name:</strong> {ship.name}</p>
            <p><strong>ID:</strong> {ship.id}</p>
            <p><strong>Owner:</strong> {ship.ownerId}</p>
            <p><strong>Grid Size:</strong> {ship.shipLayout?.length} x {ship.shipLayout?.[0]?.length || 0}</p>
        </div>
    );
};

export default ShipDetails;
