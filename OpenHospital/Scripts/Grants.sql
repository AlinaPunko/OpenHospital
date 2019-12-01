create role doctor_role;
grant create session to doctor_role;
grant execute on deletevisit to doctor_role;
grant execute on getvisitbyid to doctor_role;
grant execute on gettypebyname to doctor_role;
grant execute on updatedoctor to doctor_role;
grant select on selectalldoctors to doctor_role;
grant select on selectallpatients to doctor_role;
grant select on selectallvisits to doctor_role;
grant execute on selectvisitsbydoctorid to doctor_role;
grant execute on getpatientbyid to doctor_role;
grant execute on selectvisitsbypatientid to doctor_role;
grant execute on getpatientbyname to doctor_role;
grant execute on getdoctorsbyname to doctor_role;
grant execute on selectcatidbycat to doctor_role;
grant execute on selectspecidbyname to doctor_role;
grant execute on countdoctors to doctor_role;
grant execute on countpatients to doctor_role;
grant execute on countvisits to doctor_role;
grant execute on countspec to doctor_role;
grant execute on counthigh to doctor_role;
grant execute on countfirst to doctor_role;
grant execute on countsecond to doctor_role;
grant execute on countrooms to doctor_role;
grant execute on countretiree to doctor_role;
grant execute on countinfants to doctor_role;
grant execute on countvisitsbymonth to doctor_role;
grant doctor_role to doctor;

create role patient_role;
grant create session to patient_role;
grant execute on getpatientbyid to patient_role;
grant execute on selectvisitsbypatientid to patient_role;
grant execute on countdoctors to patient_role;
grant execute on countpatients to patient_role;
grant execute on countvisits to patient_role;
grant execute on countspec to patient_role;
grant execute on counthigh to patient_role;
grant execute on countfirst to patient_role;
grant execute on countsecond to patient_role;
grant execute on countrooms to patient_role;
grant execute on countretiree to patient_role;
grant execute on countinfants to patient_role;
grant execute on countvisitsbymonth to patient_role;
grant patient_role to patient;

create profile PF_USER_Patient limit
password_life_time unlimited
sessions_per_user unlimited
failed_login_attempts 7
password_lock_time 1
password_reuse_time 10
password_grace_time default
connect_time 18000
idle_time 30;

create profile pf_user_doctor limit
password_life_time unlimited
sessions_per_user unlimited
failed_login_attempts 7
password_lock_time 1
password_reuse_time 10
password_grace_time default
connect_time unlimited
idle_time 30;

Create Tablespace TS_HOSPITAL
Datafile 'E:\лабы\OpenHospital\OpenHospital\OpenHospital\TS_HOSPITAL.dbf'
size 50 m
autoextend on next 2m
MAXSIZE 200M
EXTENT MANAGEMENT LOCAL;

Create Temporary Tablespace TS_HOSPITAL_TEMP
tempfile 'E:\лабы\OpenHospital\OpenHospital\OpenHospital\TS_HOSPITAL_TEMP.dbf'
size 50 m
autoextend on next 500m
maxsize 200M
extent management local;

 
create user patient identified by 123
default tablespace ts_hospital quota 100m on ts_hospital
temporary tablespace ts_hospital_temp
profile PF_USER_Patient
ACCOUNT UNLOCK ;

-- ROLES
grant patient_role to patient;

create user doctor identified by 123
default tablespace ts_hospital quota 200m on ts_hospital
temporary tablespace ts_hospital_temp
profile PF_USER_doctor
ACCOUNT UNLOCK ;

-- ROLES
grant doctor_role to doctor;
