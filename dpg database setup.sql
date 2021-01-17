DROP DATABASE IF EXISTS DPGtestDBv2
go

CREATE DATABASE DPGtestDBv2;
go

use DPGtestDBv2;
go

--table creation
CREATE TABLE Haircut(
    Haircut_ID int PRIMARY KEY IDENTITY(1001,1), --all haircut ids are > 1000 and < 2000
    Haircut_Name varchar(20) NOT NULL,
    Haircut_Desc varchar(100),
    Price decimal(19,2) NOT NULL
);
CREATE TABLE Product(
    Product_ID int PRIMARY KEY IDENTITY(2001,1), --all haircut ids are > 2000 and < 3000
    Product_Name varchar(20) NOT NULL,
    Product_Desc varchar(100),
    Price decimal(19,2) NOT NULL,
    Quantity int NOT NULL
);
CREATE TABLE Customer(
    Customer_ID int PRIMARY KEY IDENTITY(300001,1), --all customer ids are > 300000 and < 600000
    Haircut_ID int,
    Customer_Name varchar(20) NOT NULL,
    Cell_number varchar(10),
    Num_of_Visits int NOT NULL,
    FOREIGN KEY (Haircut_ID) REFERENCES Haircut(Haircut_ID)
);
CREATE TABLE Position(
    Position_ID int PRIMARY KEY IDENTITY(1,1),     --all position ids are > 0 and less than 20
    Position_Name varchar(20) NOT NULL,
    Position_Desc varchar(100)
);
CREATE TABLE Employee(
    Employee_ID int PRIMARY KEY IDENTITY(3001,1),   --all employee ids are > 3000 and < 4000
    Position_ID int NOT NULL,
    Employee_Name varchar(20) NOT NULL,
    Age int,
    Cell_Number varchar(10) NOT NULL,
    FOREIGN KEY (Position_ID) REFERENCES Position (Position_ID)
);
CREATE TABLE Equipment(
    Equipment_ID int PRIMARY KEY IDENTITY(4001,1),  --all equipment ids are > 4000 and < 5000
    Employee_ID int NOT NULL,
    Equipment_Name varchar(20) NOT NULL,
    Equipment_Desc varchar(100),
    Date_of_Purchase date NOT NULL,
    Eqp_Value decimal(19,2) NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID)
);
CREATE TABLE Payroll(
    Employee_ID int PRIMARY KEY,        --employee table and payroll table use the same primary key
    Tax_Number varchar(8) NOT NULL UNIQUE,
    Hrs_Worked_Month int NOT NULL,
    Renum_Per_Hr decimal(19,2) NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID)
);
CREATE TABLE Service_Rendered(
    Service_Rendered_ID int PRIMARY KEY IDENTITY(600001,1),  --serveRend ids are > 600000
    Employee_ID int NOT NULL,
    Customer_ID int NOT NULL,
    Haircut_ID int NOT NULL,
    Customer_Name varchar(20),
    Quantity int NOT NULL,
    Total_Amount decimal(19,2) NOT NULL,
    Date_of_Purchase date NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID),
    FOREIGN KEY (Customer_ID) REFERENCES Customer (Customer_ID),
    FOREIGN KEY (Haircut_ID) REFERENCES Haircut (Haircut_ID)
);
CREATE TABLE Product_Sold(
    Product_Sold_ID int PRIMARY KEY IDENTITY(600001,1),  --serveRend ids are > 600000
    Employee_ID int NOT NULL,
    Customer_ID int NOT NULL,
    Product_ID int NOT NULL,
    Customer_Name varchar(20),
    Quantity int NOT NULL,
    Total_Amount decimal(19,2) NOT NULL,
    Date_of_Purchase date NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID),
    FOREIGN KEY (Customer_ID) REFERENCES Customer (Customer_ID),
    FOREIGN KEY (Product_ID) REFERENCES Product (Product_ID)
);
CREATE TABLE Profit_Loss_Data(
    _Year smallint NOT NULL,
    _Month smallint NOT NULL,
    Expenses decimal(19,2) NOT NULL,
    _Target decimal(19,2) NOT NULL
    CONSTRAINT PK_date PRIMARY KEY NONCLUSTERED(_Year,_Month)
);
go
--//////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////

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

