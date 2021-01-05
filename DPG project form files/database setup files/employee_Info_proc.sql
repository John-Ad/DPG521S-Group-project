use DPGtestDb
go

/*
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
            FROM Equipment WHERE Equipment_ID=@id
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

exec sp_GetEmpInfo 1
*/

select * from Employee
select * from Service_Rendered
select * from Haircut
select * from Product_Sold

