create table "Hospital" (
	"ID" nvarchar2(5) not null,
	"Title" nvarchar2(30) not null,
	"Address" nVARCHAR2(50) NOT NULL,
	constraint HOSPITAL_PK PRIMARY KEY ("ID"));


/
CREATE TABLE "Visit_type" (
	"ID" int not null,
	"Type" nVARCHAR2(20) NOT NULL,
	constraint VISIT_TYPE_PK PRIMARY KEY ("ID"));

CREATE sequence "VISIT_TYPE_ID_SEQ";

CREATE trigger "BI_VISIT_TYPE_ID"
  before insert on "Visit_type"
  for each row
begin
  select "VISIT_TYPE_ID_SEQ".nextval into :NEW."ID" from dual;
end;

/
create table "Doctor_category" (
	"ID" nvarchar2(1) not null,
	"Category" nVARCHAR2(15) NOT NULL,
	constraint DOCTOR_CATEGORY_PK PRIMARY KEY ("ID"));


/
CREATE TABLE "Doctor_specialization" (
	"ID" int not null,
	"Specialization" nVARCHAR2(25) NOT NULL,
	constraint DOCTOR_SPECIALIZATION_PK PRIMARY KEY ("ID"));

CREATE sequence "DOCTOR_SPECIALIZATION_ID_SEQ";

CREATE trigger "BI_DOCTOR_SPECIALIZATION_ID"
  before insert on "Doctor_specialization"
  for each row
begin
  select "DOCTOR_SPECIALIZATION_ID_SEQ".nextval into :NEW."ID" from dual;
end;

/
CREATE TABLE "Doctor" (
	"ID" int not null,
	"Name" nvarchar2(30) not null,
	"Address" nvarchar2(30) not null,
	"Phone" nvarchar2(20) not null,
	"Category" nVARCHAR2(1) NOT NULL,
	"Specialization" INT NOT NULL,
	"Salary" decimal not null,
	"Hospital" nVARCHAR2(5) NOT NULL,
	constraint DOCTOR_PK PRIMARY KEY ("ID"));

CREATE sequence "DOCTOR_ID_SEQ";

CREATE trigger "BI_DOCTOR_ID"
  before insert on "Doctor"
  for each row
begin
  select "DOCTOR_ID_SEQ".nextval into :NEW."ID" from dual;
end;

/
CREATE TABLE "User" (
	"ID" int not null,
	"Login" nvarchar2(20) not null,
	"Password" nVARCHAR2(50) NOT NULL,
	"DoctorID" INT ,
	"PaientID" INT ,
	constraint USER_PK PRIMARY KEY ("ID"));

CREATE sequence "USER_ID_SEQ";

CREATE trigger "BI_USER_ID"
  before insert on "User"
  for each row
begin
  select "USER_ID_SEQ".nextval into :NEW."ID" from dual;
end;

/
create table "Room_type" (
	"ID" nvarchar2(3) not null,
	"Type" nVARCHAR2(20) NOT NULL,
	constraint ROOM_TYPE_PK PRIMARY KEY ("ID"));


/
create table "Room" (
	"Hospital" nvarchar2(5) not null,
	"Number" nvarchar2(10) not null,
	"Type" nVARCHAR2(10) NOT NULL,
	constraint ROOM_PK PRIMARY KEY ("Hospital","Number"));


/
CREATE TABLE "Patient" (
	"ID" int not null,
	"name" nVARCHAR2(30) NOT NULL,
	"Birthdate" date not null,
	"Address" nvarchar2(50) not null,
	"Phone" nVARCHAR2(20) NOT NULL,
	constraint PATIENT_PK PRIMARY KEY ("ID"));

CREATE sequence "PATIENT_ID_SEQ";

CREATE trigger "BI_PATIENT_ID"
  before insert on "Patient"
  for each row
begin
  select "PATIENT_ID_SEQ".nextval into :NEW."ID" from dual;
end;

/
CREATE TABLE "Visit" (
	"Doctor" INT NOT NULL,
	"Patient" int not null,
	"DateTime" DATE NOT NULL,
	"Type" int not null,
	"Sympthoms" nvarchar2(200),
	"Diagnosis" nvarchar2(200),
	"Prescription" nvarchar2(200),
	"Notes" nvarchar2(200),
	"Room" nvarchar2(10) not null,
	"Hospital" nvarchar2(5) not null,
	constraint VISIT_PK PRIMARY KEY ("Doctor","DateTime"));

drop table visits;
/




ALTER TABLE "Doctor" ADD CONSTRAINT "Doctor_fk0" FOREIGN KEY ("Category") REFERENCES "Doctor_category"("ID");
ALTER TABLE "Doctor" ADD CONSTRAINT "Doctor_fk1" FOREIGN KEY ("Specialization") REFERENCES "Doctor_specialization"("ID");
ALTER TABLE "Doctor" ADD CONSTRAINT "Doctor_fk2" FOREIGN KEY ("Hospital") REFERENCES "Hospital"("ID");

ALTER TABLE "User" ADD CONSTRAINT "User_fk0" FOREIGN KEY ("DoctorID") REFERENCES "Doctor"("ID");
ALTER TABLE "User" ADD CONSTRAINT "User_fk1" FOREIGN KEY ("PaientID") REFERENCES "Patient"("ID");


ALTER TABLE "Room" ADD CONSTRAINT "Room_fk0" FOREIGN KEY ("Hospital") REFERENCES "Hospital"("ID");
ALTER TABLE "Room" ADD CONSTRAINT "Room_fk1" FOREIGN KEY ("Type") REFERENCES "Room_type"("ID");


ALTER TABLE "Visit" ADD CONSTRAINT "Visit_fk0" FOREIGN KEY ("Doctor") REFERENCES "Doctor"("ID");
ALTER TABLE "Visit" ADD CONSTRAINT "Visit_fk1" FOREIGN KEY ("Patient") REFERENCES "Patient"("ID");
alter table "Visit" add constraint "Visit_fk2" foreign key ("Type") references "Visit_type"("ID");
ALTER TABLE "Visit" ADD CONSTRAINT "Visit_fk3" FOREIGN KEY ("Room", "Hospital") REFERENCES "Room"("Number", "Hospital");
