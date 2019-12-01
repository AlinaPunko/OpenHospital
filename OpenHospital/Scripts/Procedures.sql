create or replace procedure selectdoctorsbyspec(spec in doctor_specializations.specialization%type, prc out sys_refcursor) is
begin
open prc for select * from admin.doctors  where specialization in (select id from doctor_specializations where specialization = spec);
end selectdoctorsbyspec;

create or replace procedure selectdoctorsbyname(dname in doctors.name%type, prc out sys_refcursor) is
begin
open prc for select * from admin.doctors  where name=dname;
end selectdoctorsbyname;

create or replace procedure selectspecidbyname(sname in doctor_specializations.specialization%type, prc out sys_refcursor) is
begin
open prc for select * from admin.doctor_specializations where specialization = sname;
end selectspecidbyname;

create or replace procedure selectcatidbycat(cname in doctor_categories.category%type, prc out sys_refcursor) is
begin
open prc for select * from doctor_categories where category=cname;
end selectcatidbycat;

create or replace procedure selectdoctorsbycat(cat in doctor_categories.category%type, prc out sys_refcursor) is
begin
open prc for select * from admin.doctors  where category in (select id from doctor_categories where category = cat);
end selectdoctorsbycat;

create or replace procedure adddoctor ( doctorname in doctors.name%type, doctoraddress doctors.address%type, doctorphone in doctors.phone%type,
doctorcat in doctors.category%TYPE, doctorspec in doctors.specialization%TYPE)
as begin
 insert into admin.doctors ( name, address, phone, category, specialization ) values (doctorname , doctoraddress , doctorphone , doctorcat, 
doctorspec);
end adddoctor;

create or replace procedure updatedoctor (doctorid in doctors.id%type, doctorname in doctors.name%type, doctoraddress doctors.address%type, doctorphone in doctors.phone%type,
doctorcat in doctors.category%TYPE, doctorspec in doctors.specialization%TYPE)
as begin
 update admin.doctors set name = doctorname, address =doctoraddress, phone=doctorphone, category=doctorcat, specialization=doctorspec where id=doctorid;
end updatedoctor;

create or replace procedure register ( username users.login%type, userpassword in users.password%type, doctor in users.doctorid%type, patient in users.patientid%type, role in users.roleid%type)
as begin
insert into admin.users ( login , password, doctorid, patientid, roleid) values (username , userpassword , doctor , patient , role );
end register;

create or replace procedure updateuser (userid in users.id%type, userlogin users.login%type, userpassword in users.password%type)
as begin
update admin.users set login=userlogin, password=userpassword where id=userid;
end updateuser;

create or replace procedure addpatient ( patientname in patients.name%type,patientbirthdate in patients.birthdate%type,
patientaddress in patients.address%type, patientphone in patients.phone%type)
as begin
insert into admin.patients ( name, birthdate, address, phone) values (patientname, patientbirthdate, patientaddress, patientphone );
end addpatient;

create or replace procedure updatepatient ( patientid in patients.id%type, patientname in patients.name%type, patientbirthdate in patients.birthdate%type,
patientaddress in patients.address%type, patientphone in patients.phone%type)
as begin
update admin.patients set name=patientname, birthdate=patientbirthdate, address=patientaddress, phone=patientphone where id=patientid;
end  updatepatient;

create or replace procedure addroom ( roomnumber in rooms.room_number%type, roomtype in rooms.type%type)
as begin
insert into admin.rooms ( room_number, type) values (roomnumber, roomtype );
end addroom;

create or replace procedure updateroom ( roomnumber in rooms.room_number%type, roomtype in rooms.type%type)
as begin
update admin.rooms set type = type where room_number=roomnumber;
end updateroom;

create or replace procedure deleteroom ( roomnumber in rooms.room_number%type)
as begin
delete from admin.rooms where room_number = roomnumber;
end deleteroom;

create or replace procedure deleteuser (userid in users.id%type) as
begin
 delete from admin.users where id = userid;
end deleteuser;

create or replace procedure deleteuserbydoctorID (did in users.doctorID%type) as
begin
 delete from admin.users where doctorid = did;
end deleteuserbydoctorID;

create or replace procedure deleteuserbypatientID (pid in users.patientID%type) as
begin
 delete from admin.users where patientid = pid;
end deleteuserbypatientID;

create or replace procedure deletedoctor (did in doctors.id%type) as
begin
 delete from admin.doctors where id = did;
end deletedoctor;

create or replace procedure deletepatient (pid in patients.id%type) as
begin
 delete from admin.patients where id = pid;
end deletepatient;



create or replace procedure login ( username in users.login%type, userpassword in users.password%type, prc out sys_refcursor) as 
begin
 open prc for select * from admin.users left join admin.patients on users.patientid = patients.id where login = username and password = userpassword;
end login;

create or replace procedure selectvisitsbypatientid( patientid in visits.patient%type, prc out sys_refcursor) as
begin
 open prc for select visits.id, doctors.name, patients.name, visits.datetime, visit_types.type, visits.sympthoms, visits.diagnosis,
 visits.prescription, visits.notes, visits.room from visits inner join visit_types on visits.type = visit_types.id inner join doctors on doctors.id = visits.doctor
 inner join patients on patients.id = visits.patient where patient=patientid;
end selectvisitsbypatientid; 

create or replace procedure getUserByName ( username in users.login%type, prc out sys_refcursor) as 
begin
 open prc for select * from users where login = username ;
