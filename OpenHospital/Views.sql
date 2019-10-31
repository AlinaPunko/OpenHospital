create view selectallusers as select * from users;
create view selectallvisits as select * from visits;
create view selectallpatients as select * from patients;
create view selectalldoctors as select * from doctors;
create view selectallhospitals as select * from hospitals;
create view selectallrooms as select * from rooms;

create view selectdoctorsbyspec as select count(*) as "Количество", doctor_specializations.specialization as "Специализация" from doctors inner join doctor_specializations
on doctors.specialization = doctor_specializations.specialization group by doctor_specializations.specialization;

create view selectdoctorsbycat as select count(*) as "Количество", doctors.category as "Категория"  from doctors inner join doctor_categories 
on doctors.category = doctor_categories.category group by doctors.category;

create view selectvisitsbytype as select count(*), visit_types.type as "Тип" from visits inner join visit_types on visits.type = visit_types.type group by visit_types.type;