--example data          --can be changed
exec sp_HaircutInsert "fade","tapered cut",50.00
exec sp_HaircutInsert "school cut","standard school cut",30.00
exec sp_HaircutInsert "mohawk","bald with spiked center",100.00
exec sp_HaircutInsert "bald","completely shaven head",20.00
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

--example data      --can be changed
exec sp_ProdInsert "comb","standard comb",45.00,30
exec sp_ProdInsert "brush","standard brush",65.00,22
exec sp_ProdInsert "shaving cream","moisturizing shaving cream",55.00,12
exec sp_ProdInsert "hair spray","non toxic hairspray",80.00,8
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

--example data      --can be changed
exec sp_CustInsert 1001,"john doe",'0812111328',4
exec sp_CustInsert 1002,"jane doe",'0812111329',5
exec sp_CustInsert 1003,"steve anderson",'0852111330',6
exec sp_CustInsert 1004,"johannes paulus",'0852111331',7
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

--example data      --can be changed
exec sp_PosInsert "cashier","handles transactions" 
exec sp_PosInsert "manager","manages business" 
exec sp_PosInsert "barber","cuts hair" 
exec sp_PosInsert "cleaner","cleans business" 
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

--example data      --can be changed
exec sp_EmpInsert 1,"ashley adams",20,'0812408877'
exec sp_EmpInsert 2,"david mutenga",30,'0812408878'
exec sp_EmpInsert 3,"frank van wyk",24,'0852408879'
exec sp_EmpInsert 4,"adrian van de merwe",43,'0852408880'
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

--example data      --can be changed
exec sp_EquipInsert 3003,'Hair clippers','Used to cut hair',200.00
exec sp_EquipInsert 3003,'Pack of razor blades','Used to trim hair',10.00
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
go


--Service_Rendered insert function

--for servRend and prod sold, use the sales form to insert data
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

    --prof/lossData insert
drop proc if exists sp_ProfLossInsert
go
CREATE PROC sp_ProfLossInsert(
    @expens AS decimal(19,2),
    @trgt AS decimal(19,2)
)
AS
BEGIN
    DECLARE @year AS smallint,@month AS smallint,@recCount AS int
    SET @year=CAST(YEAR(GETDATE()) AS smallint)
    SET @month=CAST(MONTH(GETDATE()) AS smallint)
    SET @recCount=(SELECT COUNT(*) FROM Profit_Loss_Data WHERE _Year=@year AND _Month=@month)
    if(@expens>=0 AND @trgt>=0 AND @recCount=0)
    BEGIN
        INSERT INTO Profit_Loss_Data VALUES (@year,@month,@expens,@trgt)
    END
    else 
    BEGIN
        THROW 50001,'Invalid parameters!',4
    END
END
go
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

--deletion proc
drop proc if exists sp_DeleteRecord
go
CREATE PROC sp_DeleteRecord(
    @table AS int,
    @id AS int
)
AS
BEGIN
    if(@table=1) 
        DELETE FROM Haircut WHERE Haircut_ID=@id 
    if(@table=2)
        DELETE FROM Product WHERE Product_ID=@id 
    if(@table=3)
        DELETE FROM Customer WHERE Customer_ID=@id 
    if(@table=4)
        DELETE FROM Position WHERE Position_ID=@id 
    if(@table=5)
        DELETE FROM Employee WHERE Employee_ID=@id 
    if(@table=6)
        DELETE FROM Equipment WHERE Equipment_ID=@id 
    if(@table=7)
        DELETE FROM Payroll WHERE Employee_ID=@id 
    if(@table=8)
        DELETE FROM Service_Rendered WHERE Service_Rendered_ID=@id 
    if(@table=9)
        DELETE FROM Product_Sold WHERE Product_Sold_ID=@id 
    if(@table<1 OR @table>9)
        THROW 50001,'Invalid parameters provided',19
END
go
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////

--update procs      
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

drop proc if exists sp_updateEmployee
go
CREATE PROC sp_updateEmployee(
    @id AS int=null,
    @posID AS int=null,
    @name AS varchar(20)=null,
    @age AS int,
    @cNum AS varchar(10)=null
)
AS
BEGIN
    if(@id IN(SELECT Employee_ID FROM Employee) AND @posID IN(SELECT Position_ID FROM Position) AND (@name is not null AND @name!='') AND (@cNum LIKE '081_______%' OR @cNum LIKE '085_______%'))
    BEGIN
        UPDATE Employee 
        SET Position_ID=@posID,Employee_Name=@name,Age=@age,Cell_Number=@cNum
        WHERE Employee_ID=@id
    END
    else
    BEGIN
        THROW 50001, 'Failed to update record: invalid field/s',15
    END
