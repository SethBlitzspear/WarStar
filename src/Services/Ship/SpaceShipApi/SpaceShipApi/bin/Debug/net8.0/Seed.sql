-- Declare IDs for components and spaceship
DECLARE @blitzspearId UNIQUEIDENTIFIER = NEWID();
DECLARE @keelComponentId UNIQUEIDENTIFIER = NEWID();
DECLARE @engineComponentId UNIQUEIDENTIFIER = NEWID();
DECLARE @reactorComponentId UNIQUEIDENTIFIER = NEWID();
DECLARE @rightLaserComponentId UNIQUEIDENTIFIER = NEWID();
DECLARE @topLaserComponentId UNIQUEIDENTIFIER = NEWID();
DECLARE @leftLaserComponentId UNIQUEIDENTIFIER = NEWID();

-- Insert the spaceship
INSERT INTO SpaceShips (Id, Name, OwnerId)
VALUES (@blitzspearId, 'The Blitzspear', '6BC73FFE-6BD4-4BEF-0C86-08DCEF8D0DA3');

-- Insert the components without foreign key references
INSERT INTO Components (Id, ComponentTypeId, SpaceShipId, Connections, PowerCouplings, Armour, StructuralIntegrity, LifeSupport, MinPowerDraw,MaxPowerDraw, Mass, Price, Properties )
VALUES
    (@keelComponentId, '96EA7006-3195-4EBB-882A-1823858BB161', @blitzspearId, 15, 15, 10, 100, 0, 0, 0, 100, 12000, ''),  -- Keel
    (@engineComponentId, 'A46766A0-0A4F-4314-A2B8-0AF36163FEC9', @blitzspearId, 15, 15, 2, 30, 1, 10, 1000, 200, 5000, ''),  -- Engine
    (@reactorComponentId, '06CEA7A9-90EF-4CFE-BE6D-FE6BE63583D1', @blitzspearId, 15, 15, 1, 10, 1, 0, 1, 250, 7000, ''),  -- Reactor
    (@rightLaserComponentId, '89ABEF19-F35D-4AA7-BA0D-FFF02967069C', @blitzspearId, 15, 15, 2, 20, 0, 0, 100, 50, 2000, ''),  -- Weapon
    (@topLaserComponentId, '89ABEF19-F35D-4AA7-BA0D-FFF02967069C', @blitzspearId, 15, 15, 2, 20, 0, 0, 100, 50, 2000, ''),  -- Weapon
    (@leftLaserComponentId, '89ABEF19-F35D-4AA7-BA0D-FFF02967069C', @blitzspearId, 15, 15, 2, 20, 0, 0, 100, 50, 2000, '');  -- Weapon

-- Update components to set foreign key relationships
UPDATE Components
SET TopComponentId = @topLaserComponentId,
    LeftComponentId = @leftLaserComponentId,
    RightComponentId = @rightLaserComponentId,
    BottomComponentId = @reactorComponentId
WHERE Id = @keelComponentId;

UPDATE Components
SET TopComponentId = @keelComponentId,
    BottomComponentId = @engineComponentId
WHERE Id = @reactorComponentId;

UPDATE Components
SET TopComponentId = @reactorComponentId
WHERE Id = @engineComponentId;

UPDATE Components
SET LeftComponentId = @keelComponentId
WHERE Id = @rightLaserComponentId;

UPDATE Components
SET BottomComponentId = @keelComponentId
WHERE Id = @topLaserComponentId;

UPDATE Components
SET RightComponentId = @keelComponentId
WHERE Id = @leftLaserComponentId;
