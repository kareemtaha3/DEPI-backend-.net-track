
create table Countries (
CountryID int identity(1,1) primary key,
CountryName nvarchar(255) Not null
);


create table Categories(
CategoryID int identity(1,1) primary key,
CategoryName nvarchar(255) not null,
[Description] text
);

create table StatusType(
StatusID int primary key,
StatusName varchar(255) not null
);




create table Users(
UserID int identity(1,1) primary key,
Username nvarchar(255) not null,
email varchar(255) not null,
PasswordHash varbinary(64) not null,
StatusID int not null, --(Active,Terminated,Inactive)
AddressLine1 nvarchar(255) NOT null,
AddressLine2 nvarchar(255) not null,
City nvarchar(255) not null,
[State] varchar(255) not null,
PostalCode varchar(255) not null,
CountryID int not null,
CreatedAt datetime not null default getdate()
foreign key (CountryID) references Countries (CountryID) on delete cascade on update cascade,
foreign key (StatusID) references StatusType (StatusID) on delete cascade on update cascade
);



create table Items(
ItemID int identity(1,1) primary key,
SellerID int not null,
CategoryID int not null,
Title nvarchar(255) not null,
[Description] text not null,
StartingPrice decimal(10,2) not null,
CurrentPrice decimal(10,2) not null,
StartDate Date not null,
EndDate Date not null,
ImageURL nvarchar(255) not null,
CreatedAt datetime not null Default getdate(),
foreign key (SellerID) references Users ( UserID) on delete cascade on update cascade,
foreign key (CategoryID) references Categories (CategoryID) on delete cascade on update cascade
);



create table Bids(
BidID int identity(1,1) primary key,
ItemID int not null,
UserID int not null,
BidAmount decimal(10,2) not null,
BidTime datetime not null Default getdate(),
foreign key (ItemID) references Items(ItemID) on delete no action on update no action,
foreign key (UserID) references Users(UserID) on delete no action on update no action
);


create table Orders(
OrderID int identity(1,1) primary key,
BuyerID int not null,
ItemID int not null,
OrderDate date not null,
TotalAmount int not null,
foreign key (BuyerID) references Users(UserID) on delete no action on update no action,
foreign key (ItemID) references Items(ItemID) on delete no action on update no action
);





create table Notifications(
NotificationID int identity(1,1) primary key,
UserID int not null,
[Message] ntext not null,
IsRead binary not null,
CreatedAt datetime not null default getdate(),
foreign key (UserID) references Users(UserID) on delete cascade on update cascade
);

