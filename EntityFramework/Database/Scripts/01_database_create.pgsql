-- Database: cnc

-- DROP DATABASE cnc;

CREATE DATABASE cnc
    WITH 
    OWNER = cnc_admin
    ENCODING = 'UTF8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

GRANT TEMPORARY, CONNECT ON DATABASE cnc TO PUBLIC;

GRANT ALL ON DATABASE cnc TO cnc_admin;