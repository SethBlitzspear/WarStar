-- Generate a random OwnerId for the spaceship
DECLARE @OwnerId UNIQUEIDENTIFIER = NEWID();

-- Insert the spaceship
INSERT INTO [dbo].[SpaceShips] ([Id], [Name], [OwnerId])
VALUES (NEWID(), 'The Blitzspear', @OwnerId);

-- Retrieve the Id of the inserted spaceship
DECLARE @SpaceShipId UNIQUEIDENTIFIER = (SELECT TOP 1 [Id] FROM [dbo].[SpaceShips] WHERE [Name] = 'The Blitzspear');

-- Generate component Ids
DECLARE @KeelId UNIQUEIDENTIFIER = NEWID();
DECLARE @WeaponLeftId UNIQUEIDENTIFIER = NEWID();
DECLARE @WeaponTopId UNIQUEIDENTIFIER = NEWID();
DECLARE @WeaponBottomId UNIQUEIDENTIFIER = NEWID();
DECLARE @ReactorId UNIQUEIDENTIFIER = NEWID();
DECLARE @EngineId UNIQUEIDENTIFIER = NEWID();

-- Insert components without relationships first
INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@KeelId, '96ea7006-3195-4ebb-882a-1823858bb161', NULL, @SpaceShipId, 16, 16);

INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@WeaponLeftId, '89abef19-f35d-4aa7-ba0d-fff02967069c', NULL, @SpaceShipId, 16, 16);

INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@WeaponTopId, '89abef19-f35d-4aa7-ba0d-fff02967069c', NULL, @SpaceShipId, 16, 16);

INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@WeaponBottomId, '89abef19-f35d-4aa7-ba0d-fff02967069c', NULL, @SpaceShipId, 16, 16);

INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@ReactorId, '06cea7a9-90ef-4cfe-be6d-fe6be63583d1', NULL, @SpaceShipId, 16, 16);

INSERT INTO [dbo].[Components] ([Id], [ComponentTypeId], [Properties], [SpaceShipId], [Connections], [PowerCouplings])
VALUES (@EngineId, 'a46766a0-0a4f-4314-a2b8-0af36163fec9', NULL, @SpaceShipId, 16, 16);

-- Update the relationships between components
UPDATE [dbo].[Components]
SET [TopComponentId] = @WeaponTopId,
    [BottomComponentId] = @WeaponBottomId,
    [LeftComponentId] = @WeaponLeftId,
    [RightComponentId] = @ReactorId
WHERE [Id] = @KeelId;

UPDATE [dbo].[Components]
SET [RightComponentId] = @KeelId
WHERE [Id] = @WeaponLeftId;

UPDATE [dbo].[Components]
SET [BottomComponentId] = @KeelId
WHERE [Id] = @WeaponTopId;

UPDATE [dbo].[Components]
SET [TopComponentId] = @KeelId
WHERE [Id] = @WeaponBottomId;

UPDATE [dbo].[Components]
SET [LeftComponentId] = @KeelId,
    [RightComponentId] = @EngineId
WHERE [Id] = @ReactorId;

UPDATE [dbo].[Components]
SET [LeftComponentId] = @ReactorId
WHERE [Id] = @EngineId;
