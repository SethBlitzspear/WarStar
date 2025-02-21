import React from "react";
import ShipEditorTools from "./ShipEditorTools";
import ShipEditorWorkspace from "./ShipEditorWorkspace";
import { UserModel } from "../../Models/UserModel";

type ShipEditorProps = {
    spaceShipId: string;
    loggedInUser: UserModel | null;
};

const ShipEditor: React.FC<ShipEditorProps> = ({ spaceShipId, loggedInUser }) => {
    return (
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            
            {/* Tools Section */}
            <ShipEditorTools />

            {/* Ship Editing Section */}
            <ShipEditorWorkspace spaceShipId={spaceShipId} loggedInUser={loggedInUser} />
        </div>
    );
};

export default ShipEditor;
