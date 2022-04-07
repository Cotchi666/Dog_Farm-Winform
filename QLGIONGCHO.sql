create database QLGIONGCHO
go

use QLGIONGCHO
Go

Create table [dbo].[Employee]
(
	EmpId INT NOT NULL PRIMARY KEY IDENTITY(300,1),
	EmpName VARCHAR(50)NOT NULL ,
	EmpDob Date NOT NULL ,
	Gender VARCHAR(50) NOT NULL ,
	Phone VARCHAR(50) NOT NULL ,
	Address VARCHAR(50) NOT NULL ,
)
go

Create table [dbo].[Health]
(
	ReportId INT NOT NULL PRIMARY KEY IDENTITY(500,1),
	DogId INT NOT NULL ,
	DogName VARCHAR(50) NOT NULL ,
	ReportDate Date NOT NULL ,
	[Event] VARCHAR(50) NOT NULL ,
	Diagnosis VARCHAR(50) NOT NULL ,
	Cost VARCHAR(50) NOT NULL ,
	Vetname VARCHAR(50) NOT NULL ,
	Treatment VARCHAR(50) NOT NULL ,

)
go
Create table [dbo].[Breeding]
(
	BreedId INT NOT NULL PRIMARY KEY IDENTITY(2000,1),
	BreedDate Date NOT NULL,
	DogId INT NOT NULL ,
	DogName VARCHAR(50) NOT NULL ,
	PregDate Date NOT NULL ,
	DateBorned Date NOT NULL ,
	DogAge INT NOT NULL ,
	Remarks VARCHAR(50) NOT NULL ,


)