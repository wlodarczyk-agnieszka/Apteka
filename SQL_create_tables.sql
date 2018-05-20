create table Medicines (
ID int primary key identity, 
Name varchar(50), 
Manufacturer varchar(100), 
Price decimal(7,2), 
Amount int, 
WithPrescription bit not null default(0)
);

create table Prescriptions (
ID int identity primary key,
CustomerName varchar(100) not null,
PESEL varchar(11),
PrescriptionNumber varchar(30) not null
);

create table Orders (
ID int identity primary key,
MedicineID int not null,
PrescriptionID int,
Amount int,
Date Date DEFAULT GETDATE()
constraint FK_MedicineID foreign key (MedicineID) references Medicines (ID),
constraint FK_PrescriptionID foreign key (PrescriptionID) references Prescriptions (ID)
);

