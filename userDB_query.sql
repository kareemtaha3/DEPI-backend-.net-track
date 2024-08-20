create table Users(
UserID int primary key,
Username nvarchar(50),
Email nvarchar(100),
PasswordHash nvarchar(255),
[Status] nvarchar(50),
AddressLine1 nvarchar(100),
AddressLine2 nvarchar(100),
City nvarchar(50),
[State] nvarchar(50),
PostalCode nvarchar(20),
CountryID int,
CreatedAt datetime
);