end getuserbyname;

create or replace procedure getVisitByID ( did in visits.id%type, prc out sys_refcursor) as 
begin
 open prc for select visits.id, doctors.name, patients.name, visits.datetime, visit_types.type, visits.sympthoms, visits.diagnosis,
 visits.prescription, visits.notes, visits.room from visits inner join visit_types on visits.type = visit_types.id inner join doctors on doctors.id = visits.doctor
 inner join patients on patients.id = visits.patient;
end getvisitbyid;

create or replace procedure getUserByID ( userid in users.id%type, prc out sys_refcursor) as 
begin
 open prc for select * from users where id = userid ;
end getUserByID;
begin

create or replace procedure gettypebyname ( vtype in visit_types.type%type, prc out sys_refcursor) as 
begin
 open prc for select * from visit_types where type=vtype ;
end gettypebyname;

begin
--addpatient ('qwerty', '23.05.2000', 'qwerty', '1234567890'); 
register ('qwertyq', '123',null, 6); 
--register('admin1', 'admin', null, null);
end;

create or replace procedure deletevisit (dname in visits.doctor%type, dt in visits.datetime%type) is
begin
delete from admin.visits where doctor=dname and datetime=dt;
end deletevisit;

create or replace procedure getdoctorbyid (did in doctors.id%type, prc out sys_refcursor) is
begin
open prc for select * from admin.doctors where id = did;
end getdoctorbyid;
create or replace procedure getpatientbyname(pname in patients.name%type, prc out sys_refcursor) is
begin
open prc for select * from admin.patients where name=pname;
end getpatientbyname;

create or replace 
procedure selectvisitsbydoctorid( doctorid in visits.doctor%type, prc out sys_refcursor) as
begin
 open prc for select visits.id, doctors.name, patients.name, visits.datetime, visit_types.type, visits.sympthoms, visits.diagnosis,
 visits.prescription, visits.notes, visits.room from visits inner join patients on visits.patient = patients.id inner join visit_types on visits.type = visit_types.id inner join doctors on doctors.id = visits.doctor where visits.doctor=doctorid;
end selectvisitsbydoctorid;

create or replace procedure addvisit( visitdoctor in visits.doctor%type, visitpatient in visits.patient%type,
visitdatetime in visits.datetime%type, visittype in visits.type%type, visitsympthoms in visits.sympthoms%type, visitdiagnosis in visits.diagnosis%type,
visitprescription in visits.prescription%type, visitnotes in visits.notes%type, visitroom in visits.room%type, visitfile in visits.visit_file%type )
as
begin
insert into admin.visits(doctor, patient, datetime, type, sympthoms, diagnosis,prescription, notes, room, visit_file)
values ( visitdoctor, visitpatient ,visitdatetime , visittype, visitsympthoms , visitdiagnosis ,visitprescription , visitnotes, visitroom, visitfile  );
end addvisit;

create or replace procedure updatevisit(visitid in visits.id%type,  visitdoctor in visits.doctor%type, visitpatient in visits.patient%type,
visitdatetime in visits.datetime%type, visittype in visits.type%type, visitsympthoms in visits.sympthoms%type, visitdiagnosis in visits.diagnosis%type,
visitprescription in visits.prescription%type, visitnotes in visits.notes%type, visitroom in visits.room%type, visitfile in visits.visit_file%type )
as
begin
update admin.visits set doctor=visitdoctor, patient=visitpatient ,datetime=visitdatetime , type=visittype, sympthoms=visitsympthoms,
diagnosis=visitdiagnosis ,prescription=visitprescription , notes=visitnotes, room=visitroom, visit_file=visitfile where id = visitid;
end updatevisit;




create or replace procedure countdoctors (countd out number) is begin
select count(*) into countd from admin.doctors;
end countdoctors;

create or replace procedure countspec (counts out number) is begin
select count(*) into counts from admin.doctor_specializations;
end countspec;

create or replace procedure counthigh (counth out number) is begin
select count(*) into counth from admin.doctors group by category having category='Â';
end counthigh;

create or replace procedure countfirst (counth out number) is begin
select count(*) into counth from admin.doctors group by category having category='1';
end countfirst;

create or replace procedure countsecond (counth out number) is begin
select count(*) into counth from admin.doctors group by specialization having specialization='2';
end countsecond;

create or replace procedure countrooms (counth out number) is begin
select count(*) into counth from admin.rooms;
end countrooms;

create or replace procedure countpatients (counth out number) is begin
select count(*) into counth from admin.patients;
end countpatients;

create or replace procedure countvisits (counth out number) is begin
select count(*) into counth from admin.visits;
end countvisits;

create or replace procedure countvisitsbymonth (counth out number) is begin
select count(*) into counth from admin.visits where extract(month from datetime) = extract(month from sysdate);
end countvisitsbymonth;

select xmlelement("document",
       xmlattributes('testId' as "DocumentID"),
       xmlagg(
              xmlelement("row", xmlforest(t.room_number "Room", t.type "Type"))
             )
                 ) as xmlresult
  from rooms t;

create or replace procedure specmore (counth out number, spec out doctor_specializations.specialization%type) is
begin
select count(*), doctor_specializations.specialization into counth, spec from admin.doctors inner join admin.doctor_specializations on doctors.specialization= doctor_specializations.id  group by doctor_specializations.specialization ;
end specmore;
