DROP USER dev CASCADE;
CREATE USER dev IDENTIFIED BY dev;
GRANT CONNECT, RESOURCE TO dev;

DROP USER devseguro CASCADE;
CREATE USER devseguro IDENTIFIED BY devseguro;
GRANT CONNECT, RESOURCE TO devseguro;