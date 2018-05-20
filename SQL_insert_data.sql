/* MEDICINES */

INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Alantan Plus, krem', 'UNIA ',  5.58, 18, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Allegra 120mg', 'SANOFI ',  9.28, 27, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Apap, 50 tabletek', 'USP ZDROWIE ',  15.33, 31, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Allnutrition', 'SFD ',  2.69, 58, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Beta Karoten Sun', 'NUTROPHARM ',  7.99, 26, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Calcium 300 + witamina C', 'POLSKI LEK ',  2.89, 79, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Cetip 10mg', 'VITAMAX ',  3.29, 45, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Cynarex 250mg', 'HERBAPOL ',  7.98, 24, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Dicoflor 60', 'BAYER',  24.75, 15, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('ENTIL, ¿el', 'Aflofarm',  4.27, 67, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Ibuprom MAX', 'USP ZDROWIE ',  16.28, 27, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Ibuprom Zatoki', 'USP ZDROWIE ',  9.50, 63, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Ketonal Active', 'SANDOZ ',  6.89, 46, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Magne B6 Forte', 'SANOFI ',  16.89, 18, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Nurofen Express Forte', 'RECKITT BENC ',  11.54, 35, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Rutinoscorbin', 'GLAXOSMITHKLI ',  10.42, 17, 0);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Clatra', 'MENARINI ',  28.99, 29, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Telfexo ', 'POLPHARMA S.A. ',  18.49, 30, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Milurit', 'PROTERAPIA ',  8.53, 26, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Yasmin', 'BAYER ',  29.99, 48, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Belara', 'GEDEON RICHTER POLSKA ',  31.99, 25, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Xifaxan', 'BRAK',  96.99, 47, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Neurovit', 'G.L.PHARMA GMBH ',  54.99, 26, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Heparegen', 'PHARMASWISS CESKA REPUBLIKA ',  64.99, 38, 1);
INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription) VALUES ('Limetic', 'POLPHARMA S.A. ',  24.49, 41, 1);


/* PRESCRIPTIONS */

INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Janina Kowalska', '43012512345', '123456');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Janina Kowalska', '43012512345', '098765');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Janina Kowalska', '43012512345', '754316');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Bogdan Kowal', '71081498765', '584743');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Bogdan Kowal', '71081498765', '079463');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Agata Opal', '78062397653', '086542');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Adam Gruszka', '82120446354', '987643');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Eugenia Sroka', '39041674836', '372846');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Eugenia Sroka', '39041674836', '865426');
INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber) VALUES ('Mieczys³aw Kowalewski', '87021387643', '987654');

/* ORDERS */
-- with prescription
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (17, 1, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (23, 2, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (19, 3, 2);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (18, 4, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (25, 5, 3);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (20, 6, 2);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (21, 7, 3);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (18, 8, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (24, 9, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (21, 10, 2);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (22, 10, 3);
-- without prescription
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (7, null, 3);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (5, null, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (2, null, 4);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (14, null, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (8, null, 2);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (3, null, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (7, null, 1);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (9, null, 2);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (4, null, 3);
INSERT INTO Orders (MedicineID, PrescriptionID, Amount) VALUES (10, null, 1);
