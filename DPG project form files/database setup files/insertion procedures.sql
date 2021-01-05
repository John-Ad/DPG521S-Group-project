use DPGtestDB
go

/*
drop proc if exists sp_HaircutInsert
delete Haircut
dbcc checkident(Haircut,reseed,1)
go
*/

--Haircut insert

/*
CREATE PROC sp_HaircutInsert(
    @hName AS varchar(20),
    @hDesc AS varchar(100),
    @hPrice AS money
)
AS
BEGIN
    BEGIN TRAN
        if (@hName != '' AND @hPrice > 0)
            BEGIN
                INSERT INTO Haircut VALUES(@hName,@hDesc,@hPrice);
                COMMIT
            END
        else
            BEGIN
                PRINT @hName
                PRINT 'Insertion into Haircut table failed';
                ROLLBACK
            END
END


exec sp_HaircutInsert 'fade','hair tapers off',30
exec sp_HaircutInsert 'school','school cut',10
exec sp_HaircutInsert 'mohawk','large spikes in center',200

*/

--Product insert
--drop proc if exists sp_ProdInsert
--go
/*
CREATE PROC sp_ProdInsert(
    @pName AS varchar(20),
    @pDesc AS varchar(100),
    @pPrice AS money,
    @pQuantity AS int
)
AS
BEGIN
    BEGIN TRAN
        if(@pName != '' AND @pPrice > 0 AND @pQuantity >= 0)
            BEGIN
                INSERT INTO Product VALUES(@pName,@pDesc,@pPrice,@pQuantity)
                COMMIT
            END
        else
            BEGIN
                PRINT 'Insertion in Products table failed'
                ROLLBACK
            END
END

exec sp_ProdInsert 'jdf','sdfjh',11.11,12
select * from Product
delete Product;
dbcc checkident(Product,reseed,0)
*/

--Customer insertion
/*
drop proc if exists sp_CustInsert
go
CREATE PROC sp_CustInsert(
    @hID AS int,
    @cName AS varchar(20),
    @cCell AS int,
    @cNumVisits AS int
)
AS
BEGIN
    BEGIN TRAN
        if(@cName != '' AND @cNumVisits >= 0)
            BEGIN
                if(@hID > 0 AND @hID IN (SELECT Haircut_ID FROM Haircut))
                    BEGIN
                        INSERT INTO Customer VALUES(@hID,@cName,@cCell,@cNumVisits)
                    END
                else
                    BEGIN
                        INSERT INTO Customer VALUES(NULL,@cName,@cCell,@cNumVisits)
                    END
                COMMIT
            END
        else
            BEGIN
                PRINT 'insertion into customer table failed'
            END
END
--exec sp_CustInsert NULL,'jim',0812111340,1
--position insert function
*/
/*
CREATE PROC sp_PosInsert(
    @pName AS varchar(20),
    @pDesc AS varchar(100)
)
AS
BEGIN
    BEGIN TRAN
        if(@pName != '')
            BEGIN
                INSERT INTO Position VALUES(@pName,@pDesc)
                COMMIT
            END
        else
            BEGIN
                ROLLBACK
            END
END
--exec sp_PosInsert 'manager',NULL

--Employee insert function
CREATE PROC sp_EmpInsert(
    @ePosID AS int,
    @eName AS varchar(20),
    @eAge AS int,
    @eCell AS int 
)
AS
BEGIN                       --test this before continueing
    BEGIN TRAN
        if(@ePosID NOT IN(SELECT Position_ID FROM Position) OR @eName='' OR @eCell<810000000)
            BEGIN
                PRINT 'Failed to insert into emp table'
                ROLLBACK
            END
        else
            BEGIN
                PRINT 'Succeeded in inserting into emp table'
                INSERT INTO Employee VALUES(@ePosID,@eName,@eAge,@eCell)
                COMMIT
            END
END
go
--exec sp_EmpInsert 1,'james',33,812111328
*/

/*
--equipment insert function
drop proc if exists sp_EquipInsert
go
CREATE PROC sp_EquipInsert(
    @eEmpID AS int,
    @eName AS varchar(20),
    @eDesc AS varchar(100),
    @eValue AS money
)
AS
BEGIN
    BEGIN TRAN
        if(@eEmpID NOT IN(SELECT Employee_ID FROM Employee) OR @eName='' OR @eValue <= 0)
            BEGIN
                PRINT 'FAILED'
            END
        else
            BEGIN
                INSERT INTO Equipment VALUES(@eEmpID,@eName,@eDesc,GETDATE(),@eValue)
                COMMIT
            END
END
go
exec sp_EquipInsert 1,'razor','used for trimming',100
go

--payroll insert function
drop proc if exists sp_PayrollInsert
go
CREATE PROC sp_PayrollInsert(
    @empID AS INT,
    @taxNum AS INT,
    @hrsWorked AS INT,
    @renumPerHr AS INT
)
AS
BEGIN
    BEGIN TRAN
        if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR @taxNum<1 OR @hrsWorked<0 OR @renumPerHr<0)
            BEGIN
                PRINT 'FAILED'
            END
        else 
            BEGIN
                INSERT INTO Payroll VALUES(@empID,@taxNum,@hrsWorked,@renumPerHr)
                COMMIT
            END
END
go
exec sp_PayrollInsert 1,2392482,22,33
*/

