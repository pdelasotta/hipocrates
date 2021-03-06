ALTER SESSION SET NLS_DATE_FORMAT = 'MM/DD/YYYY HH24:MI:SS';
ALTER SESSION SET NLS_LANGUAGE = 'LATIN AMERICAN SPANISH';
ALTER SESSION SET NLS_TERRITORY  = 'CHILE';


CREATE TABLE AFILIADO
(
	ID_AFILIADO  NUMBER(8) NOT NULL,
	RUT          NUMBER(9),
	VERIFICADOR  CHAR(1),
	ID_PLAN      NUMBER(8)
)
;


CREATE TABLE BENEFICIO
(
	ID_BENEFICIO   NUMBER(8) NOT NULL,
	PORCENTAJE     NUMBER(5,2) NOT NULL,
	LIMITE_DINERO  NUMBER(8),
	ID_PLAN        NUMBER(8),
	ID_PRESTACION  NUMBER(8)
)
;


CREATE TABLE EMPRESA
(
	ID_EMPRESA    NUMBER(8) NOT NULL,
	NOMBRE        VARCHAR2(64),
	ID_T_EMPRESA  NUMBER(8)
)
;


CREATE TABLE PLAN
(
	ID_PLAN     NUMBER(8) NOT NULL,
	NOMBRE      VARCHAR2(256),
	ID_EMPRESA  NUMBER(8)
)
;


CREATE TABLE PRESTACION
(
	ID_PRESTACION  NUMBER(8) NOT NULL,
	NOMBRE         VARCHAR2(128),
	CODIGO         VARCHAR2(128) NOT NULL
)
;


CREATE TABLE T_EMPRESA
(
	ID_T_EMPRESA  NUMBER(8) NOT NULL,
	NOMBRE        VARCHAR2(64)
)
;



ALTER TABLE PRESTACION
	ADD CONSTRAINT UQ_PRESTACION_CODIGO UNIQUE (CODIGO)
;

ALTER TABLE AFILIADO ADD CONSTRAINT PK_Afiliado 
	PRIMARY KEY (ID_AFILIADO)
;

ALTER TABLE BENEFICIO ADD CONSTRAINT PK_BENEFICIO 
	PRIMARY KEY (ID_BENEFICIO)
;

ALTER TABLE EMPRESA ADD CONSTRAINT PK_EMPRESA 
	PRIMARY KEY (ID_EMPRESA)
;

ALTER TABLE PLAN ADD CONSTRAINT PK_PLAN 
	PRIMARY KEY (ID_PLAN)
;

ALTER TABLE PRESTACION ADD CONSTRAINT PK_PRESTACION 
	PRIMARY KEY (ID_PRESTACION)
;

ALTER TABLE T_EMPRESA ADD CONSTRAINT PK_T_EMPRESA 
	PRIMARY KEY (ID_T_EMPRESA)
;



ALTER TABLE AFILIADO ADD CONSTRAINT FK_AFILIADO_PLAN 
	FOREIGN KEY (ID_PLAN) REFERENCES PLAN (ID_PLAN)
;

ALTER TABLE BENEFICIO ADD CONSTRAINT FK_BENEFICIO_PLAN 
	FOREIGN KEY (ID_PLAN) REFERENCES PLAN (ID_PLAN)
;

ALTER TABLE BENEFICIO ADD CONSTRAINT FK_BENEFICIO_PRESTACION 
	FOREIGN KEY (ID_PRESTACION) REFERENCES PRESTACION (ID_PRESTACION)
;

ALTER TABLE EMPRESA ADD CONSTRAINT FK_EMPRESA_T_EMPRESA 
	FOREIGN KEY (ID_T_EMPRESA) REFERENCES T_EMPRESA (ID_T_EMPRESA)
;

ALTER TABLE PLAN ADD CONSTRAINT FK_PLAN_EMPRESA 
	FOREIGN KEY (ID_EMPRESA) REFERENCES EMPRESA (ID_EMPRESA)
;






CREATE SEQUENCE SEQ_AFILIADO_ID_AFILIADO 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_AFILIADO_ID_AFILIADO 
	BEFORE INSERT 
	ON AFILIADO 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_AFILIADO_ID_AFILIADO.NEXTVAL 
		INTO :NEW.ID_AFILIADO 
		FROM DUAL; 
	END;
/
CREATE SEQUENCE SEQ_BENEFICIO_ID_BENEFICIO 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_BENEFICIO_ID_BENEFICIO 
	BEFORE INSERT 
	ON BENEFICIO 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_BENEFICIO_ID_BENEFICIO.NEXTVAL 
		INTO :NEW.ID_BENEFICIO 
		FROM DUAL; 
	END;
