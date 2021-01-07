use DPGtestDB
go

/*
drop proc if exists sp_updateHaircut
go
CREATE PROC sp_updateHaircut(
    @id AS int,
    @hName AS varchar(20),
    @hdesc AS varchar(100),
    @hPrice AS decimal(19,2)
)
AS
BEGIN
    BEGIN TRAN
        if(@id IN (SELECT Haircut_ID FROM Haircut) AND @hName!='' AND @hPrice>0)
        BEGIN
            UPDATE Haircut
            SET Haircut_Name=@hName, Haircut_Desc=@hdesc, Price=@hPrice
            WHERE Haircut_ID=@id
            COMMIT
        END
        else
        BEGIN
            THROW 50001, 'Failed to update record: invalid field/s',15
        END
END
go
exec sp_updateHaircut 1001,'fade','tapered cut',24
select * from Haircut

drop proc if exists sp_updateProduct
go
CREATE PROC sp_updateProduct(
    @id AS int,
    @pName AS varchar(20),
    @pdesc AS varchar(100),
    @pPrice AS decimal(19,2),
    @pQuantity AS int
)
AS
BEGIN
    BEGIN TRAN
        if(@id IN (SELECT Product_ID FROM Product) AND @pName!='' AND @pPrice>0 AND @pQuantity>=0)
        BEGIN
            UPDATE Product
            SET Product_Name=@pName, Product_Desc=@pdesc, Price=@pPrice, Quantity=@pQuantity
            WHERE Product_ID=@id
            COMMIT
        END
        else
        BEGIN
            THROW 50001, 'Failed to update record: invalid field/s',2
        END
END
go
exec sp_updateProduct 2001,'comb','standard comb',45,31
select * from Product

drop proc if exists sp_updateCustomer
go
CREATE PROC sp_updateCustomer(
    @id AS int,
    @hID AS int,
    @cName AS varchar(20),
    @cNum AS varchar(10),
    @numOfVis AS int
)
AS
BEGIN
    BEGIN TRAN
        if(@id IN (SELECT Customer_ID FROM Customer) AND @cName!='' AND @numOfVis>0 AND (@cNum LIKE '081_______%' OR @cNum LIKE '085_______%'))
        BEGIN
            if(@hID>0 AND @hID IN (SELECT Haircut_ID FROM Haircut))
            BEGIN
                UPDATE Customer
                SET Haircut_ID=@hID, Customer_Name=@cName, Cell_Number=@cNum, Num_of_Visits=@numOfVis
                WHERE Customer_ID=@id
                COMMIT
            END
            else
            BEGIN
                UPDATE Customer
                SET Haircut_ID=null, Customer_Name=@cName, Cell_Number=@cNum, Num_of_Visits=@numOfVis
                WHERE Customer_ID=@id
                COMMIT
            END
        END
        else
        BEGIN
            THROW 50001, 'Failed to update record: invalid field/s',15
        END
END
go
--exec sp_updateCustomer 300001, 1001, 'john doe', '0812111328', 20
select * from Customer
go
*/