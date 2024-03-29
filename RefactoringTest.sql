USE [RefactoringTest]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 27/01/2024 2:21:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NULL,
	[ClientStatusId] [tinyint] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 27/01/2024 2:21:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nchar](20) NULL,
	[Surname] [nchar](30) NULL,
	[DateOfBirth] [date] NULL,
	[EmailAddress] [nchar](30) NULL,
	[HasCreditLimit] [bit] NOT NULL,
	[CreditLimit] [int] NOT NULL,
	[ClientId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [HasCreditLimit]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [CreditLimit]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 27/01/2024 2:21:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nuwan Dilina
-- Create date: 26/01/2024
-- Description:	Add users according to provided details
-- =============================================
CREATE PROCEDURE [dbo].[uspAddUser]
	@Firstname VARCHAR(20),
	@Surname VARCHAR(30),
	@DateOfBirth DATE,
	@EmailAddress VARCHAR(50),
	@HasCreditLimit BIT,
	@CreditLimit INT,
	@ClientId INT
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN  

		BEGIN TRY 
         
			IF NOT EXISTS (SELECT * FROM Client WHERE ClientId = @ClientId)
			BEGIN
				RETURN
			END

			INSERT INTO [dbo].[User]
			   ([Firstname]
			   ,[Surname]
			   ,[DateOfBirth]
			   ,[EmailAddress]
			   ,[HasCreditLimit]
			   ,[CreditLimit]
			   ,[ClientId])
			VALUES
			   (@Firstname
			   ,@Surname
			   ,@DateOfBirth
			   ,@EmailAddress
			   ,@HasCreditLimit
			   ,@CreditLimit
			   ,@ClientId)

		END TRY

		BEGIN CATCH
			RETURN 
		END CATCH    

  END

END
GO
/****** Object:  StoredProcedure [dbo].[uspGetClientById]    Script Date: 27/01/2024 2:21:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nuwan Dilina
-- Create date: 26/01/2024
-- Description:	Returen client details by provided client ID
-- =============================================
CREATE PROCEDURE [dbo].[uspGetClientById]
	@ClientId int
AS
BEGIN

	SET NOCOUNT ON;

	BEGIN  

		BEGIN TRY 
         
			SELECT TOP 1 * from Client where ClientId = @ClientId

		END TRY

		BEGIN CATCH
			
		END CATCH    

  END

END
GO