END

drop proc if exists sp_updatePayroll
go
CREATE PROC sp_updatePayroll(
    @id AS int=null,
    @taxNum AS varchar(8)=null,
    @hrsWrkd AS int=null,
    @renum AS decimal(19,2)=null
)
AS
BEGIN 
    if(@id NOT IN(SELECT Employee_ID FROM Employee) OR (@taxNum NOT LIKE '0_______%') OR @hrsWrkd<0 OR @renum<0)
    BEGIN
        THROW 50001, 'Failed to update record: invalid field/s',15
    END
    else
    BEGIN
        UPDATE Payroll 
        SET Tax_Number=@taxNum,Hrs_Worked_Month=@hrsWrkd,Renum_Per_Hr=@renum
        WHERE Employee_ID=@id
    END
END
go

drop proc if exists sp_updateEquipment
go
CREATE PROC sp_updateEquipment(
    @id AS int,
    @empID AS int,
    @name AS varchar(20),
    @desc AS varchar(100),
    @val AS decimal(19,2)
)
AS
BEGIN
    if(@id IN (SELECT Equipment_ID FROM Equipment) AND @empID IN (SELECT Employee_ID FROM Employee) AND @name!='' AND @val>=0 )
    BEGIN
        UPDATE Equipment
        SET Employee_ID=@empID,Equipment_Name=@name,Equipment_Desc=@desc,Eqp_Value=@val
        WHERE Equipment_ID=@id
    END
    else 
    BEGIN
        THROW 50001, 'Failed to update record: invalid field/s',15
    END
END
go

drop proc if exists sp_updateProfLoss
go
CREATE PROC sp_updateProfLoss(
    @expenses AS decimal(19,2),
    @target AS decimal(19,2)
)
AS
BEGIN
    DECLARE @year AS smallint,@month AS smallint,@recCount AS int
    SET @year=CAST(YEAR(GETDATE()) AS smallint)
    SET @month=CAST(MONTH(GETDATE()) AS smallint)

    SET @recCount=(SELECT COUNT(*) FROM Profit_Loss_Data WHERE _Year=@year AND _Month=@month)
    if(@recCount=0)
    BEGIN
        THROW 50002,'No record for this month exists! Add a new record',6
        RETURN
    END

    UPDATE Profit_Loss_Data
    SET Expenses=@expenses,_Target=@target
    WHERE _Year=@year AND _Month=@month
END
go
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

--search by name proc
DROP PROC IF EXISTS sp_SearchByName
go
CREATE PROC sp_SearchByName
(
    @name AS varchar(20),
    @table AS int
)
AS
BEGIN
    if(@table=1) 
        SELECT * FROM Haircut WHERE Haircut_Name LIKE '%' + @name + '%' ORDER BY Haircut_Name
    if(@table=2)
        SELECT * FROM Product WHERE Product_Name LIKE '%' + @name + '%' ORDER BY Product_Name
    if(@table=3)
        SELECT * FROM Customer WHERE Customer_Name LIKE '%' + @name + '%' ORDER BY Customer_Name
    if(@table=4)
        SELECT * FROM Position WHERE Position_Name LIKE '%' + @name + '%' ORDER BY Position_Name
    if(@table=5)
        SELECT * FROM Employee WHERE Employee_Name LIKE '%' + @name + '%' ORDER BY Employee_Name
    if(@table=6)
        SELECT * FROM Equipment WHERE Equipment_Name LIKE '%' + @name + '%'  ORDER BY Equipment_Name
    if(@table=7)
        SELECT * FROM Service_Rendered WHERE Customer_Name LIKE '%' + @name + '%' ORDER BY Customer_Name
    if(@table=8)
        SELECT * FROM Product_Sold WHERE Customer_Name LIKE '%' + @name + '%' ORDER BY Customer_Name
END
go
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
--/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

