use SmartOfficeDB
Go

create table Department
(
   DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
   DepartmentName NVARCHAR(100) NOT NULL,
   IsActive BIT NOT NULL DEFAULT 1,
   CreatedDAte DATETIME NOT NULL DEFAULT GETDATE()
)
GO

CREATE TABLE Employee
(
  EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
  EmplotyeeName NVARCHAR(150) NOT NULL,
  Email NVARCHAR(150) NOT NULL UNIQUE,
  Salary DECIMAL(18,2) NOT NULL,
  DepartmentId Int NOT NULL,
  CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

  CONSTRAINT FK_Employee_Department
  FOREIGN KEY (DepartmentId)
  REFERENCES Department(DepartmentId)
  ON DELETE NO ACTION
  ON UPDATE CASCADE
)
GO

CREATE INDEX IX_Employee_DepartmentId
On Employee(DepartmentId)
Go

CREATE INDEX IX_Employee_Email
On Employee(Email)
Go


INSERT INTO Department (DepartmentName)
values('Human Resources'),
      ('IT'),
	  ('Finance'),
	  ('Marketing'),
	  ('Operations')
	  Go

INSERT INTO Employee (EmployeeName,Email,Salary,DepartmentId)
values('Rohan Jadhav','rohan@comapny.com',55000,2),
      ('Amit Singh','amit@comapny.com',45000,1),
	  ('John Doe','John@comapny.com',48000,3),
	  ('Vishal Jadhav','Vishal@comapny.com',42000,2),
	  ('Hitarth Saudankar','Hith@comapny.com',53000,4)
	  Go


	  USE SmartOfficeDB;
GO

CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO Users (Username, Password, Role)
VALUES 
('admin', 'admin@123', 'Admin'),
('manager', 'manager@123', 'Manager'),
('user', 'user@123', 'User');
GO
truncate table Employee