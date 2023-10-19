# Cockroach DB

## Access via Client Container

Witin the client containers terminal:

`cockroach --certs-dir=/cockroach-certs/ --user root --host=cockroach-cockroachdb-public.cockroachdb.svc.cluster.local:26257 sql`

What exactly is this doing? 

`cockroach`: exectuable which opens a console to query the database

`--certs-dir`: a folder that must contain the certificates to connect, these are used instead of a password when cockroachdb is set up in secure mode

`--user`: the user you are logging in as, must match the certificates

`--host`: the cockroachdb cluster you are connecting to

`sql`: enter sql console mode

## Queries


### Arrays


### JSON


## Indexes

### Includes

### Inverted Indexes



