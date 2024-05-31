# KafkaFlow Consumer

## Overview

KafkaFlow is an open source Kafka Client, built on top of Confluent Kafka Client, which makes setup and parallel processing easy and simple to follow.

Repo: https://github.com/Farfetch/kafkaflow

## Goals

While KafkaFlow makes things simpler, we need to configure it to our needs and some of this configuration is not clearly documented. There are our goals:

[X] - Parallel processing of partitions
[X] - Process messages in order as per the guaranteed order per partition
[X] - Only commit (complete) messages once processed, never skip messages
[X] - Extensive & clear logging
[ ] - Resume on re-balance/exception

### Parallel processing of partitions
Done by using Workers

### Process messages in order as per the guaranteed order per partition
Done by using `.WithWorkerDistributionStrategy<PartitionKeyDistributionStrategy>()`

> **_NOTE:_**  ByteSum is the default strategy. This maintains order per message key not per partition. This may actually be OK but it depends on how the offset commits are done. This is because ByteSum can process messages out of order per partition, but in order per message key, so you could complete offset 2 before offset 1, but how would KafkaFlow handle this if offset 2 completes but offset 1 fails? The main thing is we do not want to skip/lose messages!

### Only commit (complete) messages once processed

This was achieved by this setting:
`.WithManualMessageCompletion()`

And then in the batch message processor as each message is processed call:
`messageContext.ConsumerContext.Complete();`

This performs well as it completes it locally and the offsets are committed back to Kafka every x seconds (this is also configurable).

### Extensive & clear logging

As a batch is received and then completed the following key information is logged:
- topic
- partition
- min offset of the batch
- max offset of the batch
- debug logging can be enabled to log the contents of messages (need to check for performance with high volumes)

### Resume on re-balance/exception
TODO

