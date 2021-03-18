  CREATE TABLE Exercises(
	Exercises varchar(50) NOT NULL,
	MuscleGroups varchar(20) NULL,
	Priorety int NULL,
   CONSTRAINT PK_Exercises PRIMARY KEY (Exercises));

CREATE TABLE Messages(
	messages varchar(300) NOT NULL,
	id_Message int NOT NULL,
	Who_sender int NOT NULL,
	Who_recipient int NOT NULL,
	GroupMessage int NULL,
	DateTime date NOT NULL,
  CONSTRAINT PK_Messages PRIMARY KEY (id_Message));-- Date=>DateTime
  
   CREATE TABLE GroupForClient(-->>GroupForClient
	Number_Group int NOT NULL,
	Id_Trainer int NOT NULL,
 CONSTRAINT PK_Group_1 PRIMARY KEY (Number_Group));
 
  CREATE TABLE Trainer(
	FirstName varchar(50) NOT NULL,
	SecondName varchar(50) NOT NULL,
	Id_Trainer int NOT NULL,
	Password varchar(50) NOT NULL,
	Login varchar(50) NOT NULL,
	Number_Group int NULL,
 CONSTRAINT PK_Trainer PRIMARY KEY (Id_Trainer),
 CONSTRAINT FK_Trainer_Group FOREIGN KEY(Number_Group)
REFERENCES GroupForClient (Number_Group));

 CREATE TABLE DataTrainer(
	Id_Trainer int NOT NULL,
	Weight int NOT NULL,
	Height int NOT NULL,
	BarbellSquat int NOT NULL,
	Deadlift int NOT NULL,
	BenchPress int NOT NULL,
	Pullups int NOT NULL,
	BodyType varchar(50) NOT NULL,
 CONSTRAINT PK_DataTrainer PRIMARY KEY (Id_Trainer),
 CONSTRAINT FK_DataTrainer_Trainer FOREIGN KEY(Id_Trainer)
 REFERENCES Trainer (Id_Trainer));
 /*введение средаства диагностики схема бд
 записка 
 проект*/
  CREATE TABLE Client(
	FirstName varchar(50) NOT NULL,
	SecondName varchar(50) NOT NULL,
	Login varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Id_Client int NOT NULL,
	Number_Group int NULL,
   CONSTRAINT PK_Client_1 PRIMARY KEY (Id_Client),
   CONSTRAINT FK_Client_Group1 FOREIGN KEY(Number_Group)
   REFERENCES GroupForClient (Number_Group));
   
   CREATE TABLE ExercisesForClient(
  Id_Client int NULL,
  Id_Exercises int NULL,
	Exercises varchar(100) NULL,
  MuscleGroups varchar(50) NULL,
  DayForExercise varchar(50) NULL,
  Priorety int NULL,
  CONSTRAINT PK_ExForClient1 PRIMARY KEY (Id_Exercises),
  CONSTRAINT FK_ExForClient_Workout1 FOREIGN KEY(Id_Client)
  REFERENCES Client (Id_Client));-- Number=>NumberEx
  
  CREATE TABLE DataClient(
	Id_DataClient int NOT NULL,
  Id_Client int NOT NULL,
	Weight int NOT NULL,
	Height int NOT NULL,
	Bodytype varchar(50) NOT NULL,
	Progress_Weight float NULL,
  Progress_Power float NULL,
  DateTime date NULL,
 CONSTRAINT PK_DataClient PRIMARY KEY (Id_DataClient),
 CONSTRAINT FK_DataClient_Client FOREIGN KEY(Id_Client)
REFERENCES Client (Id_Client));

  CREATE TABLE Workout(
	NumberWorkout int NOT NULL,
  Id_DataClient int NULL,
  Pulse int NULL,
  difficulty_Workout varchar(100) NULL,
  intensity varchar(100) NULL,
  CountExercises int NULL,
	DateTime date NULL,
 CONSTRAINT PK_WorkoutExercises PRIMARY KEY (NumberWorkout),
 CONSTRAINT FK_WorkoutEx_ExForClient FOREIGN KEY(Id_DataClient)
 REFERENCES DataClient (Id_DataClient));
 
