
DROP DATABASE IF EXISTS DPGtestDBv2
go

CREATE DATABASE DPGtestDBv2;
go

use DPGtestDBv2;
go

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
--///////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////
CREATE TABLE Transaction_Performed(
    Transaction_ID int PRIMARY KEY IDENTITY(1000001,1),
    Employee_ID int NOT NULL,
    Customer_ID int NOT NULL,
    Customer_Name varchar(20) NOT NULL,
    Total decimal(19,2) NOT NULL,
    Date_of_Purchase datetime NOT NULL
);
CREATE TABLE Service_Rendered(
    Service_Rendered_ID int PRIMARY KEY IDENTITY(600001,1),  --serveRend ids are > 600000
    Haircut_ID int NOT NULL,
    Quantity int NOT NULL,
    Total_Amount decimal(19,2) NOT NULL,
    FOREIGN KEY (Haircut_ID) REFERENCES Haircut (Haircut_ID)
);
CREATE TABLE Product_Sold(
    Product_Sold_ID int PRIMARY KEY IDENTITY(600001,1),  --serveRend ids are > 600000
    Product_ID int NOT NULL,
    Quantity int NOT NULL,
    Total_Amount decimal(19,2) NOT NULL,
    FOREIGN KEY (Product_ID) REFERENCES Product (Product_ID)
);
--///////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////
--///////////////////////////////////////////////////////////////////////////////////////
go
*/

drop table if EXISTS Profit_Loss_Data
go
CREATE TABLE Profit_Loss_Data(
    _Year smallint NOT NULL,
    _Month smallint NOT NULL,
    Expenses decimal(19,2) NOT NULL,
    _Target decimal(19,2) NOT NULL
    CONSTRAINT PK_date PRIMARY KEY NONCLUSTERED(_Year,_Month)
);
go

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
