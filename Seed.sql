USE HotelBooking

CREATE TABLE RoomType (
    [Id] int NOT NULL PRIMARY KEY,
    [Name] varchar(255)
)

CREATE TABLE Room (
    [Id] int NOT NULL PRIMARY KEY,
    [RoomTypeId] int FOREIGN KEY REFERENCES RoomType(Id),
    [PricePerDay] float(10),
    [Status] varchar(255),
    [Available] bit
);

DELETE FROM Room;
DELETE FROM RoomType;

INSERT INTO RoomType([Id], [Name]) 
VALUES 
    (1, 'Basic'), 
    (2, 'Standard'), 
    (3, 'Deluxe'), 
    (4, 'Penthouse');

INSERT INTO Room(Id, RoomTypeId, PricePerDay, [Status], Available) VALUES 
    (1, 1, 12.5, 'A bit dusty', 1),
    (2, 4, 299.99, 'Very nice!', 1),
    (3, 2, 150, 'No comments', 0),
    (4, 3, 100, 'Might catch on fire, honostly a bit of a gamble ', 0),
    (5, 2, 160, 'Quite nice', 1);