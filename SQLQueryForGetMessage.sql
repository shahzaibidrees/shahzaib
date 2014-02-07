USE [Vcard]
GO
/****** Object:  StoredProcedure [dbo].[sp_Chat]    Script Date: 11/28/2013 12:49:@SenderId ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[sp_Chat]
(
@OperationID			int,			--0
@ID						int,			--1
@SenderId				int,			--2
@ReceiverId				int,			--3
@ContactId				int,			--4
@Message				nvarchar(1000),	--5
@SendDate				datetime,		--6
@ReceiveDate			datetime,		--7
@IsDeleted				int,			--8
@IsReadFrom				varchar(50),	--9
@IsReadTo    			varchar(50),	--10
@Mobile					varchar(50)		--11
)
AS
BEGIN
--For 
if (@OperationID = 1)
Begin

UPDATE [tbl_Chat] SET IsReadFrom='Yes' WHERE ([tbl_Chat].SenderId = @SenderId AND [tbl_Chat].ReceiverId=@ReceiverId)
UPDATE [tbl_Chat] SET IsReadTo='Yes' WHERE ([tbl_Chat].SenderId = @ReceiverId AND [tbl_Chat].ReceiverId=@SenderId)

SET NOCOUNT ON; 

SELECT     dbo.tbl_user.*, dbo.tbl_Chat.*,

CASE WHEN DATEDIFF(day, [tbl_Chat].[SendDate], getdate()) = 0 THEN 'Today' WHEN DATEDIFF(day, [tbl_Chat].[SendDate], getdate()) 
                      = @SenderId THEN 'Yesterday' ELSE CONVERT(VARCHAR(12),
                       [tbl_Chat].[SendDate], 107) END AS 'MessagesDate',CONVERT(VARCHAR(12), [tbl_Chat].[SendDate], 107) AS 'MessagesDate1',(SELECT TOP(@SenderId)[tbl_user].Name)


FROM         dbo.tbl_Chat INNER JOIN
                      dbo.tbl_user ON dbo.tbl_Chat.SenderId = dbo.tbl_user.ID where SenderId = @ReceiverId or SenderId = @SenderId 

End
if (@OperationID = 2)
Begin
Declare @user int
set @user=(select ID from tbl_User where Mobile = @Mobile)


insert into tbl_Chat (SenderId, ReceiverId, ContactId, [Message],SendDate,ReceiveDate,IsDeleted,IsReadFrom,IsReadTo) values(@user, @ReceiverId, @ContactId, @Message, @SendDate, @ReceiveDate, @IsDeleted,@IsReadFrom,@IsReadTo)



End

END