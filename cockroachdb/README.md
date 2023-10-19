# Cockroach DB

## Access via Client Container

Within the client containers terminal (OpenShift):

`cockroach --certs-dir=/cockroach-certs/ --user root --host=cockroach-cockroachdb-public.cockroachdb.svc.cluster.local:26257 sql`

What exactly is this doing? 

`cockroach`: exectuable which opens a console to query the database

`--certs-dir`: a folder that must contain the certificates to connect, these are used instead of a password when cockroachdb is set up in secure mode

`--user`: the user you are logging in as, must match the certificates

`--host`: the cockroachdb cluster you are connecting to

`sql`: enter sql console mode

## Run Cockroach DB locally in Docker

`docker run --rm -d --name=roach -p 8080:8080 -p 26257:26257 cockroachdb/cockroach:latest start-single-node --insecure`

## Queries

Basics

`SELECT * FROM bet`

`SELECT * FROM bet LIMIT 100;`

```
SELECT *
FROM bet b
INNER JOIN bet_selection bs ON bs.id = b.id
LIMIT 10;
```

### Arrays

```
SELECT *
FROM bet_selection
WHERE selections <@ ARRAY[1]
LIMIT 10;
```


### JSON

## Tables

**_NOTE:_** auto incrementing ids are a very bad idea in distributed systems

```
CREATE TABLE dogs
(
    id uuid,
    name STRING,
    breed STRING,
    tricks STRING[]
);
```




## Indexes

**_NOTE:_** sequential numbers in indexes are a bad idea unless you use a hash sharded index



### Includes

### Inverted Indexes


## Change Feeds



