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

## Run Cockroach DB locally in Docker (Single Node Insecure)

`docker run --rm -d --name=roach -p 8080:8080 -p 26257:26257 cockroachdb/cockroach:latest start-single-node --insecure`

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

## Queries

Basics

`SELECT * FROM dogs`

`SELECT * FROM dogs LIMIT 100;`

```
SELECT *
FROM dogs d
INNER JOIN breed b ON d.breed = b.name
LIMIT 10;
```

### Arrays

```
SELECT *
FROM dogs
WHERE tricks <@ ARRAY['sit']
LIMIT 10;
```

### JSON



## Indexes

**_NOTE:_** sequential numbers in indexes are a bad idea unless you use a hash sharded index

`CREATE INDEX ix_breed ON dogs(breed);`

### Storing

`CREATE INDEX ix_name ON dogs(name) STORING (breed);`

### Inverted Indexes

`CREATE INVERTED INDEX ix_breed_tricks ON dogs(breed, tricks);`

Limitations: only one array/json column per index. Must be the last column in the index.

### Hash Sharded Indexes


## Change Feeds

`CREATE CHANGEFEED FOR TABLE <table_name> INTO 'kafka//host:port'`

## Monitoring

Cockroach DB Dashboard or Grafana

### CPU (Hardware)

### Latency (Overview)

### Heartbeat latency (Distributed Dashboard)

### Index recommendations


## Run in Docker Compose (Multi Node Insecure)

Execute in terminal:
`docker-compose up -d`

Initialise via terminal:
`docker exec -it roach1 ./cockroach --host=roach1:26357 init --insecure`
