create or replace 
PROCEDURE EDIT_CLIENT(
FIRSTNAME1 VARCHAR,
SECONDNAME1 VARCHAR,
LOGIN1 VARCHAR,
PASSWORD1 VARCHAR,
client_id_dataclient number,
client_id_client number,
client_weight number,
client_height number,
client_bodytype VARCHAR,
client_progress_power float
)
AS 
BEGIN
declare
A1_1 DATACLIENT%rowtype;
A1_2 DATACLIENT%rowtype;
progess_weight number;
begin
 SELECT * into A1_1
  FROM (SELECT * FROM DATACLIENT where(ID_CLIENT=client_id_client) ORDER BY DATETIME ASC )
 WHERE ROWNUM = 1;
  SELECT * into A1_2
  FROM (SELECT * FROM DATACLIENT where(ID_CLIENT=client_id_client) ORDER BY DATETIME DESC )
 WHERE ROWNUM = 1;
 progess_weight:=(A1_1.Weight/A1_2.Weight);
 dbms_output.put_line(progess_weight);
insert into dataclient values(client_id_dataclient,client_id_client,client_weight,client_height,client_bodytype,progess_weight,client_progress_power,CURRENT_DATE);
UPDATE client SET firstname=FIRSTNAME1,secondname=SECONDNAME1,login=LOGIN1,password=PASSWORD1 where(id_client=client_id_client);
end;
END EDIT_CLIENT;

create or replace 
PROCEDURE EDIT_EXERCISES_FOR_CLIENT(
ID_CLIENT1 number,
EXERCISES1 varchar,
ID_EXERCISES1 number
)
AS 
BEGIN
UPDATE exercisesforclient SET EXERCISES=EXERCISES1 where(id_client=ID_CLIENT1 and ID_EXERCISES=ID_EXERCISES1);
END EDIT_EXERCISES_FOR_CLIENT;

create or replace 
PROCEDURE EDIT_GROUP_FOR_CLIENT(
IDCLIENT1 number,
NUMBER_GROUP1 number
)
AS 
BEGIN
UPDATE client SET number_group=NUMBER_GROUP1 where(id_client=IDCLIENT1);
END EDIT_GROUP_FOR_CLIENT;

create or replace 
PROCEDURE EDIT_TRAINER_GROUP(
IDTRAINER1 number,
number_group1 number
)
AS 
BEGIN
UPDATE trainer SET number_group=number_group1 where(id_trainer=IDTRAINER1);
END;

create or replace 
PROCEDURE EXPORT_XML AS 
BEGIN
DECLARE 
 cSQLclient VARCHAR2(32000) := 'select * from client';
 cSQLdataclient VARCHAR2(32000) := 'select * from dataclient';
 cSQLdatatrainer VARCHAR2(32000) := 'select * from datatrainer';
 cSQLexercises VARCHAR2(32000) := 'select * from exercises';
 cSQLexercisesforclient VARCHAR2(32000) := 'select * from exercisesforclient';
 cSQLgroupforclient VARCHAR2(32000) := 'select * from groupforclient';
 cSQLmessages VARCHAR2(32000) := 'select * from messages';
 cSQLresultexercises VARCHAR2(32000) := 'select * from resultexercises';
 cSQLtrainer VARCHAR2(32000) := 'select * from trainer';
 cSQLworkout VARCHAR2(32000) := 'select * from workout';
  resultclient clob;
  resultdataclient clob;
  resultdatatrainer clob;
  resultexercises clob;
  resultexercisesforclient clob;
  resultgroupforclient clob;
  resultmessages clob;
  resultresultexercises clob;
  resulttrainer clob;
  resultworkout clob;
  qryCtx1 dbms_xmlgen.ctxHandle;
  qryCtx2 dbms_xmlgen.ctxHandle;
  qryCtx3 dbms_xmlgen.ctxHandle;
  qryCtx4 dbms_xmlgen.ctxHandle;
  qryCtx5 dbms_xmlgen.ctxHandle;
  qryCtx6 dbms_xmlgen.ctxHandle;
  qryCtx7 dbms_xmlgen.ctxHandle;
  qryCtx8 dbms_xmlgen.ctxHandle;
  qryCtx9 dbms_xmlgen.ctxHandle;
  qryCtx10 dbms_xmlgen.ctxHandle;
  f1 utl_file.file_type;
  f2 utl_file.file_type;
  f3 utl_file.file_type;
  f4 utl_file.file_type;
  f5 utl_file.file_type;
  f6 utl_file.file_type;
  f7 utl_file.file_type;
  f8 utl_file.file_type;
  f9 utl_file.file_type;
  f10 utl_file.file_type;
    l_buffer  VARCHAR2(32767);
  l_amount  BINARY_INTEGER := 32767;
  l_pos     INTEGER :=1;
