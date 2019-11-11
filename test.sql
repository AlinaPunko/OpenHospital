create table testmm ( id int, img ordimage);
insert into testmm ( id, img) values (3, ordimage.init('FILE', 'D:/direct', '1.bmp'));
select * from testmm;
ALTER TABLE DOCTOR_SPECIALIZATIONS MODIFY(ID GENERATED AS IDENTITY (START WITH 1));
declare
  imag ORDImage;
  ctx RAW(64) := NULL;
begin
  select  img
    into  imag 
    from testmm
    where id = 3;
  imag.export(ctx, 'FILE', 'D:\direct1', '1.bmp');
END;
create directory media_dir as
    'd:/direct';
grant write on directory media_dir to sys;
GRANT read ON DIRECTORY media_dir TO sys;


declare
  image ORDImage;
  ctx RAW(64) := NULL;
BEGIN
  -- Insert a new row into the pm.online_media table.
  insert into testmm
         (id, img)
  values (1000, ORDImage.init('FILE', 'media_dir', '1.bmp'))
  returning img
  INTO image;
  
  -- Bring the media into the database and populate the attributes.
  image.import(ctx);
  
  commit;
END;