use DPGtestDB
go

/*
--Haircut insert
drop proc if exists sp_HaircutInsert
go
CREATE PROC sp_HaircutInsert(
    @hName AS varchar(20),
    @hDesc AS varchar(100),
    @hPrice AS decimal(19,2)
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
                THROW 50001, 'Invalid parameters provided', 2
            END
END
go

exec sp_HaircutInsert "fade","tapered cut",50.00
exec sp_HaircutInsert "school cut","standard school cut",30.00
exec sp_HaircutInsert "mohawk","bald with spiked center",100.00
exec sp_HaircutInsert "bald","completely shaven head",20.00
select * from Haircut
go

--Product insert
drop proc if exists sp_ProdInsert
go
CREATE PROC sp_ProdInsert(
    @pName AS varchar(20),
    @pDesc AS varchar(100),
    @pPrice AS decimal(19,2),
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
                THROW 50001, 'Invalid parameters provided', 2
            END
END
go

exec sp_ProdInsert "comb","standard comb",45.00,30
exec sp_ProdInsert "brush","standard brush",65.00,22
exec sp_ProdInsert "shaving cream","moisturizing shaving cream",55.00,12
exec sp_ProdInsert "hair spray","non toxic hairspray",80.00,8
select * from Product
go


--Customer insertion
drop proc if exists sp_CustInsert
go
CREATE PROC sp_CustInsert(
    @hID AS int,
    @cName AS varchar(20),
    @cCell AS varchar(10),
    @cNumVisits AS int
)
AS
BEGIN
    BEGIN TRAN
        if(@cName != '' AND @cNumVisits >= 0 AND (@cCell LIKE '081_______%' OR @cCell LIKE '085_______%'))
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
                THROW 50001, 'Invalid parameters provided', 2
            END
END
go

exec sp_CustInsert 1001,"john doe",'0812111328',4
exec sp_CustInsert 1002,"jane doe",'0812111329',5
exec sp_CustInsert 1003,"steve anderson",'0852111330',6
exec sp_CustInsert 1004,"johannes paulus",'0852111331',7
select * from Customer
go


--position insert function
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
                THROW 50001, 'Invalid parameters provided', 2
            END
END
go

exec sp_PosInsert "cashier","handles transactions" 
exec sp_PosInsert "manager","manages business" 
exec sp_PosInsert "barber","cuts hair" 
exec sp_PosInsert "cleaner","cleans business" 
select * from Position
go


--Employee insert function
drop proc if exists sp_EmpInsert
go
CREATE PROC sp_EmpInsert(
    @ePosID AS int,
    @eName AS varchar(20),
    @eAge AS int,
    @eCell AS varchar(10) 
)
AS
BEGIN                       --test this before continueing
    BEGIN TRAN
        if(@ePosID NOT IN(SELECT Position_ID FROM Position) OR @eName='' OR (@eCell NOT LIKE '081_______%' AND @eCell NOT LIKE '085_______%'))
            BEGIN
                THROW 50001, 'Invalid parameters provided', 2
            END
        else
            BEGIN
                INSERT INTO Employee VALUES(@ePosID,@eName,@eAge,@eCell)
                COMMIT
            END
END
go

exec sp_EmpInsert 1,"ashley adams",20,'0812408877'
exec sp_EmpInsert 2,"david mutenga",30,'0812408878'
exec sp_EmpInsert 3,"frank van wyk",24,'0852408879'
exec sp_EmpInsert 4,"adrian van de merwe",43,'0852408880'
select * from Employee
go


--equipment insert function
drop proc if exists sp_EquipInsert
go
CREATE PROC sp_EquipInsert(
    @eEmpID AS int,
    @eName AS varchar(20),
    @eDesc AS varchar(100),
    @eValue AS decimal(19,2)
)
AS
BEGIN
    BEGIN TRAN
        if(@eEmpID NOT IN(SELECT Employee_ID FROM Employee) OR @eName='' OR @eValue <= 0)
            BEGIN
                THROW 50001, 'Invalid parameters provided', 2
            END
        else
            BEGIN
                INSERT INTO Equipment VALUES(@eEmpID,@eName,@eDesc,GETDATE(),@eValue)
                COMMIT
            END
END
go

select * from position
select * from Employee
exec sp_EquipInsert 3003,'Hair clippers','Used to cut hair',200.00
exec sp_EquipInsert 3003,'Pack of razor blades','Used to trim hair',10.00
select * from Equipment
go


--payroll insert function
drop proc if exists sp_PayrollInsert
go
CREATE PROC sp_PayrollInsert(
    @empID AS INT,
    @taxNum AS varchar(8),
    @hrsWorked AS INT,
    @renumPerHr AS INT
)
AS
BEGIN
    BEGIN TRAN
        if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR (@taxNum NOT LIKE '0_______%') OR @hrsWorked<0 OR @renumPerHr<0)
            BEGIN
                THROW 50001, 'Invalid parameters provided', 2
            END
        else 
            BEGIN
                INSERT INTO Payroll VALUES(@empID,@taxNum,@hrsWorked,@renumPerHr)
                COMMIT
            END
END
go

exec sp_PayrollInsert 3001,'01234567',10,20
exec sp_PayrollInsert 3002,'01234568',28,50
exec sp_PayrollInsert 3003,'01234569',40,30
exec sp_PayrollInsert 3004,'01234570',18,10
select * from Payroll
go


--Service_Rendered insert function
drop proc if exists sp_ServRendInsert
go
CREATE PROC sp_ServRendInsert(
    @empID AS INT,
    @custID AS INT,
    @hairID AS INT,
    @custName AS varchar(20),
    @quantity AS int,
    @total AS decimal(19,2)
)
AS
BEGIN
    BEGIN TRAN
        if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR @custID NOT IN(SELECT Customer_ID FROM Customer) OR @hairID NOT IN(SELECT Haircut_ID FROM Haircut) OR @custName='' OR @quantity<=0 OR @total<0)
            BEGIN
                THROW 50001, 'Invalid parameters provided', 2
            END
        else 
            BEGIN 
                INSERT INTO Service_Rendered VALUES(@empID,@custID,@hairID,@custName,@quantity,@total,GETDATE())
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
    @quantity AS int,
    @total AS decimal(19,2)
)
AS
BEGIN
    BEGIN TRAN
        if(@empID NOT IN(SELECT Employee_ID FROM Employee) OR @custID NOT IN(SELECT Customer_ID FROM Customer) OR @prodID NOT IN(SELECT Product_ID FROM Product) OR @custName='' OR @quantity<=0 OR @total<0)
            BEGIN
                THROW 50001, 'Invalid parameters provided', 2
            END
        else 
            BEGIN 
                INSERT INTO Product_Sold VALUES(@empID,@custID,@prodID,@custName,@quantity,@total,GETDATE())
                COMMIT
            END
END
go
*/

/*
dbcc checkident(Product_Sold,reseed,0)
*/
go



















