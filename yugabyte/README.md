

# Single Node
Start single node "cluster":
`docker-compose -f docker-compose-single-node.yaml up -d`

Run the following command to check the cluster status:
`docker exec -it yugabyte yugabyted status`

To open the YSQL shell, run ysqlsh:
`docker exec -it yugabyte bash -c '/home/yugabyte/bin/ysqlsh --echo-queries --host $(hostname)'`

Monitor your cluster YugabyteDB UI:
`http://localhost:15433`

# Multi Node