/*    CREATE TABLE GroupExercises(
  Id_GroupExercisesForDay int NULL,
  NumberWorkout int NULL,
  DayForExercise varchar(50) NULL,
  CONSTRAINT PK_GroupEx PRIMARY KEY (Id_GroupExercisesForDay),
  CONSTRAINT FK_GroupEx_Workout1 FOREIGN KEY(NumberWorkout)
  REFERENCES Workout (NumberWorkout));-- Number=>NumberEx*/
  
  CREATE TABLE ResultExercises(
  NumberWorkout int NOT NULL,
  Id_Exercises int NULL,
	Exercises varchar(100) NULL,
  Weight_For_Exercises int NULL,
  Qquantity int NULL,
  DayForExercise varchar(50) NULL,
  MuscleGroups varchar(50) NULL,
  Priorety int NULL,
  DateTime date NULL,
  CONSTRAINT PK_ResultEx PRIMARY KEY (Id_Exercises),
  CONSTRAINT FK_ResultEx_ExForWorkout FOREIGN KEY(NumberWorkout)
  REFERENCES Workout (NumberWorkout));-- Number=>NumberEx
ALTER TABLE ResultExercises ADD DateTime date NULL;
  drop table Exercises;
drop table Messages;
drop table ExercisesForClient;
drop table GroupExercises;
drop table ResultExercises;
drop table WORKOUT;
drop table DataClient;
drop table Client;
drop table DataTrainer;
drop table Trainer;
drop table GroupForClient;
 
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('"Полувер" в кроссовере',	'Верхняя часть спины',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Армейский жим',	'Плечи',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Гиперэкстензия',	'Нижняя часть спины',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Жим Арнольда',	'Плечи',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Жим гантелей лежа',	'Грудь',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Жим гантелей сидя',	'Плечи',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Жим сидя в рычажном тренажере',	'Грудь',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Жим штанги лёжа',	'Грудь',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Зашагивания на скамью',	'Бёдра',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Косые скручивания на полу',	'Пресс',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Отведение гантелей через стороны стоя',	'Плечи',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Отведение руки с гантелей в наклоне',	'Трицепс',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Отжимания на брусьях',	'Грудь',2);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Отжимания от пола',	'Грудь',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Отжимания спиной к скамье',	'Трицепс',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Подтягивания обратным хватом',	'Верхняя часть спины',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Подъём на мыски сидя',	'Икры',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Подъем на носки стоя',	'Икры',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Подъем ног в висе',	'Пресс',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Подъем ног в упоре на локти',	'Пресс',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Приседание со штангой',	'Бёдра',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Разгибание рук в локтевом суставе на блоке стоя',	'Трицепс',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сведение рук в тренажере',	'Грудь',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сгибание ног в тренажере лежа',	'Бёдра',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сгибание ног в тренажёре сидя',	'Бёдра',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сгибание рук на скамье Скотта',	'Бицепс',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сгибание рук с гантелями стоя',	'Бицепс',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Сгибание рук со штангой стоя',	'Бицепс',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Скручивания на наклонной скамье',	'Пресс',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Становая тяга классическая',	'Нижняя часть спины',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Тяга верхнего блока к груди',	'Верхняя часть спины',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Тяга гантелей лежа на скамье на животе',	'Верхняя часть спины',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Тяга т-грифа в наклоне к поясу',	'Верхняя часть спины',4);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Тяга штанги в наклоне к поясу',	'Верхняя часть спины',5);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Тяга штанги к подбородку',	'Плечи',3);
INSERT INTO exercises(EXERCISES,MUSCLEGROUPS,PRIORETY) VALUES ('Французский жим с гантелями сидя',	'Трицепс',4);
COMMIT;

create table DIAGNOSTIC_TABLE
(
	date1 DATE,
	operation nvarchar2(500),
	trigger1 nvarchar2(500),
	info nvarchar2(500)
);

create or replace 
trigger Trigger_For_CLIENT
after update or insert or delete on CLIENT
for each row
begin
	dbms_output.put_line('Trigger_For_Table_CLIENT');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_CLIENT', :new.ID || ' ' || :new.LOGIN || ' ' || :new.PASSWORD|| ' ' || :new.FIRSTNAME|| ' ' || :new.SECONDNAME);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_CLIENT', 'before: ' || :old.ID || ' ' || :old.LOGIN || ' ' || :old.PASSWORD|| ' ' || :old.FIRSTNAME|| ' ' || :old.SECONDNAME || 'after: ' || :new.ID || ' ' || :new.LOGIN || ' ' || :new.PASSWORD|| ' ' || :new.FIRSTNAME|| ' ' || :new.SECONDNAME);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_CLIENT', :old.ID || ' ' || :old.LOGIN || ' ' || :old.PASSWORD|| ' ' || :old.FIRSTNAME|| ' ' || :old.SECONDNAME);
	end if;
