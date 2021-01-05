use DPGtestDB
go

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

exec sp_SearchByName 'oe',3
go


