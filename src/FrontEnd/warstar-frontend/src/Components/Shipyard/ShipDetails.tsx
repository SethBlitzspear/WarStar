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
            <p><strong>Mass:</strong> {ship.mass}</p>
            <p><strong>BackgroundDraw:</strong> {ship.backgroundPowerDraw}</p>
            <p><strong>Ship Size:</strong> {ship.shipSize}</p>
        </div>
    );
};

export default ShipDetails;
