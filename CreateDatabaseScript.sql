CREATE DATABASE HotelBooking -- maybe a bit too much? presume that db exists, drop and recreate tables?
-- create tables below

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