end;

create or replace 
trigger Trigger_For_DATACLIENT
after update or insert or delete on DATACLIENT
for each row
begin
	dbms_output.put_line('Trigger_For_Table_DATACLIENT');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_DATACLIENT', :new.ID_DATACLIENT || ' ' || :new.ID_CLIENT || ' ' || :new.WEIGHT || ' ' || :new.HEIGHT|| ' ' || :new.BODYTYPE|| ' ' || :new.PROGRESS_WEIGHT || ' ' || :new.PROGRESS_POWER ||' ' || :new.DATETIME);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_DATACLIENT', 'before: ' || :old.ID_DATACLIENT || ' ' || :old.ID_CLIENT || ' ' || :old.WEIGHT || ' ' || :old.HEIGHT|| ' ' || :old.BODYTYPE|| ' ' || :old.PROGRESS_WEIGHT || ' ' || :old.PROGRESS_POWER ||' ' || :old.DATETIME || 'after: ' || :new.ID_DATACLIENT || ' ' || :new.ID_CLIENT || ' ' || :new.WEIGHT || ' ' || :new.HEIGHT|| ' ' || :new.BODYTYPE|| ' ' || :new.PROGRESS_WEIGHT || ' ' || :new.PROGRESS_POWER ||' ' || :new.DATETIME);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_DATACLIENT', :old.ID_DATACLIENT || ' ' || :old.ID_CLIENT || ' ' || :old.WEIGHT || ' ' || :old.HEIGHT|| ' ' || :old.BODYTYPE|| ' ' || :old.PROGRESS_WEIGHT || ' ' || :old.PROGRESS_POWER ||' ' || :old.DATETIME);
	end if;
end;

create or replace 
trigger Trigger_For_DATATRAINER
after update or insert or delete on DATATRAINER
for each row
begin
	dbms_output.put_line('Trigger_For_Table_DATATRAINER');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_DATATRAINER', :new.ID_TRAINER || ' ' || :new.WEIGHT || ' ' || :new.HEIGHT|| ' ' || :new.BARBELLSQUAT|| ' ' || :new.DEADLIFT || ' ' || :new.BENCHPRESS || ' ' || :new.PULLUPS || ' ' || :new.BODYTYPE);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_DATATRAINER', 'before: ' || :old.ID_TRAINER || ' ' || :old.WEIGHT || ' ' || :old.HEIGHT|| ' ' || :old.BARBELLSQUAT || ' ' || :old.DEADLIFT || ' ' || :old.BENCHPRESS || ' ' || :old.PULLUPS || ' ' || :old.BODYTYPE || 'after: ' || :new.ID_TRAINER || ' ' || :new.WEIGHT || ' ' || :new.HEIGHT|| ' ' || :new.BARBELLSQUAT|| ' ' || :new.DEADLIFT || ' ' || :new.BENCHPRESS || ' ' || :new.PULLUPS || ' ' || :new.BODYTYPE);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_DATATRAINER', :old.ID_TRAINER || ' ' || :old.WEIGHT || ' ' || :old.HEIGHT|| ' ' || :old.BARBELLSQUAT || ' ' || :old.DEADLIFT || ' ' || :old.BENCHPRESS || ' ' || :old.PULLUPS || ' ' || :old.BODYTYPE);
	end if;
end;

create or replace 
trigger Trigger_For_TRAINER
after update or insert or delete on TRAINER
for each row
begin
	dbms_output.put_line('Trigger_For_Table_TRAINER');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_TRAINER', :new.ID_TRAINER || ' ' || :new.LOGIN || ' ' || :new.PASSWORD|| ' ' || :new.FIRSTNAME|| ' ' || :new.SECONDNAME || ' ' || :new.NUMBER_GROUP);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_TRAINER', 'before: ' || :old.ID_TRAINER || ' ' || :old.LOGIN || ' ' || :old.PASSWORD|| ' ' || :old.FIRSTNAME|| ' ' || :old.SECONDNAME  || ' ' || :old.NUMBER_GROUP || 'after: ' || :new.ID_TRAINER || ' ' || :new.LOGIN || ' ' || :new.PASSWORD|| ' ' || :new.FIRSTNAME|| ' ' || :new.SECONDNAME || ' ' || :new.NUMBER_GROUP);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_TRAINER', :old.ID_TRAINER || ' ' || :old.LOGIN || ' ' || :old.PASSWORD|| ' ' || :old.FIRSTNAME|| ' ' || :old.SECONDNAME || ' ' || :old.NUMBER_GROUP);
	end if;