BEGIN
  qryCtx1 := DBMS_XMLGEN.newContext(cSQLclient);
  DBMS_XMLGEN.setRowSetTag(qryCtx1, 'client');
  DBMS_XMLGEN.setRowTag(qryCtx1, 'columns');
  resultclient := DBMS_XMLGen.getXML(qryCtx1);
  DBMS_XMLGen.CloseContext(qryCtx1);
  
    qryCtx2 := DBMS_XMLGEN.newContext(cSQLdataclient);
  DBMS_XMLGEN.setRowSetTag(qryCtx2, 'dataclient');
  DBMS_XMLGEN.setRowTag(qryCtx2, 'columns');
  resultdataclient := DBMS_XMLGen.getXML(qryCtx2);
  DBMS_XMLGen.CloseContext(qryCtx2);
  
    qryCtx3 := DBMS_XMLGEN.newContext(cSQLdatatrainer);
  DBMS_XMLGEN.setRowSetTag(qryCtx3, 'datatrainer');
  DBMS_XMLGEN.setRowTag(qryCtx3, 'columns');
  resultdatatrainer := DBMS_XMLGen.getXML(qryCtx3);
  DBMS_XMLGen.CloseContext(qryCtx3);
  
    qryCtx4 := DBMS_XMLGEN.newContext(cSQLexercises);
  DBMS_XMLGEN.setRowSetTag(qryCtx4, 'exercises');
  DBMS_XMLGEN.setRowTag(qryCtx4, 'columns');
  resultexercises := DBMS_XMLGen.getXML(qryCtx4);
  DBMS_XMLGen.CloseContext(qryCtx4);
  
    qryCtx5 := DBMS_XMLGEN.newContext(cSQLexercisesforclient);
  DBMS_XMLGEN.setRowSetTag(qryCtx5, 'exercisesforclient');
  DBMS_XMLGEN.setRowTag(qryCtx5, 'columns');
  resultexercisesforclient := DBMS_XMLGen.getXML(qryCtx5);
  DBMS_XMLGen.CloseContext(qryCtx5);
  
    qryCtx6 := DBMS_XMLGEN.newContext(cSQLgroupforclient);
  DBMS_XMLGEN.setRowSetTag(qryCtx6, 'groupforclient');
  DBMS_XMLGEN.setRowTag(qryCtx6, 'columns');
  resultgroupforclient := DBMS_XMLGen.getXML(qryCtx6);
  DBMS_XMLGen.CloseContext(qryCtx6);
  
    qryCtx7 := DBMS_XMLGEN.newContext(cSQLmessages);
  DBMS_XMLGEN.setRowSetTag(qryCtx7, 'messages');
  DBMS_XMLGEN.setRowTag(qryCtx7, 'columns');
  resultmessages := DBMS_XMLGen.getXML(qryCtx7);
  DBMS_XMLGen.CloseContext(qryCtx7);
  
    qryCtx8 := DBMS_XMLGEN.newContext(cSQLresultexercises);
  DBMS_XMLGEN.setRowSetTag(qryCtx8, 'resultexercises');
  DBMS_XMLGEN.setRowTag(qryCtx8, 'columns');
  resultresultexercises := DBMS_XMLGen.getXML(qryCtx8);
  DBMS_XMLGen.CloseContext(qryCtx8);
  
      qryCtx9 := DBMS_XMLGEN.newContext(cSQLtrainer);
  DBMS_XMLGEN.setRowSetTag(qryCtx9, 'trainer');
  DBMS_XMLGEN.setRowTag(qryCtx9, 'columns');
  resulttrainer := DBMS_XMLGen.getXML(qryCtx9);
  DBMS_XMLGen.CloseContext(qryCtx9);
  
        qryCtx10 := DBMS_XMLGEN.newContext(cSQLworkout);
  DBMS_XMLGEN.setRowSetTag(qryCtx10, 'workout');
  DBMS_XMLGEN.setRowTag(qryCtx10, 'columns');
  resultworkout := DBMS_XMLGen.getXML(qryCtx10);
  DBMS_XMLGen.CloseContext(qryCtx10);
  
  --CREATE OR REPLACE DIRECTORY EXTRACT_DIR AS 'C:\Files\KursProject\XML';
  
      begin
    l_amount := 32767;
    l_pos :=1;
    f1 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_CLIENT_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultclient, l_amount, l_pos, l_buffer);
