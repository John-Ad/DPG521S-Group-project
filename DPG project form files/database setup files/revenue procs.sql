use DPGTestDB
go

--general proc for getting dates
drop proc if exists sp_getDates
go
create proc sp_getDates(
    @mode AS int=null,
    @start AS date output,
    @end AS date output
)
AS
BEGIN
    if(@mode=1)     --day
    BEGIN
        SET @start=GETDATE()
        SET @end=@start
    END
    if(@mode=2)     --month
    BEGIN
        SET @start=DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1)
        SET @end=DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),DAY(EOMONTH(GETDATE())))
    END
    if(@mode=3)     --year
    BEGIN
        SET @start=DATEFROMPARTS(YEAR(GETDATE()),1,1)
        SET @end=DATEFROMPARTS(YEAR(GETDATE()),12,31)
    END
    if(@mode>3 OR @mode<1)   
    BEGIN
        THROW 50001,'Invalid Parameters',18
        RETURN
    END
END
go
----------------------------------

--gets sales data for top 3 or specific items
drop proc if exists sp_getBestSelling
go
CREATE PROC sp_getBestSelling(
    @mode AS int=NULL,
    @isProd AS bit=NULL,           --indicates whether haircut or product data is required
    @start AS date=NULL,
    @end AS date=NULL,
    @id AS int=0             --if an id is passed, only the data for that specific product will be retrieved
)
AS
BEGIN
    if(@mode>=1 AND @mode<=3)     -- 1=day, 2=month, 3=year
    BEGIN
        EXEC sp_getDates @mode,@start OUTPUT,@end OUTPUT
    END

    if(@mode=4 AND (@start=NULL OR @end=NULL))   --gets most popular haircuts/products for a customer period of time
    BEGIN
        THROW 50001,'For a custom search the start and end date must be specified',18
        RETURN
    END
    if((@mode!>0 AND @mode!<5) OR @isProd=NULL)
    BEGIN
        THROW 50001, 'Invalid parameters!',21
        RETURN
    END
    if(@start>@end)
    BEGIN
        THROW 50001, 'start date must be less than end date!',24
        RETURN
    END
    
    if(@id=0)       --if id > 0 then a specific search is required
    BEGIN
        if(@isProd=0)       --get serveRend data
        BEGIN
                SELECT TOP 3 Service_Rendered.Haircut_ID, Haircut.Haircut_Name, SUM(Service_Rendered.Quantity) AS Quantity, SUM(Service_Rendered.Total_Amount) AS Total_Sales FROM Service_Rendered
                INNER JOIN Haircut ON Service_Rendered.Haircut_ID=Haircut.Haircut_ID
                WHERE Service_Rendered.Date_of_Purchase >= @start AND Service_Rendered.Date_of_Purchase <= @end
                GROUP BY Service_Rendered.Haircut_ID, Haircut_Name
                ORDER BY SUM(Service_Rendered.Quantity) DESC
        END
        else
        BEGIN               --get prodSold data
                SELECT TOP 3 Product_Sold.Product_ID, Product.Product_Name, SUM(Product_Sold.Quantity) AS Quantity, SUM(Product_Sold.Total_Amount) AS Total_Sales FROM Product_Sold
                INNER JOIN Product ON Product_Sold.Product_ID=Product.Product_ID
                WHERE Product_Sold.Date_of_Purchase >= @start AND Product_Sold.Date_of_Purchase <= @end
                GROUP BY Product_Sold.Product_ID, Product_Name
                ORDER BY SUM(Product_Sold.Quantity) DESC
        END
    END
    else
    BEGIN
        if(@isProd=0)       --get serveRend data
        BEGIN
                SELECT Service_Rendered.Haircut_ID, Haircut.Haircut_Name, SUM(Service_Rendered.Quantity) AS Quantity, SUM(Service_Rendered.Total_Amount) AS Total_Sales FROM Service_Rendered
                INNER JOIN Haircut ON Service_Rendered.Haircut_ID=Haircut.Haircut_ID
                WHERE (Service_Rendered.Date_of_Purchase >= @start AND Service_Rendered.Date_of_Purchase <= @end) AND Service_Rendered.Haircut_ID=@id
                GROUP BY Service_Rendered.Haircut_ID, Haircut_Name
        END
        else
        BEGIN               --get prodSold data
                SELECT Product_Sold.Product_ID, Product.Product_Name, SUM(Product_Sold.Quantity) AS Quantity, SUM(Product_Sold.Total_Amount) AS Total_Sales FROM Product_Sold
                INNER JOIN Product ON Product_Sold.Product_ID=Product.Product_ID
                WHERE (Product_Sold.Date_of_Purchase >= @start AND Product_Sold.Date_of_Purchase <= @end) AND Product_Sold.Product_ID=@id
                GROUP BY Product_Sold.Product_ID, Product_Name
        END
    END
END
go
-------------------------------------------------

--gets total sales based on a given time period
drop proc if exists sp_GetTotalSales
go
CREATE PROC sp_GetTotalSales(
    @mode AS int=2,
    @start AS date=null,
    @end AS date=null
)
AS
BEGIN
    if(@mode>=1 AND @mode<=3)     -- 1=day, 2=month, 3=year
    BEGIN
        EXEC sp_getDates @mode,@start OUTPUT,@end OUTPUT
    END
    if(@mode=4 AND (@start=NULL OR @end=NULL))   --gets most popular haircuts/products for a customer period of time
    BEGIN
        THROW 50001,'For a custom search the start and end date must be specified',18
        RETURN
    END
    if(@mode>4)
    BEGIN
        THROW 50001, 'start date must be less than end date!',24
        RETURN
    END
    if(@start>@end)
    BEGIN
        THROW 50001, 'start date must be less than end date!',24
        RETURN
    END

    SELECT SUM(Total_Amount) AS Total_Sales FROM Service_Rendered
    WHERE Service_Rendered.Date_of_Purchase>=@start AND Service_Rendered.Date_of_Purchase<=@end 
    UNION
    SELECT SUM(Total_Amount) FROM Product_Sold
    WHERE Product_Sold.Date_of_Purchase>=@start AND Product_Sold.Date_of_Purchase<=@end 
END
go

--exec sp_GetTotalSales
--exec sp_getBestSelling 3,1
--exec sp_GetBestSelling 4,0,'2020-01-01','2021-01-31',1001

/*
--start > end test
declare @d as date,@e as date
set @d=DATEFROMPARTS(YEAR(getdate()),month(getdate()),'13')
set @e=getdate()
exec sp_getBestSelling 4,1,@d,@e
-------------------------

exec sp_getBestSelling 1,0
SELECT * from Service_Rendered
SELECT * from Product_Sold

-- random testing
declare @d as date
--set @d=DATEFROMPARTS(YEAR(getdate()),month(getdate()),'13')
SET @d=DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),DAY(EOMONTH(GETDATE())))
--set @d=DATEFROMPARTS(2020,10,12)
select @d
*/

/*

*/


select * from Haircut
select * from Service_Rendered
select * from Product
select * from Product_Sold

/*
drop proc if exists sp_GetSpecifItemSales
go
CREATE PROC sp_GetSpecifItemSales(
    @isProd AS bit,
    @id AS int
)
AS
BEGIN
    if(@isProd=0)
    BEGIN
        SELECT Haircut_ID, SUM(Quantity)
    END
END
*/













