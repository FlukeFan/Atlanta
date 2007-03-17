CREATE DATABASE 'LendingLibrary.fdb' user 'SYSDBA' password 'masterkey';

CREATE GENERATOR hibernate_sequence;
SET GENERATOR hibernate_sequence TO 1000;

CREATE TABLE Library (
    Library_Id  INTEGER         NOT NULL
    );

CREATE TABLE Media (
    Media_Id    INTEGER         NOT NULL,
    Library_Id  INTEGER         NOT NULL,
    Type        INTEGER         NOT NULL,
    Name        VARCHAR(50)     NOT NULL,
    Description VARCHAR(50)     NOT NULL
    );

