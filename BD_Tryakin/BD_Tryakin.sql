Create database GippoBase
use GippoBase

create table ProductTypes	--тип продукта
(
	Id_ProductType int identity(1, 1) primary key not null,
	Name_ProductType varchar(20) not null
)

create table Invoices --накладная
(
	ID_Invoice int identity(1, 1) primary key not null,
	Name_Invoice varchar(30) not null,
	Date_Invoice date not null,
	ProductCount_Invoice int not null
)

create table PurchaseInvoices --приходная накладная
(
	ID_PurchaseInvoice int identity(1, 1) primary key not null,
	Name_PurchaseInvoice varchar(30) not null,
	Date_PurchaseInvoice date not null,
	ProductCount_PurchaseInvoice int not null
)

create table Users -- кладовщик
(
	Login_User varchar(20) primary key not null,
	Password_User varchar(20) not null,
	Name_User varchar(20) not null,
	Surname_User varchar(20) not null,	
	Address_User varchar(50) not null,
	IsAdmin_User bit default 0 not null,
)

create table Products -- продукт
(
	Id_Product int identity(1, 1) primary key not null,
	Name_Product varchar(50) not null,
	ProductCount_Product int not null,
	Description_Product varchar(150),
	Price_Product decimal(6,2) not null,

	Id_ProductType int not null,
	constraint FK_Id_ProductType foreign key (Id_ProductType) references ProductTypes(Id_ProductType)
	on delete cascade on update cascade,

	ID_Invoice int null,
	constraint FK_ID_Invoice foreign key (ID_Invoice) references Invoices(ID_Invoice)
	on update cascade on delete cascade,

	ID_PurchaseInvoice int null,
	constraint FK_ID_PurchaseInvoice foreign key (ID_PurchaseInvoice) references PurchaseInvoices(ID_PurchaseInvoice)
	on update cascade on delete cascade,

	Login_User varchar(20) null,
	constraint FK_Id_User foreign key (Login_User) references Users(Login_User)
	on delete cascade on update cascade
)

drop table Products
drop table Users
drop table PurchaseInvoices
drop table Invoices
drop table ProductTypes

insert into Users values
('Admin', 'Admin', 'Ivan', 'Ivanovich', 'Pupkina 69a', 1),
('User', 'User', 'Sergey', 'Sergeevich', 'Lapkina 22b', 0)