end;

create or replace 
trigger Trigger_For_RESULTEXERCISES
after update or insert or delete on RESULTEXERCISES
for each row
begin
	dbms_output.put_line('Trigger_For_RESULTEXERCISES');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_RESULTEXERCISES', :new.NUMBERWORKOUT || ' ' || :new.ID_EXERCISES || ' ' || :new.EXERCISES|| ' ' || :new.WEIGHT_FOR_EXERCISES || ' ' || :new.QQUANTITY || ' ' || :new.DAYFOREXERCISE|| ' ' || :new.MUSCLEGROUPS|| ' ' || :new.PRIORETY || ' ' || :new.DATETIME);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_RESULTEXERCISES',
    'before: ' || :old.NUMBERWORKOUT || ' ' || :old.ID_EXERCISES || ' ' || :old.EXERCISES|| ' ' || :old.WEIGHT_FOR_EXERCISES || ' ' || :old.QQUANTITY || ' ' || :old.DAYFOREXERCISE|| ' ' || :old.MUSCLEGROUPS|| ' ' || :old.PRIORETY || ' ' || :old.DATETIME || 
    'after: ' || :new.NUMBERWORKOUT || ' ' || :new.ID_EXERCISES || ' ' || :new.EXERCISES|| ' ' || :new.WEIGHT_FOR_EXERCISES || ' ' || :new.QQUANTITY || ' ' || :new.DAYFOREXERCISE|| ' ' || :new.MUSCLEGROUPS|| ' ' || :new.PRIORETY || ' ' || :new.DATETIME);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_RESULTEXERCISES', :old.NUMBERWORKOUT || ' ' || :old.ID_EXERCISES || ' ' || :old.EXERCISES|| ' ' || :old.WEIGHT_FOR_EXERCISES || ' ' || :old.QQUANTITY || ' ' || :old.DAYFOREXERCISE|| ' ' || :old.MUSCLEGROUPS|| ' ' || :old.PRIORETY || ' ' || :old.DATETIME);
	end if;
end;

create or replace 
trigger Trigger_For_MESSAGES
after update or insert or delete on MESSAGES
for each row
begin
	dbms_output.put_line('Trigger_For_Table_MESSAGES');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_MESSAGES', :new.MESSAGES || ' ' || :new.ID_MESSAGE || ' ' || :new.WHO_SENDER|| ' ' || :new.WHO_RECIPIENT|| ' ' || :new.GROUPMESSAGE|| ' ' || :new.DATETIME);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_MESSAGES', 'before: ' || :old.MESSAGES || ' ' || :old.ID_MESSAGE || ' ' || :old.WHO_SENDER|| ' ' || :old.WHO_RECIPIENT|| ' ' || :old.GROUPMESSAGE || ' ' || :old.DATETIME || 'after: ' || :new.MESSAGES || ' ' || :new.ID_MESSAGE || ' ' || :new.WHO_SENDER|| ' ' || :new.WHO_RECIPIENT|| ' ' || :new.GROUPMESSAGE|| ' ' || :new.DATETIME);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_MESSAGES', :old.MESSAGES || ' ' || :old.ID_MESSAGE || ' ' || :old.WHO_SENDER|| ' ' || :old.WHO_RECIPIENT|| ' ' || :old.GROUPMESSAGE || ' ' || :old.DATETIME);
	end if;
end;

create or replace 
trigger Trigger_For_GROUPFORCLIENT
after update or insert or delete on GROUPFORCLIENT
for each row
begin
	dbms_output.put_line('Trigger_For_Table_GROUPFORCLIENT');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_GROUPFORCLIENT', :new.NUMBER_GROUP || ' ' || :new.ID_TRAINER);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_GROUPFORCLIENT', 'before: ' || :old.NUMBER_GROUP || ' ' || :old.ID_TRAINER || 'after: ' || :new.NUMBER_GROUP || ' ' || :new.ID_TRAINER);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_GROUPFORCLIENT', :old.NUMBER_GROUP || ' ' || :old.ID_TRAINER);
	end if;