utl_file.put_line(f1, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f1);
  end;
  
      begin
    l_amount := 32767;
    l_pos :=1;
    f2 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_DATACLIENT_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultdataclient, l_amount, l_pos, l_buffer);
utl_file.put_line(f2, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f2);
  end;
  
  begin
      l_amount := 32767;
    l_pos :=1;
    f3 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_DATATRAINER_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultdatatrainer, l_amount, l_pos, l_buffer);
    utl_file.put_line( f3, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f3);
  end;
  
    begin
    l_amount := 32767;
    l_pos :=1;
    f4 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_EXERCISES_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultexercises, l_amount, l_pos, l_buffer);
utl_file.put_line(f4, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f4);
  end;
  
    begin
    l_amount := 32767;
    l_pos :=1;
    f5 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_EXERCISESFORCLIENT_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultexercisesforclient, l_amount, l_pos, l_buffer);
utl_file.put_line(f5, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f5);
  end;
  
    begin
    l_amount := 32767;
    l_pos :=1;
    f6 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_GROUPFORCLIENT_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultgroupforclient, l_amount, l_pos, l_buffer);
utl_file.put_line(f6, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f6);
  end;
  
    begin
    l_amount := 32767;
    l_pos :=1;
    f7 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_MESSAGES_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultmessages, l_amount, l_pos, l_buffer);
utl_file.put_line(f7, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f7);
  end;
  
    begin
    l_amount := 32767;
    l_pos :=1;
f8 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_RESULTEXERCISES_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultresultexercises, l_amount, l_pos, l_buffer);
utl_file.put_line(f8, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f8);
  end;
  
  begin
    l_amount := 32767;
    l_pos :=1;
      f9 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_TRAINER_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resulttrainer, l_amount, l_pos, l_buffer);
    utl_file.put_line( f9, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f9);
  end;
  
        begin
    l_amount := 32767;
    l_pos :=1;
    f10 := utl_file.fopen('EXTRACT_DIR', 'EXPORT_WORKOUT_TO_XML.xml', 'w');
      LOOP
    DBMS_LOB.read (resultworkout, l_amount, l_pos, l_buffer);
utl_file.put_line(f10, l_buffer);
    l_pos := l_pos + l_amount;
      END LOOP;
      EXCEPTION
  WHEN OTHERS THEN
  utl_file.fclose(f10);
  end;
  END;
END EXPORT_XML;

create or replace 
PROCEDURE FIND_EXERCISES_FOR_CLIENT
(exerforclient OUT SYS_REFCURSOR,
ident number) 
AS
BEGIN 
OPEN exerforclient FOR  SELECT * FROM EXERCISESFORCLIENT where(ident=ID_CLIENT) ORDER BY ID_EXERCISES ASC;
END;

create or replace 
PROCEDURE FIND_INFO_CLIENT 
(curs_client OUT SYS_REFCURSOR,
login1 varchar,
password1 varchar) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM client where(LOGIN=login1)and(PASSWORD=password1);
END FIND_INFO_CLIENT;

create or replace 
PROCEDURE FIND_INFO_CLIENT_FOR_ID 
(curs_client OUT SYS_REFCURSOR,
id_client1 varchar) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM client where(id_client=id_client1);
END FIND_INFO_CLIENT_FOR_ID;

create or replace 
PROCEDURE FIND_INFO_DATACLIENT  
(curs_client OUT SYS_REFCURSOR,
Id_client1 number) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM Dataclient where(Id_client=Id_client1)ORDER BY DATETIME desc;
END FIND_INFO_DATACLIENT;

