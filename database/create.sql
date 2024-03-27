CREATE DATABASE Weather;
USE Weather;

CREATE TABLE Users(
ID int primary key not null auto_increment,
Login varchar(55),
Password varchar(55)
);

CREATE TABLE Locations(
ID int primary key not null auto_increment,
Name varchar(55),
UserId int,
Latitude decimal,
Longtitude decimal,
foreign key (UserId) REFERENCES  Users(ID) ON DELETE CASCADE
);

CREATE TABLE Sessions(
ID varchar(55) primary key not null,
UserId int,
ExpiresAt datetime,
foreign key (UserId) REFERENCES  Users(ID) ON DELETE CASCADE
);