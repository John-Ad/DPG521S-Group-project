/*
DROP DATABASE IF EXISTS DPGtestDB
go

CREATE DATABASE DPGtestDB;
go

use DPGtestDB;
go

CREATE TABLE Haircut(
    Haircut_ID int PRIMARY KEY IDENTITY(1,1),
    Haircut_Name varchar(20) NOT NULL,
    Haircut_Desc varchar(100),
    Price money NOT NULL
);
CREATE TABLE Product(
    Product_ID int PRIMARY KEY IDENTITY(1,1),
    Product_Name varchar(20) NOT NULL,
    Product_Desc varchar(100),
    Price money NOT NULL,
    Quantity int NOT NULL
);
CREATE TABLE Customer(
    Customer_ID int PRIMARY KEY IDENTITY(1,1),
    Haircut_ID int,
    Customer_Name varchar(20) NOT NULL,
    Cell_number int,
    Num_of_Visits int NOT NULL,
    FOREIGN KEY (Haircut_ID) REFERENCES Haircut(Haircut_ID)
);
CREATE TABLE Position(
    Position_ID int PRIMARY KEY IDENTITY(1,1),
    Position_Name varchar(20) NOT NULL,
    Position_Desc varchar(100)
);
CREATE TABLE Employee(
    Employee_ID int PRIMARY KEY IDENTITY(1,1),
    Position_ID int NOT NULL,
    Employee_Name varchar(20) NOT NULL,
    Age int,
    Cell_Number int NOT NULL,
    FOREIGN KEY (Position_ID) REFERENCES Position (Position_ID)
);
CREATE TABLE Equipment(
    Equipment_ID int PRIMARY KEY IDENTITY(1,1),
    Employee_ID int NOT NULL,
    Equipment_Name varchar(20) NOT NULL,
    Equipment_Desc varchar(100),
    Date_of_Purchase date NOT NULL,
    Eqp_Value money NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID)
);
CREATE TABLE Payroll(
    Employee_ID int PRIMARY KEY,
    Tax_Number int NOT NULL UNIQUE,
    Hrs_Worked_Month int NOT NULL,
    Renum_Per_Hr int NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID)
);
CREATE TABLE Service_Rendered(
    Service_Rendered_ID int PRIMARY KEY IDENTITY(1,1),
    Employee_ID int NOT NULL,
    Customer_ID int NOT NULL,
    Haircut_ID int NOT NULL,
    Customer_Name varchar(20),
    Total_Amount money NOT NULL,
    Date_of_Purchase date NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID),
    FOREIGN KEY (Customer_ID) REFERENCES Customer (Customer_ID),
    FOREIGN KEY (Haircut_ID) REFERENCES Haircut (Haircut_ID)
);
CREATE TABLE Product_Sold(
    Product_Sold_ID int PRIMARY KEY IDENTITY(1,1),
    Employee_ID int NOT NULL,
    Customer_ID int NOT NULL,
    Product_ID int NOT NULL,
    Customer_Name varchar(20),
    Total_Amount money NOT NULL,
    Date_of_Purchase date NOT NULL,
    FOREIGN KEY (Employee_ID) REFERENCES Employee (Employee_ID),
    FOREIGN KEY (Customer_ID) REFERENCES Customer (Customer_ID),
    FOREIGN KEY (Product_ID) REFERENCES Product (Product_ID)
);

SELECT * FROM Product;

go
*/
