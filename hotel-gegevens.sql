-- Invoegen in Customer tabel
INSERT INTO "Customer" (name, email, phone, address, status)
VALUES 
('Jan Peeters', 'jan.peeters@email.com', '0470123456', 'Brussel | 1000 | Hoofdstraat | 123', 1),
('Eva Willems', 'eva.willems@email.com', '0470567890', 'Gent | 9000 | Koningstraat | 11', 1),
('Lucas De Vries', 'lucas.devries@email.com', '0470654789', 'Antwerpen | 2000 | Meir | 22', 0),
('Marie Dujardin', 'marie.dujardin@email.com', '0470123987', 'Leuven | 3000 | Diestsestraat | 33', 1),
('Sophie Martens', 'sophie.martens@email.com', '0470456781', 'Hasselt | 3500 | Grote Markt | 77', 1),
('Olivier Dupont', 'olivier.dupont@email.com', '0470234567', 'Luik | 4000 | Rue de la Loi | 88', 0),
('Anaïs Leroy', 'anais.leroy@email.com', '0470345678', 'Namen | 5000 | Boulevard de lEurope | 99', 1);

-- Invoegen in Organizer tabel
INSERT INTO "Organizer" (name, email, phone, address)
VALUES 
('Organisatie XYZ', 'contact@organisatiexyz.be', '0470654321', 'Antwerpen | 2000 | Parklaan | 45'),
('Event Masters', 'info@eventmasters.be', '0470987654', 'Brussel | 1000 | Zavelstraat | 44'),
('Fun Adventures', 'contact@funadventures.be', '0470123456', 'Brugge | 8000 | Hoogstraat | 55'),
('Culture Club', 'info@cultureclub.be', '0470777788', 'Gent | 9000 | Cultuurstraat | 101'),
('Nature Walks', 'contact@naturewalks.be', '0470666699', 'Ardennen | 4900 | Bosweg | 202');

-- Invoegen in Activity tabel
INSERT INTO "Activity" (name, description, eventdatetime, duration, location, availablespots, adultcost, childcost, discount, adultage, organizerId)
VALUES 
('Stadswandeling Gent', 'Verken de historische stad Gent met een ervaren gids.', '2024-05-15 10:00:00', 120, 'Gent | 9000 | Centrum | 1', 20, 15.00, 10.00, 0.05, 18, 1),
('Fietstocht Brugge', 'Ontdek de historische stad Brugge op de fiets.', '2024-06-20 09:00:00', 180, 'Brugge | 8000 | Markt | 1', 15, 20.00, 15.00, 0.05, 12, 2),
('Kookworkshop', 'Leer traditionele Belgische gerechten koken.', '2024-07-05 18:00:00', 240, 'Antwerpen | 2000 | Keukenstraat | 66', 10, 50.00, 0.00, 0.00, 18, 2),
('Bierproeverij', 'Proef verschillende Belgische bieren.', '2024-08-10 17:00:00', 120, 'Leuven | 3000 | Brouwerijplein | 2', 20, 30.00, 0.00, 0.00, 18, 3),
('Natuurwandeling Ardennen', 'Geniet van een wandeling in de prachtige Ardennen.', '2024-09-15 08:00:00', 240, 'Ardennen | 4900 | Natuurpad | 3', 15, 25.00, 12.50, 0.05, 12, 4);

-- Invoegen in Member tabel
INSERT INTO "Member" (name, birthday, CustomerId, status)
VALUES 
('Sofie Janssens', '1990-04-01', 1, 1),
('Thomas Jansen', '1985-08-15', 2, 1),
('Laura Baert', '1992-03-21', 3, 1),
('Bart De Smet', '1980-12-05', 4, 1),
('Clara Vandenbroucke', '1995-05-20', 5, 1);

-- Invoegen in Registration tabel
INSERT INTO "Registration" (activityId, customerId, cost)
VALUES 
(1, 1, 15.00),
(2, 2, 20.00),
(2, 3, 20.00),
(3, 1, 50.00),
(4, 4, 30.00),
(4, 5, 30.00),
(5, 2, 25.00);
