create database ThuVien
go

use ThuVien
go

create table Admins (
	Id int not null primary key identity,
	Email varchar(200) not null unique,
	Password varchar(50) not null,
	Fullname nvarchar(50),
	IsActived bit not null default 0,
	IsDeleted bit not null default 0
)
go

create table Account (
	Id varchar(200) not null primary key,
	Email varchar(50) not null unique,
	Fullname nvarchar(200) not null,
	Password varchar(200) not null,
	Address ntext,
	Avatar varchar(200),
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table Banners (
	Id int not null primary key identity,
	BannerImage varchar(200) not null unique,
	Title nvarchar(200) not null,
	Description nvarchar(500) not null,
	IsActived bit not null default 0,
	IsDeleted bit not null default 0
)
go

create table Category (
	Id varchar(200) not null primary key,
	Name nvarchar(200) not null unique,
	Slug varchar(50) not null unique,
	Image varchar(200),
	ParentId varchar(200) default 0,
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table Publisher (
	Id varchar(200) not null primary key,
	Name varchar(50) not null unique,
	Slug varchar(50) not null unique,
	Image varchar(200) not null unique,
	Website varchar(200) not null unique,
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeletedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table Author (
	Id varchar(200) not null primary key,
	Name varchar(50) not null unique,
	Slug varchar(50) not null unique,
	Image varchar(200) not null unique,
	Website varchar(200) not null unique,
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table Books (
	Id varchar(200) not null primary key,
	Slug varchar(50) not null unique,
	CategoryId varchar(200) not null foreign key references Category(Id),
	ManufactureId varchar(200) not null foreign key references Publisher(Id),
	Name varchar(50) not null unique,
	FeatureImage varchar(200) not null unique,
	Price bigint not null default 0,
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table BookDetail (
	Id varchar(200) not null primary key,
	ProductId varchar(200) not null foreign key references Books(Id),
	CategoryId varchar(200) not null foreign key references Category(Id),
	ProductPropertyId varchar(200) not null foreign key references Publisher(Id),
	ProductProperty varchar(50), 
	Value varchar(50),
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table BookImage (
	Id varchar(200) not null primary key,
	ProductId varchar(200) not null foreign key references Books(Id),
	Image varchar(200) not null unique,
	IsActived bit not null default 1,
	CreatedAt datetime default getdate(),
	CreatedBy varchar(200),
	UpdatedAt datetime,
	UpdatedBy varchar(200),
	DeletedAt datetime,
	DeleedBy varchar(200),
	IsDeleted bit not null default 0
)
go

create table Ticket (
	Id int not null primary key identity,
	Code varchar(50) not null unique,
	AccountId int not null foreign key references Account(Id),
	CreatedAt datetime not null default getdate(),
	TotalAmount int,
	Description nvarchar(500),
	IsActived bit not null default 0,
	IsDeleted bit not null default 0
)
go

create table TicketDetails (
	Id int not null primary key identity,
	OrderId int not null foreign key references Ticket(Id),
	OrderCode varchar(50) not null,
	BookId int not null foreign key references Books(Id),
	NumberOfBook int,
	Price int,
	IsActived bit not null default 0,
	IsDeleted bit not null default 0
)
go