create or replace 
PROCEDURE GET_CLIENT_FOR_CHAT
(curs_trainer OUT SYS_REFCURSOR,
LOGIN1 varchar) 
AS
BEGIN 
OPEN curs_trainer FOR SELECT * FROM CLIENT where(login=LOGIN1);
END;

create or replace 
PROCEDURE GET_EXERCISESFORCLIENT
(exerforclient OUT SYS_REFCURSOR) 
AS
BEGIN 
OPEN exerforclient FOR SELECT * FROM EXERCISESFORCLIENT;
END GET_EXERCISESFORCLIENT;

create or replace 
PROCEDURE GET_INFO_CLIENT
(curs_client OUT SYS_REFCURSOR) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM client;
END;

create or replace 
PROCEDURE GET_INFO_CLIENT_FORGROUP
(curs_client OUT SYS_REFCURSOR,
numbergroup number) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM client where(number_group=numbergroup);
END;

create or replace 
PROCEDURE GET_INFO_DATACLIENT
(curs_client OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM dataclient;
END;

create or replace 
PROCEDURE GET_INFO_EXERCISES
(exer OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN exer FOR SELECT * FROM exercises;
END;

create or replace 
PROCEDURE GET_INFO_EXERCISESFORCLIENT
(exerforclient OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN exerforclient FOR SELECT * FROM EXERCISESFORCLIENT;
END;

create or replace 
PROCEDURE GET_INFO_GROUPFORCLIENT
(curs OUT SYS_REFCURSOR) 
AS
BEGIN 
OPEN curs FOR SELECT * FROM groupforclient;
END;

create or replace 
PROCEDURE GET_INFO_MESSAGES
(curs_client OUT SYS_REFCURSOR) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM messages;
END GET_INFO_MESSAGES;

create or replace 
PROCEDURE GET_INFO_RESULTEXERCISES
(exer OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN exer FOR SELECT * FROM RESULTEXERCISES;
END GET_INFO_RESULTEXERCISES;

create or replace 
PROCEDURE GET_INFO_TRAINER
(curs_trainer OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN curs_trainer FOR SELECT * FROM trainer;
END;

create or replace 
PROCEDURE GET_INFO_TRAINER_FORGROUP
(curs_client OUT SYS_REFCURSOR,
numbergroup number) 
AS
BEGIN 
OPEN curs_client FOR SELECT * FROM trainer where(number_group=numbergroup);
END;

create or replace 
PROCEDURE GET_INFO_WORKOUT
(curs_trainer OUT SYS_REFCURSOR ) 
AS
BEGIN 
OPEN curs_trainer FOR SELECT * FROM workout;
END GET_INFO_WORKOUT;

create or replace 
PROCEDURE GET_TRAINER_FOR_CHAT
(curs_trainer OUT SYS_REFCURSOR,
LOGIN1 varchar) 
AS
BEGIN 
OPEN curs_trainer FOR SELECT * FROM trainer where(login=LOGIN1);
END;

create or replace 
PROCEDURE IMPORT_XML_CLIENT AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_CLIENT_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
  
insert into xml_tmp values (xmltype(cSQLclient));

insert into client
select FIRSTNAME,SECONDNAME,LOGIN,PASSWORD,ID_CLIENT,NUMBER_GROUP
from xml_tmp x, xmltable('/client/columns'
passing x.xml_data columns FIRSTNAME VARCHAR2(50 BYTE), SECONDNAME VARCHAR2(50 BYTE),
LOGIN VARCHAR2(50 BYTE),PASSWORD VARCHAR2(50 BYTE),ID_CLIENT NUMBER,NUMBER_GROUP NUMBER
) xt;
delete from xml_tmp;
END;
END IMPORT_XML_CLIENT;

create or replace 
PROCEDURE IMPORT_XML_DATACLIENT AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_DATACLIENT_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into DATACLIENT
select ID_DATACLIENT,ID_CLIENT,WEIGHT,HEIGHT,BODYTYPE,PROGRESS_WEIGHT,PROGRESS_POWER,DATETIME
from xml_tmp x, xmltable('/dataclient/columns'
passing x.xml_data columns ID_DATACLIENT NUMBER(38,0),ID_CLIENT NUMBER(38,0), WEIGHT NUMBER(38,0),HEIGHT NUMBER(38,0),
BODYTYPE VARCHAR2(50 BYTE),PROGRESS_WEIGHT FLOAT,PROGRESS_POWER FLOAT,DATETIME Date) xt;
delete from xml_tmp;
END;
END IMPORT_XML_DATACLIENT;

create or replace 
PROCEDURE IMPORT_XML_DATATRAINER AS 
BEGIN
DECLARE 
 cSQLclient clob;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_DATATRAINER_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into DATATRAINER
select id_trainer,WEIGHT,HEIGHT,BARBELLSQUAT,DEADLIFT,BENCHPRESS,PULLUPS,BODYTYPE
from xml_tmp x, xmltable('/datatrainer/columns'
passing x.xml_data columns id_trainer NUMBER(38,0), WEIGHT NUMBER(38,0),HEIGHT NUMBER(38,0),
BARBELLSQUAT NUMBER(38,0),DEADLIFT NUMBER(38,0),BENCHPRESS NUMBER(38,0),
PULLUPS NUMBER(38,0),BODYTYPE VARCHAR2(50 BYTE)) xt;
delete from xml_tmp;
END;
END IMPORT_XML_DATATRAINER;

create or replace 
PROCEDURE IMPORT_XML_EXERCISES AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_EXERCISES_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into exercises
select EXERCISES,MUSCLEGROUPS,PRIORETY
from xml_tmp x, xmltable('/exercises/columns'
passing x.xml_data columns EXERCISES VARCHAR2(50 BYTE), MUSCLEGROUPS VARCHAR2(20 BYTE),
PRIORETY NUMBER(38,0)) xt;
delete from xml_tmp;
END;
END IMPORT_XML_EXERCISES;

create or replace 
PROCEDURE IMPORT_XML_EXERCISESFORCLIENT AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_EXERCISESFORCLIENT_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into EXERCISESFORCLIENT
select ID_CLIENT,ID_EXERCISES,EXERCISES,MUSCLEGROUPS,DAYFOREXERCISE,PRIORETY
from xml_tmp x, xmltable('/exercisesforclient/columns'
passing x.xml_data columns ID_CLIENT NUMBER(38,0), ID_EXERCISES NUMBER(38,0),EXERCISES VARCHAR2(50 BYTE),
MUSCLEGROUPS VARCHAR2(50 BYTE),DAYFOREXERCISE VARCHAR2(50 BYTE),PRIORETY NUMBER(38,0)) xt;
delete from xml_tmp;
END;
END IMPORT_XML_EXERCISESFORCLIENT;

create or replace 
PROCEDURE IMPORT_XML_GROUPFORCLIENT AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_GROUPFORCLIENT_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);

insert into xml_tmp values (xmltype(cSQLclient));

insert into groupforclient
select NUMBER_GROUP,ID_TRAINER
from xml_tmp x, xmltable('/groupforclient/columns'
passing x.xml_data columns NUMBER_GROUP NUMBER(38,0), ID_TRAINER NUMBER(38,0)
) xt;
delete from xml_tmp;
END;
END IMPORT_XML_GROUPFORCLIENT;

create or replace 
PROCEDURE IMPORT_XML_MESSAGES AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_MESSAGES_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into messages
select MESSAGES,ID_MESSAGE,WHO_SENDER,WHO_RECIPIENT,GROUPMESSAGE,DATETIME
from xml_tmp x, xmltable('/messages/columns'
passing x.xml_data columns MESSAGES VARCHAR2(300 BYTE), ID_MESSAGE NUMBER(38,0),
WHO_SENDER NUMBER(38,0),WHO_RECIPIENT NUMBER(38,0),GROUPMESSAGE NUMBER(38,0),DATETIME DATE) xt;
delete from xml_tmp;
END;
END IMPORT_XML_MESSAGES;

create or replace 
PROCEDURE IMPORT_XML_RESULTEXERCISES AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_RESULTEXERCISES_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);
insert into xml_tmp values (xmltype(cSQLclient));

insert into resultexercises
select NUMBERWORKOUT,ID_EXERCISES,EXERCISES,WEIGHT_FOR_EXERCISES,QQUANTITY,DAYFOREXERCISE,
MUSCLEGROUPS,PRIORETY,DATETIME
from xml_tmp x, xmltable('/resultexercises/columns'
passing x.xml_data columns NUMBERWORKOUT NUMBER(38,0),ID_EXERCISES NUMBER(38,0),
EXERCISES VARCHAR2(50 BYTE),WEIGHT_FOR_EXERCISES NUMBER(38,0),QQUANTITY NUMBER(38,0),
DAYFOREXERCISE VARCHAR2(50 BYTE),MUSCLEGROUPS VARCHAR2(50 BYTE),PRIORETY NUMBER(38,0),DATETIME Date) xt;
delete from xml_tmp;
END;
END IMPORT_XML_RESULTEXERCISES;

create or replace 
PROCEDURE IMPORT_XML_TRAINER AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_TRAINER_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);

insert into xml_tmp values (xmltype(cSQLclient));

insert into TRAINER
select FIRSTNAME,SECONDNAME,ID_TRAINER,PASSWORD,LOGIN,NUMBER_GROUP
from xml_tmp x, xmltable('/trainer/columns'
passing x.xml_data columns FIRSTNAME VARCHAR2(50 BYTE), SECONDNAME VARCHAR2(50 BYTE),ID_TRAINER NUMBER(38,0),
PASSWORD VARCHAR2(50 BYTE),LOGIN VARCHAR2(50 BYTE),NUMBER_GROUP NUMBER(38,0))xt;
delete from xml_tmp;
END;
END IMPORT_XML_TRAINER;

create or replace 
PROCEDURE IMPORT_XML_WORKOUT AS 
BEGIN
DECLARE 
 cSQLclient CLOB;
 line VARCHAR2(100);
 f utl_file.file_type; 
 x pls_integer := 0;
begin
  begin
    f := utl_file.fopen('EXTRACT_DIR', 'EXPORT_WORKOUT_TO_XML.xml', 'R');
loop
  utl_file.GET_LINE(f,line);
  cSQLclient:=CONCAT(cSQLclient,line);
  x:=x+1;
exit when x=-5;
  end loop;
exception
when others
then dbms_output.put_line(sqlerrm);
end;
  utl_file.fclose(f);

insert into xml_tmp values (xmltype(cSQLclient));

insert into WORKOUT
select NUMBERWORKOUT,ID_DATACLIENT,PULSE,DIFFICULTY_WORKOUT,INTENSITY,COUNTEXERCISES,DATETIME
from xml_tmp x, xmltable('/workout/columns'
passing x.xml_data columns NUMBERWORKOUT NUMBER(38,0), ID_DATACLIENT NUMBER(38,0),PULSE NUMBER(38,0),
DIFFICULTY_WORKOUT VARCHAR2(50 BYTE),INTENSITY VARCHAR2(50 BYTE),COUNTEXERCISES NUMBER(38,0),DATETIME DATE)xt;
delete from xml_tmp;
END;
END IMPORT_XML_WORKOUT;

create or replace 
PROCEDURE INSERT_EXERCISES_FOR_CLIENT(
ID_CLIENT number,
ID_EXERCISES number,
EXERCISES VARCHAR,
MUSCLEGROUPS VARCHAR,
DAYFOREXERCISE VARCHAR,
PRIORETY number)
AS 

BEGIN
INSERT INTO EXERCISESFORCLIENT values(ID_CLIENT,ID_EXERCISES,EXERCISES,MUSCLEGROUPS,DAYFOREXERCISE,PRIORETY);
END INSERT_EXERCISES_FOR_CLIENT;

create or replace 
PROCEDURE INSERT_EXERCISESFORCLIENT(
ID_CLIENT number,
ID_EXERCISES number,
EXERCISES VARCHAR,
MUSCLEGROUPS VARCHAR,
DAYFOREXERCISE VARCHAR,
PRIORETY number)
AS 

BEGIN
INSERT INTO EXERCISESFORCLIENT values(ID_CLIENT,ID_EXERCISES,EXERCISES,MUSCLEGROUPS, DAYFOREXERCISE,PRIORETY);
END INSERT_EXERCISESFORCLIENT;

create or replace 
PROCEDURE INSERT_GROUPFORCLIENT(
NUMBER_GROUP number,
ID_TRAINER number
) 
AS
BEGIN 
INSERT INTO groupforclient values(NUMBER_GROUP,ID_TRAINER);
END;

create or replace 
PROCEDURE INSERT_MESSAGE(
MESSAGES VARCHAR,
ID_MESSAGE NUMBER,
WHO_SENDER NUMBER,
WHO_RECIPIENT NUMBER,
GROUPMESSAGE NUMBER ,
DATETIME DATE
)
AS 
BEGIN
INSERT INTO messages values(MESSAGES,ID_MESSAGE,WHO_SENDER,WHO_RECIPIENT,GROUPMESSAGE,DATETIME);
END INSERT_MESSAGE;

create or replace 
PROCEDURE INSERT_RESULT_EXERCISES(
NUMBERWORKOUT number,
ID_EXERCISES number,
EXERCISES varchar,
WEIGHT_FOR_EXERCISES number,
QQUANTITY number,
DAYFOREXERCISE varchar,
MUSCLEGROUPS varchar,
PRIORETY number)
AS 

BEGIN
INSERT INTO RESULTEXERCISES values(NUMBERWORKOUT,ID_EXERCISES,EXERCISES,WEIGHT_FOR_EXERCISES,QQUANTITY,DAYFOREXERCISE, MUSCLEGROUPS,PRIORETY,CURRENT_DATE);
END INSERT_RESULT_EXERCISES;

create or replace 
PROCEDURE INSERT_WORKOUT(
NUMBERWORKOUT number,
ID_DATACLIENT number,
PULSE number,
DIFFICULTY_WORKOUT VARCHAR,
INTENSITY VARCHAR,
COUNTEXERCISES number)
AS 
BEGIN
INSERT INTO WORKOUT values(NUMBERWORKOUT,ID_DATACLIENT,PULSE,DIFFICULTY_WORKOUT,INTENSITY,COUNTEXERCISES,CURRENT_DATE);
END INSERT_WORKOUT;

create or replace 
PROCEDURE REG_CLIENT(
client_id number,
client_firstname VARCHAR,
client_secondname VARCHAR,
client_login VARCHAR,
client_password VARCHAR,
client_group number)
AS 

BEGIN
INSERT INTO client(id_client,firstname,secondname,login,password,number_group) values(client_id,client_firstname,client_secondname,client_login,client_password,client_group);
END REG_CLIENT;

create or replace 
PROCEDURE REG_CLIENT_DATA(
client_id_dataclient number,
client_id_client number,
client_weight number,
client_height number,
client_bodytype VARCHAR,
client_progress_weight float,
client_progress_power float)
AS 
BEGIN
insert into dataclient values(client_id_dataclient,client_id_client,client_weight,client_height,client_bodytype,client_progress_weight,client_progress_power,CURRENT_DATE);
END REG_CLIENT_DATA;

create or replace 
PROCEDURE REG_TRAINER(
trainer_id number,
trainer_firstname VARCHAR,
trainer_secondname VARCHAR,
trainer_login VARCHAR,
trainer_password VARCHAR,
trainer_group number)
AS 

BEGIN
INSERT INTO trainer(id_trainer,firstname,secondname,login,password,number_group) values(trainer_id,
trainer_firstname,
trainer_secondname,
trainer_login,
trainer_password,
trainer_group);
END REG_TRAINER;

create or replace 
PROCEDURE REG_TRAINER_DATA(
trainer_id number,
trainer_weight number,
trainer_height number,
trainer_BARBELLSQUAT number,
trainer_DEADLIFT number,
trainer_BENCHPRESS number,
trainer_PULLUPS number,
trainer_BODYTYPE varchar)
AS 
BEGIN
  insert into datatrainer(id_trainer,weight,height,barbellsquat,deadlift,benchpress,pullups,bodytype) values(trainer_id,trainer_weight,trainer_height,trainer_BARBELLSQUAT,
  trainer_DEADLIFT,trainer_BENCHPRESS,trainer_PULLUPS,trainer_BODYTYPE);
END REG_TRAINER_DATA;

create or replace 
PROCEDURE SORT_MESSAGES_FOR_CLIENT
(curs_trainer OUT SYS_REFCURSOR,
idrecipient number,
idsender number) 
AS
BEGIN 
OPEN curs_trainer FOR SELECT * FROM messages
where(who_recipient=idrecipient and who_sender=idsender OR who_recipient=idsender and who_sender=idrecipient)
ORDER BY datetime ASC;
END SORT_MESSAGES_FOR_CLIENT;