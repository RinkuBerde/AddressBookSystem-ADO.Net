select * from AddressBookDetails
  --UC02-SP for insert value in table--
CREATE or alter PROCEDURE [dbo].[spInsertintoTable]
(
    @FirstName varchar(255),
	@LastName varchar(255),
	@Address varchar(255),
	@City varchar(255),
	@State varchar(255),
	@zip int,
	@PhoneNumber bigint,
	@Email varchar(255),
	@Book_Name varchar(200),
	@Contact_Type  varchar(200)
)
as
begin
	INSERT INTO  AddressBookDetails VALUES
	(
	 @FirstName, @LastName, @Address, @City, @State, @Zip, @PhoneNumber, @Email, @Book_Name, @Contact_Type
	 )
end
GO

-- SP for Get Contact
CREATE PROCEDURE [dbo].[SpGetContact]
@FirstName varchar(255)
AS
	SELECT  FirstName, LastName, Address, City, State, Zip, PhoneNumber, Email, Book_Name, Contact_Type FROM AddressBookDetails WHERE FirstName = @FirstName;
RETURN 0

 --UC03-SP for Edit Contact---
CREATE PROCEDURE [dbo].[SpEditContact]
@name varchar(255),
@FirstName varchar(255),
	@LastName varchar(255),
	@Address varchar(255),
	@City varchar(255),
	@State varchar(255),
	@zip int,
	@PhoneNumber bigint,
	@Email varchar(255),
	@Book_Name varchar(200),
	@Contact_Type  varchar(200)
AS
	UPDATE AddressBookDetails set FirstName = @FirstName, LastName =@LastName , Address = @Address, City =@City , State = @State, Zip =@Zip , PhoneNumber = @PhoneNumber, Email = @Email , Book_Name=@Book_Name , Contact_Type = @Contact_Type 
	Where FirstName = @name;
RETURN 0