/*
DROP DATABASE IF EXISTS DPGtestDB
go

CREATE DATABASE DPGtestDB;
go

use DPGtestDB;
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

go
*/
