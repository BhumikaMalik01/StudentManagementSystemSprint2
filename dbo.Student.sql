CREATE TABLE [dbo].[Student] (
    [StudentID]        INT            IDENTITY (1, 1) NOT NULL ,
    [StudentFirstName] NVARCHAR (MAX) NOT NULL,
    [StudentLastName]  NVARCHAR (MAX) NULL,
    [StudentCourse]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([StudentID] ASC)
);

