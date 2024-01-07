CREATE TABLE "Registration"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "activityId" INT NOT NULL,
    "customerId" INT NOT NULL,
    "cost" DECIMAL(8, 2) NOT NULL
);
ALTER TABLE
    "Registration" ADD CONSTRAINT "registration_id_primary" PRIMARY KEY("id");
CREATE TABLE "Customer"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "name" VARCHAR(500) NOT NULL,
    "email" VARCHAR(500) NOT NULL,
    "phone" VARCHAR(50) NOT NULL,
    "address" VARCHAR(500) NOT NULL,
    "status" BIT NOT NULL
);
ALTER TABLE
    "Customer" ADD CONSTRAINT "customer_id_primary" PRIMARY KEY("id");
CREATE TABLE "Organizer"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "name" VARCHAR(500) NOT NULL,
    "email" VARCHAR(500) NOT NULL,
    "phone" VARCHAR(50) NOT NULL,
    "address" VARCHAR(500) NOT NULL
);
ALTER TABLE
    "organizer" ADD CONSTRAINT "organizer_id_primary" PRIMARY KEY("id");
CREATE TABLE "Member"(
    "name" VARCHAR(500) NOT NULL,
    "birthday" datetime2(7) NOT NULL,
    "CustomerId" INT NOT NULL,
    "status" BIT NOT NULL
);
ALTER TABLE
    "Member" ADD CONSTRAINT "member_name_primary" PRIMARY KEY("name");
CREATE TABLE "Activity"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "name" VARCHAR(500) NOT NULL,
    "description" VARCHAR(500) NOT NULL,
    "eventdatetime" DATETIME NOT NULL,
    "duration" INT NOT NULL,
    "location" VARCHAR(500) NOT NULL,
    "availablespots" INT NOT NULL,
    "adultcost" DECIMAL(8, 2) NOT NULL,
    "childcost" DECIMAL(8, 2) NOT NULL,
    "discount" DECIMAL(8, 2) NOT NULL,
    "adultage" INT NOT NULL,
    "organizerId" INT NULL
);
ALTER TABLE
    "Activity" ADD CONSTRAINT "activity_id_primary" PRIMARY KEY("id");
ALTER TABLE
    "Member" ADD CONSTRAINT "member_customerid_foreign" FOREIGN KEY("CustomerId") REFERENCES "Customer"("id");
ALTER TABLE
    "Registration" ADD CONSTRAINT "registration_customerid_foreign" FOREIGN KEY("customerId") REFERENCES "Customer"("id");
ALTER TABLE
    "Activity" ADD CONSTRAINT "activity_organizerid_foreign" FOREIGN KEY("organizerId") REFERENCES "organizer"("id");
ALTER TABLE
    "Registration" ADD CONSTRAINT "registration_activityid_foreign" FOREIGN KEY("activityId") REFERENCES "Activity"("id");