--employee info query proc
DROP PROC IF EXISTS sp_GetEmpInfo
go
CREATE PROC sp_GetEmpInfo(
    @id AS int
)
AS
BEGIN
    if(@id IN (SELECT Employee_ID FROM Employee))
        BEGIN
            --declare variables
            DECLARE @numOfTrans AS int, @numOfEqp AS int,
                    @totalRev AS int, @totalEqVal AS int, @mostHelpedCus AS varchar(20)

            --set variables
            SELECT @numOfTrans=(COUNT(*)), @totalRev=(SUM(Total_Amount))
            FROM Service_Rendered WHERE Employee_ID=@id

                 --cashier id only appears in the prodsold table, if numoftrans is 0, the emp is a barber and servRendered is queried instead
            if(@numOfTrans=0)
            BEGIN
                SELECT @numOfTrans=(COUNT(*)), @totalRev=(SUM(Total_Amount))
                FROM Product_Sold WHERE Employee_ID=@id

                SELECT @mostHelpedCus=Customer_Name FROM Customer
                WHERE Customer_ID=(
                            SELECT TOP 1 Customer_ID FROM Product_Sold 
                            WHERE Employee_ID=@id
                            GROUP BY Customer_ID ORDER BY COUNT(Customer_ID) DESC)
            END
            else
            BEGIN
                SELECT @mostHelpedCus=Customer_Name FROM Customer
                WHERE Customer_ID=(
                            SELECT TOP 1 Customer_ID FROM Service_Rendered
                            WHERE Employee_ID=@id
                            GROUP BY Customer_ID ORDER BY COUNT(Customer_ID) DESC)
            END

            SELECT @numOfEqp=(COUNT(Equipment_ID)), @totalEqVal=(SUM(Eqp_Value)) 
            FROM Equipment WHERE Employee_ID=@id
            ---------------------

            --gets all the required information to complete the form
            SELECT  Employee.Employee_ID,Employee.Employee_Name,Employee.Age,Employee.Cell_Number,
		            Position.Position_Name, Position.Position_Desc,
		            Payroll.Tax_Number, Payroll.Hrs_Worked_Month, Payroll.Renum_Per_Hr,
                    @numOfTrans AS Num_of_trans,@totalRev AS total_revenue, @mostHelpedCus AS most_helped_cus,
                    @numOfEqp AS num_of_equip,@totalEqVal AS total_eqp_value
            FROM  (Employee INNER JOIN Position ON Position.Position_ID=Employee.Position_ID)
                  INNER JOIN Payroll ON Payroll.Employee_ID=Employee.Employee_ID
            WHERE Employee.Employee_ID=@id
        END
END
go
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////


--revenue procs
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
    SELECT SUM(Total_Amount) AS Prod FROM Product_Sold
    WHERE Product_Sold.Date_of_Purchase>=@start AND Product_Sold.Date_of_Purchase<=@end 
END
go
    -------------------------------------------------------------------

    --gets the 3 customers that spent the most for a given time period
drop proc if exists sp_BestCustomers
go
CREATE PROC sp_BestCustomers(
    @mode AS int=2,
    @start AS date=null,
    @end AS date=null
)
AS
BEGIN
    if(@mode>=1 AND @mode<=3)   -- 1=day, 2=month, 3=year
    BEGIN
        EXEC sp_getDates @mode,@start OUTPUT,@end OUTPUT
    END
    if(@mode>4 OR @mode<1)
    BEGIN
        THROW 50001, 'Mode must be > 0 and < 5',24
        RETURN
    END

    SELECT TOP 3 Customer_ID,Customer_Name,SUM(Total_Amount)
    FROM
    (
        SELECT Customer_ID,Customer_Name,SUM(Total_Amount) as Total_Amount,Date_of_Purchase
        FROM Service_Rendered
        GROUP By Customer_ID,Customer_Name,Date_of_Purchase
        UNION
        SELECT Customer_ID,Customer_Name,SUM(Total_Amount),Date_of_Purchase
        FROM Product_Sold
        GROUP BY Customer_ID,Customer_Name,Date_of_Purchase
    ) t
    WHERE Date_of_Purchase>=@start AND Date_of_Purchase<=@end
    GROUP BY Customer_ID,Customer_Name
    ORDER BY SUM(Total_Amount) DESC
END
go
--//////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////
--//////////////////////////////////////////////////////////////////////////////////////////////////////////

















