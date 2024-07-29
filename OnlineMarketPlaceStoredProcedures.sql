
--user management procedures
create procedure CreateUser
	@Username nvarchar(255),
	@Email varchar(255),
	@PasswordHash varbinary(64),
	@Status int,
	@AddressLine1 nvarchar(255),
	@AddressLine2 nvarchar(255),
	@City nvarchar(255),
	@State varchar(255),
	@PostalCode varchar(255),
	@CountryID int
AS
begin 
	insert into Users values(
	@Username,
	@Email,
	@PasswordHash,
	@Status,
	@AddressLine1,
	@AddressLine2,
	@City,
	@State,
	@PostalCode,
	@CountryID
	);
end;


create procedure UpdateUserStatus
	@UserID int,
	@Status int
AS
BEGIN
	update Users set StatusID=@Status where UserID=@UserID;
END;



--item Management 

create procedure CreateItem 
	@SellerID int,
	@CategoryID int,
	@Title nvarchar(255),
	@Description text,
	@StartingPrice decimal(10,2),
	@CurrentPrice decimal(10,2),
	@StartDate date,
	@EndDate date,
	@ImageURL nvarchar(255)
AS
BEGIN
	insert into Items Values(
	@SellerID,
	@CategoryID,
	@Title,
	@Description,
	@StartingPrice,
	@CurrentPrice,
	@StartDate,
	@EndDate,
	@ImageURL
	);
END;


--Bid Management

create procedure PlaceBid
	@ItemID int,
	@UserID int,
	@BidAmount decimal(10,2)
As
BEGIN
	insert into Bids Values(
	@ItemID,
	@UserID,
	@BidAmount
	);
	update Items 
	set CurrentPrice=@BidAmount 
	where ItemID=@ItemID;
END;






