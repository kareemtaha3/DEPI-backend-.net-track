

--retrieve all items along with their respective seller information
select ItemID,CategoryID,Title,[Description],StartingPrice,CurrentPrice,StartDate,EndDate,
Username,email,StatusID,AddressLine1,AddressLine2,City,[State],PostalCode,CountryID
from Items it
inner join Users us on
it.SellerID=us.UserID;


--retrieve all users along with their items, if they have any.
select * from Users us
left join Orders ord on us.UserID=ord.BuyerID
left join Items it on ord.ItemID=it.ItemID order by UserID;

--Retrieve all users and their items, showing all users and all items, even if there is nomatch
select * from Users us
full join Orders ord on us.UserID=ord.BuyerID
full join Items it on ord.ItemID=it.ItemID ;

--Retrieve items with the number of bids each item has received.

select ItemID,count(*) as NumberOfBids from Bids
group by ItemID;

--Retrieve users and the total amount they have spent on orders.

select BuyerID, sum(TotalAmount) as TotalSpendsOnOrders from Orders
group by BuyerID;

--Retrieve items along with their category names.

select it.ItemID,it.Title,cat.CategoryName from Items it
join Categories cat on it.CategoryID = cat.CategoryID;