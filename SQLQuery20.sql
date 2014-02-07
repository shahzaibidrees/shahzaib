USE [Vcard]
GO
/****** Object:  StoredProcedure [dbo].[sp_usercontacts]    Script Date: 11/28/2013 20:31:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[sp_usercontacts]
(
@OperationID		int,			--0
@ContactId			int,			--1
@UserId 			int,			--2
@Name				varchar(50),	--3
@MobileNumber		varchar(50),	--4
@EmailAddress		varchar(50),	--5
@Address			varchar(200),   --6
@Mobile				varchar(50)     --7

)
AS
declare @ID int = (select ID from dbo.tbl_user where Mobile = @mobile)
BEGIN
--For 
if (@OperationID = 1)
Begin

--declare @ID int = (select ID from dbo.tbl_user where Mobile = @mobile)
insert into dbo.tbl_Contacts(Name,MobileNumber,UserId) values (@Name,@MobileNumber,@ID)

End



if (@OperationID = 2)
Begin
Update tbl_Contacts
 set  Name=@Name,MobileNumber=@MobileNumber,EmailAddress=@EmailAddress
End


else if (@OperationID = 3)
Begin

select * from tbl_Contacts where UserId = @UserId
End

else if (@OperationID = 4)
Begin
select * from tbl_Contacts where ContactId = @ContactId
End

END
