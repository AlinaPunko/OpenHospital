create role doctor_role;
grant create session to doctor_role;
grant execute on deletevisit to doctor_role;
grant execute on getvisitbyid to doctor_role;
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
grant execute on selectdoctorsbycat to doctor_role;
grant execute on selectdoctorsbyspec to doctor_role;
grant doctor_role to doctor;

create role patient_role;
grant create session to patient_role;
grant execute on getpatientbyid to patient_role;
grant execute on selectvisitsbypatientid to patient_role;
grant patient_role to patient;

create profile PF_USER limit
password_life_time 180
sessions_per_user 2
failed_login_attempts 7
password_lock_time 1
password_reuse_time 10
password_grace_time default
connect_time 18000
idle_time 30;