/*
--Service_Rendered insert function
drop proc if exists sp_ServRendInsert
go
CREATE PROC sp_ServRendInsert(
    @empID AS INT,
    @custID AS INT,
    @hairID AS INT,
    @custName AS varchar(20),
    @total AS money
)
AS
BEGIN
    BEGIN TRAN
    if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR @custID NOT IN(SELECT Customer_ID FROM Customer) OR @hairID NOT IN(SELECT Haircut_ID FROM Haircut) OR @custName='' OR @total<0)
        BEGIN
            PRINT 'Failed to insert into ServiceRendered table'
            ROLLBACK
        END
    else 
        BEGIN 
            PRINT 'Successfully inserted into ServiceRendered table'
            INSERT INTO Service_Rendered VALUES(@empID,@custID,@hairID,@custName,@total,GETDATE())
            COMMIT
        END
END
go

--Product_Sold insert function
drop proc if exists sp_ProdSoldInsert
go
CREATE PROC sp_ProdSoldInsert(
    @empID AS INT,
    @custID AS INT,
    @prodID AS INT,
    @custName AS varchar(20),
    @total AS money
)
AS
BEGIN
    BEGIN TRAN
    if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR @custID NOT IN(SELECT Customer_ID FROM Customer) OR @prodID NOT IN(SELECT Product_ID FROM Product) OR @custName='' OR @total<0)
        BEGIN
            PRINT 'Failed to insert into ProductSold table'
            ROLLBACK
        END
    else 
        BEGIN 
            PRINT 'Successfully inserted into ProductSold table'
            INSERT INTO Product_Sold VALUES(@empID,@custID,@prodID,@custName,@total,GETDATE())
            COMMIT
        END
END
go
delete from Product_Sold
declare @n AS varchar(20)=(select Customer_Name from Customer where Customer_ID=2)
exec sp_ProdSoldInsert 1,2,1,@n,100
select * from Product_Sold
declare @d AS DATE=GETDATE()
print @d
print @n

*/

--insert data
/*
delete from Haircut
dbcc checkident(Haircut,reseed,0)

exec sp_HaircutInsert "fade","tapered cut",50.00
exec sp_HaircutInsert "school cut","standard school cut",30.00
exec sp_HaircutInsert "mohawk","bald with spiked center",100.00
exec sp_HaircutInsert "bald","completely shaven head",20.00
select * from Haircut

delete from Product
dbcc checkident(Product,reseed,0)

exec sp_ProdInsert "comb","standard comb",45.00,30
exec sp_ProdInsert "brush","standard brush",65.00,22
exec sp_ProdInsert "shaving cream","moisturizing shaving cream",55.00,12
exec sp_ProdInsert "hair spray","non toxic hairspray",80.00,8
select * from Product

delete from Customer
dbcc checkident(Customer,reseed,0)

exec sp_CustInsert 1,"john doe",812111328,4
exec sp_CustInsert 2,"jane doe",812111329,4
exec sp_CustInsert 3,"steve anderson",812111330,4
exec sp_CustInsert 4,"johannes paulus",812111331,4
select * from Customer

delete from Position
dbcc checkident(Position,reseed,0)

exec sp_PosInsert "cashier","handles transactions" 
exec sp_PosInsert "manager","manages business" 
exec sp_PosInsert "barber","cuts hair" 
exec sp_PosInsert "cleaner","cleans business" 
select * from Position


delete from Employee
dbcc checkident(Employee,reseed,0)

exec sp_EmpInsert 1,"ashley adams",20,812408877
exec sp_EmpInsert 2,"david mutenga",30,812408878
exec sp_EmpInsert 3,"frank van wyk",24,812408879
exec sp_EmpInsert 4,"adrian van de merwe",43,812408880
select * from Employee
*/

/*
delete from Product_Sold
dbcc checkident(Product_Sold,reseed,0)
delete from Service_Rendered
dbcc checkident(Service_Rendered,reseed,0)
*/




go



















