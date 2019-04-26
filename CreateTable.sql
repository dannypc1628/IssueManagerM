Create Table [User]
(
	[UserID] nchar(10) PRIMARY KEY NOT NULL,
	[UserName] nvarchar(10) NOT NULL ,
	[Password] nchar(10) NOT NULL,
	[Phone] nchar(10) NOT NULL,
	[Email] nvarchar(50) NOT NULL
)
Create Table [Unit]
(
	[UnitID] int PRIMARY KEY IDENTITY(1,1),
	[UnitName] nvarchar(10) NOT NULL UNIQUE
)
Create Table [ParentUnit]
(
	ChildID int  REFERENCES [Unit](UnitID) not null UNIQUE,
	ParentID int REFERENCES [Unit](UnitID) not null 
)
Create Table [Role]
(
	RoleID int PRIMARY KEY identity(1,1),
	[RoleName] nvarchar(50) not null UNIQUE
)
Create Table [UserRole]
(
	[UserID] nchar(10) REFERENCES [User](UserID) not null,
	[RoleID] int REFERENCES [Role](RoleID) not null
)

Create Table [State]
(
	StateID int PRIMARY KEY IDENTITY(1,1),
	StateName nvarchar(10)  not null
)
Create Table [Action]
(
	ActionID int PRIMARY KEY IDENTITY(1,1),
	ActionName nvarchar(10) not null 
)
Create Table [Question]
(
	[QuestionID] int PRIMARY KEY IDENTITY(1,1),
	[Title] nvarchar(50) not null,
	[Content] nvarchar(255),
	[Asker]nvarchar(10) not null,
	[AskDate] datetime not null,
	[CaseUnit] int,
	[CaseOfficer] nchar(10),
	[StateID] int REFERENCES [State](StateID) not null
)

Create Table [QuestionStepResult]
(
	QuestionStepResultID int PRIMARY KEY IDENTITY(1,1),
	CreateDate datetime not null,
	CreateUser nchar(10) not null,
	CreateUserRole int not null,
	[Content] nvarchar(50),
	[ActionID] int REFERENCES [Action](ActionID) not null,
	QuestionID int REFERENCES [Question](QuestionID) not null
	
	
)
