import React from 'react';
import { GridComponentType } from '../../Models/GridComponentType';

import LaserIcon from '../../icons/ShipComponentTypes/Laser.png';
import EngineIcon from '../../icons/ShipComponentTypes/Engine.png';
import ReactorIcon from '../../icons/ShipComponentTypes/Reactor.png';
import KeelIcon from '../../icons/ShipComponentTypes/Keel.png';
import DefaultIcon from '../../icons/ShipComponentTypes/Default.png';

const iconMap: Record<string, string> = {
    laser: LaserIcon,
    engine: EngineIcon,
    reactor: ReactorIcon,
    keel: KeelIcon,
};

type ShipComponentProps = { gridComponentType: GridComponentType, cellSelected : boolean };

const ShipComponent: React.FC<ShipComponentProps> = ({ gridComponentType, cellSelected }) => {
    const getIconForComponent = (name: string): string => {
        const lowerName = name.toLowerCase();
        console.log(`Resolving icon for: ${lowerName}, resolved to: ${iconMap[lowerName] || DefaultIcon}`);
        return iconMap[lowerName] || DefaultIcon;
    };

    return (
        <div
            title={gridComponentType.name}
            style={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                width: 30,
                border: '1px solid black',
                backgroundColor: 'white',
            }}
        >
            {/* Image container with bottom border */}
            <div
                style={{
                    width: '100%',
                    borderBottom: '1px solid black',
                    display: 'flex',
                    justifyContent: 'center',
                }}
            >
                <img
                    src={getIconForComponent(gridComponentType.type)}
                    alt={gridComponentType.type}
                    style={{
                        width: 30,
                        height: 30,
                    }}
                />
            </div>

            {/* Text container */}
            <div
                style={{
                    fontSize: 8,
                    textAlign: 'center',
                    backgroundColor:cellSelected? 'lightpink' : 'white',
                    width: '100%',
                }}
            >
                {gridComponentType.shortName}
            </div>
        </div>
    );
};


export default ShipComponent;