/
CREATE SEQUENCE SEQ_EMPRESA_ID_EMPRESA 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_EMPRESA_ID_EMPRESA 
	BEFORE INSERT 
	ON EMPRESA 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_EMPRESA_ID_EMPRESA.NEXTVAL 
		INTO :NEW.ID_EMPRESA 
		FROM DUAL; 
	END;
/
CREATE SEQUENCE SEQ_PLAN_ID_PLAN 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_PLAN_ID_PLAN 
	BEFORE INSERT 
	ON PLAN 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_PLAN_ID_PLAN.NEXTVAL 
		INTO :NEW.ID_PLAN 
		FROM DUAL; 
	END;
/
CREATE SEQUENCE SEQ_PRESTACION_ID_PRESTACION 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_PRESTACION_ID_PRESTACION 
	BEFORE INSERT 
	ON PRESTACION 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_PRESTACION_ID_PRESTACION.NEXTVAL 
		INTO :NEW.ID_PRESTACION 
		FROM DUAL; 
	END;
/
CREATE SEQUENCE SEQ_T_EMPRESA_ID_T_EMPRESA 
    INCREMENT BY 1 
    START WITH 1 
    NOMAXVALUE 
    MINVALUE 1 
	NOCYCLE 
	NOCACHE 
	NOORDER
;



CREATE OR REPLACE TRIGGER TRG_T_EMPRESA_ID_T_EMPRESA 
	BEFORE INSERT 
	ON T_EMPRESA 
	FOR EACH ROW 
	BEGIN 
		SELECT SEQ_T_EMPRESA_ID_T_EMPRESA.NEXTVAL 
		INTO :NEW.ID_T_EMPRESA 
		FROM DUAL; 
	END;
/


INSERT INTO T_EMPRESA (NOMBRE) VALUES ('Compañias de Seguros');
INSERT INTO T_EMPRESA (NOMBRE) VALUES ('Instituciones Mutualistas');

INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Banmedica',1);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Colmena Golden Cross',1);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Mas vida',2);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Vida tres',2);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Cruz Blanca',1);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Consalud',2);
INSERT INTO EMPRESA (NOMBRE,ID_T_EMPRESA) VALUES ('Ferrosalud',1);

INSERT INTO PLAN (NOMBRE,ID_EMPRESA) VALUES ('Judah',1);
INSERT INTO PLAN (NOMBRE,ID_EMPRESA) VALUES ('Angela',2);
INSERT INTO PLAN (NOMBRE,ID_EMPRESA) VALUES ('Sydney',3);
INSERT INTO PLAN (NOMBRE,ID_EMPRESA) VALUES ('Aidan',4);
INSERT INTO PLAN (NOMBRE,ID_EMPRESA) VALUES ('Tiger',5);

INSERT INTO AFILIADO (RUT,VERIFICADOR,ID_PLAN) VALUES ('18766326','1',1);
INSERT INTO AFILIADO (RUT,VERIFICADOR,ID_PLAN) VALUES ('18861422','1',2);
INSERT INTO AFILIADO (RUT,VERIFICADOR,ID_PLAN) VALUES ('18373278','1',3);
INSERT INTO AFILIADO (RUT,VERIFICADOR,ID_PLAN) VALUES ('16369140','K',4);
INSERT INTO AFILIADO (RUT,VERIFICADOR,ID_PLAN) VALUES ('18863461','3',5);

INSERT INTO PRESTACION (NOMBRE,CODIGO) VALUES ('Gregory','PRES001');
INSERT INTO PRESTACION (NOMBRE,CODIGO) VALUES ('Aline','PRES002');
INSERT INTO PRESTACION (NOMBRE,CODIGO) VALUES ('Leila','PRES003');
INSERT INTO PRESTACION (NOMBRE,CODIGO) VALUES ('Brady','PRES004');
INSERT INTO PRESTACION (NOMBRE,CODIGO) VALUES ('Hiroko','PRES005');

INSERT INTO BENEFICIO (PORCENTAJE,LIMITE_DINERO,ID_PLAN,ID_PRESTACION) VALUES (90,925518,1,1);
INSERT INTO BENEFICIO (PORCENTAJE,LIMITE_DINERO,ID_PLAN,ID_PRESTACION) VALUES (80,830584,2,2);
INSERT INTO BENEFICIO (PORCENTAJE,LIMITE_DINERO,ID_PLAN,ID_PRESTACION) VALUES (23,554008,3,3);
INSERT INTO BENEFICIO (PORCENTAJE,LIMITE_DINERO,ID_PLAN,ID_PRESTACION) VALUES (53,334737,4,4);
INSERT INTO BENEFICIO (PORCENTAJE,LIMITE_DINERO,ID_PLAN,ID_PRESTACION) VALUES (89,758366,5,5);

commit;