# Postgres Prototype
Getting up and running with Postgres on Windows, using [Npgsql](http://www.npgsql.org/) and [Dapper](https://github.com/StackExchange/Dapper).
Postgres is run in docker.

## Getting Started
### Prerequisites
* Docker.
* Some way to build/run dot net projects.
### Postgres Database
Start postgres database, exposed on port 5432 on the local/host machine:

```shell
docker run --name some-postgres -e POSTGRES_PASSWORD=mypassword -p 5432:5432 -d postgres
```

To stop the database:

```shell
docker stop some-postgres
```

Note that the database is not persisted between 'docker run' sessions.
### Psql
To connect via psql:

```shell
docker run -it --rm --link some-postgres:postgres postgres psql -h postgres -U postgres
```

And when prompted for the password, it will be 'mypassword'.

In psql, create a database and table:

```
postgres=# CREATE DATABASE mydb;

postgres=# \connect mydb

mydb=# CREATE TABLE person (
mydb(# id serial PRIMARY KEY,
mydb(# firstname VARCHAR(50) NOT NULL,
mydb(# lastname VARCHAR(50) NOT NULL
mydb(# );
```

To exit from psql:

```
\q
```

### Psql References
[PostgreSQL 9.2.20 Documentation : psql](https://www.postgresql.org/docs/9.2/static/app-psql.html)
[PSQL Quick Reference](http://gpdb.docs.pivotal.io/gs/43/pdf/PSQLQuickRef.pdf)

### Postgres Administration
Download and install the postgres administration UI tool [pgadmin](https://www.pgadmin.org/).
Download and install, then connect like this:
pgadmin: 
Connect via:
Host: 127.0.0.1
Port: 5432
Password: mypassword

From there it is possible to traverse to the newly created table:
Servers > dockerpg > Databases > mydb > Schemas > public > Tables > person

### db-access-tests
This is a C# project using [Npgsql](http://www.npgsql.org/) and [Dapper](https://github.com/StackExchange/Dapper) to interact with the database. 
Load/build the tests in your preferred environment. When the tests are run, a row should be created and read back.

## References
Taken from the [official docker postgres image](https://hub.docker.com/_/postgres/), with some modifications.

