/*
use [master];
GO

create database [MyBank_Cashback];
GO

use [MyBank_Cashback];
GO

create table [Client] (
   ClientId                  int primary key identity,
   Name                      varchar(250),
   CPF                       varchar(20)
);
GO

create table [Transaction] (
   TransactionId            int primary key identity,
   Description              varchar(500),
   RegisterDate             datetime,
   Value                    decimal (18,2),
   ClientId                 int
);
GO

alter table [Transaction] add ClientId int;

create table [CashbackAccount] (
   CashbackAccountId         int primary key identity,
   RegisterDate              datetime,
   Value                     decimal (18,2),
   Active                    bit,
   ClientId                  int,
   TransactionId             int
);
GO

create table [CashbackConfiguration] (
   CashbackConfigurationId   int primary key,
   RegisterDate              datetime,
   Active                    bit,
   BasePercentage            decimal (8,2)
);
GO

insert into CashbackConfiguration(CashbackConfigurationId, RegisterDate, Active, BasePercentage) values(1, getdate(), 1, 10);
GO

use [MyBank_Cashback];
GO
create login mybank_lg with PASSWORD = 'mybank_lg@P1';
create user mybank_lg for login mybank_lg;
exec sp_addrolemember N'db_owner', N'mybank_lg';

use [master];
GO
GRANT CREATE ANY DATABASE TO mybank_lg;
*/