create table Medicines (
ID int primary key identity, 
Name varchar(50), 
Manufacturer varchar(100), 
Price decimal(7,2), 
Amount int, 
WithPrescription bit not null default(0)
)

insert into Medicines (Name, Manufacturer, Price, Amount)
values ('Ibuprom', 'US Pharmacia', 12.38, 100);

insert into Medicines (Name, Manufacturer, Price, Amount)
values ('Apap', 'USP ZDROWIE', 11.73, 100);

insert into Medicines (Name, Manufacturer, Price, Amount)
values ('Amol', 'TAKEDA', 19.59, 100);

insert into Medicines (Name, Manufacturer, Price, Amount)
values ('Doppelherz Aktiv Magnez-B6', 'QUEISSER PHARMA', 7.62, 100);



-- UPDATE Medicines SET Name = @name, Manufacturer = @manufacturer, Price = @price, Amount = @amount, WithPrescription = @withPrescriptiom