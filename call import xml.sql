BEGIN
  export_xml();
  /*import_xml_messages();
  IMPORT_XML_EXERCISES();
  IMPORT_XML_GROUPFORCLIENT();
  IMPORT_XML_CLIENT();
  IMPORT_XML_TRAINER();
  IMPORT_XML_DATATRAINER();
  IMPORT_XML_DATACLIENT();
  IMPORT_XML_WORKOUT();
  IMPORT_XML_EXERCISESFORCLIENT();
  IMPORT_XML_RESULTEXERCISES();*/
END;
delete from xml_tmp;
delete from RESULTEXERCISES;
delete from workout;
delete from EXERCISESFORCLIENT;
delete from DATATRAINER;
delete from DATACLIENT;
delete from CLIENT;
delete from TRAINER;
delete from GROUPFORCLIENT;
delete from messages;
delete from EXERCISES;
delete from diagnostic_table;
select count(*) from trainer;
set serveroutput on;

declare
number1 number;
begin
FOR number1 IN 60001..100000
LOOP
   insert into trainer values(CONCAT('бот',to_char(number1)),CONCAT('бот',to_char(number1)),number1,'123',CONCAT('user',to_char(number1)),null);
END LOOP;
end;

declare
number1 number;
begin
FOR number1 IN 90001..100000
LOOP
   insert into datatrainer values(number1,90,180,250,250,250,17,'Large');
END LOOP;
end;
select count(*) from trainer

