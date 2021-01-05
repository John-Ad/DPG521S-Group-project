use DPGTestDB
go

--general delete function
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
END
go

/*
exec sp_DeleteRecord 1,4
select * from Haircut
*/















