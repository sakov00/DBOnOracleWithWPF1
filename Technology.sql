grant select on v_$process to BOSS;
grant select on v_$session to BOSS;
select p.spid
from v$process p, v$session s
where p.addr = s.paddr
  and s.audsid = userenv('SESSIONID');

select p.spid
from v$process p, v$session s
where p.addr = s.paddr
  and p.Traceid = 'UnicalString5';
  
select sid, serial# from v$session where username = 'BOSS'; 

show parameter dump_dest;

alter session set sql_trace=true;
alter session set tracefile_identifier='UnicalString5';
alter session set sql_trace=false;
--трассировка
alter session set sql_trace=true;
alter session set tracefile_identifier='UnicalString1';
exec DBMS_TRACE.SET_PLSQL_TRACE(32);
--exec DBMS_TRACE.SET_PLSQL_TRACE (32);
select tracefile from v$process where tracefile like '%UnicalString1%';
--в sql plus для преобразования трассировачного файла TKPROF E:\app\ora_install_user\diag\rdbms\orcl\orcl\trace\orcl_ora_12056_UnicalString2.trc text2.txt sys=no explain=y
select * from trainer;
select * from Datatrainer;
insert into trainer values('бот','бот',1,'123','user',null);
exec DBMS_TRACE.CLEAR_PLSQL_TRACE;
alter session set sql_trace=false;