end;

create or replace 
trigger Trigger_For_EXERCISESFORCLIENT
after update or insert or delete on EXERCISESFORCLIENT
for each row
begin
	dbms_output.put_line('Trigger_For_Table_EXERCISESFORCLIENT');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_EXERCISESFORCLIENT', :new.ID_CLIENT || ' ' || :new.ID_EXERCISES || ' ' || :new.EXERCISES || ' ' || :new.MUSCLEGROUPS|| ' ' || :new.DAYFOREXERCISE|| ' ' || :new.PRIORETY);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_EXERCISESFORCLIENT', 'before: ' || :old.ID_CLIENT || ' ' || :old.ID_EXERCISES || ' ' || :old.EXERCISES || ' ' || :old.MUSCLEGROUPS|| ' ' || :old.DAYFOREXERCISE|| ' ' || :old.PRIORETY || 'after: ' || :new.ID_CLIENT || ' ' || :new.ID_EXERCISES || ' ' || :new.EXERCISES || ' ' || :new.MUSCLEGROUPS|| ' ' || :new.DAYFOREXERCISE|| ' ' || :new.PRIORETY);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_EXERCISESFORCLIENT', :old.ID_CLIENT || ' ' || :old.ID_EXERCISES || ' ' || :old.EXERCISES || ' ' || :old.MUSCLEGROUPS|| ' ' || :old.DAYFOREXERCISE|| ' ' || :old.PRIORETY);
	end if;
end;

create or replace 
trigger Trigger_For_EXERCISES
after update or insert or delete on EXERCISES
for each row
begin
	dbms_output.put_line('Trigger_For_Table_EXERCISES');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_EXERCISES', :new.EXERCISES || ' ' || :new.MUSCLEGROUPS || ' ' || :new.PRIORETY);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_EXERCISES', 'before: ' || :old.EXERCISES || ' ' || :old.MUSCLEGROUPS || ' ' || :old.PRIORETY || 'after: ' || :new.EXERCISES || ' ' || :new.MUSCLEGROUPS || ' ' || :new.PRIORETY);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_EXERCISES', :old.EXERCISES || ' ' || :old.MUSCLEGROUPS || ' ' || :old.PRIORETY);
	end if;
end;

create or replace 
trigger Trigger_For_WORKOUT
after update or insert or delete on WORKOUT
for each row
begin
	dbms_output.put_line('Trigger_For_Table_WORKOUT');
  if inserting then
      insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'inserting','Trigger_For_Table_WORKOUT', :new.NUMBERWORKOUT || ' ' || :new.ID_DATACLIENT || ' ' || :new.PULSE|| ' ' || :new.DIFFICULTY_WORKOUT|| ' ' || :new.INTENSITY|| ' ' || :new.COUNTEXERCISES || ' ' || :new.DATETIME);
	elsif updating then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'updating', 'Trigger_For_Table_WORKOUT', 'before: ' || :old.NUMBERWORKOUT || ' ' || :old.ID_DATACLIENT || ' ' || :old.PULSE || ' ' || :old.DIFFICULTY_WORKOUT || ' ' || :old.INTENSITY || ' ' || :old.COUNTEXERCISES || ' ' || :old.DATETIME || 'after: ' || :new.NUMBERWORKOUT || ' ' || :new.ID_DATACLIENT || ' ' || :new.PULSE|| ' ' || :new.DIFFICULTY_WORKOUT|| ' ' || :new.INTENSITY|| ' ' || :new.COUNTEXERCISES || ' ' || :new.DATETIME);
	elsif deleting then
		insert into DIAGNOSTIC_TABLE values (CURRENT_DATE, 'deleting', 'Trigger_For_Table_WORKOUT', :old.NUMBERWORKOUT || ' ' || :old.ID_DATACLIENT || ' ' || :old.PULSE || ' ' || :old.DIFFICULTY_WORKOUT || ' ' || :old.INTENSITY || ' ' || :old.COUNTEXERCISES || ' ' || :old.DATETIME);
	end if;
end;
  


 

 

   

 

 

 


  
  
  


