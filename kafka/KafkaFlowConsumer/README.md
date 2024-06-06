# KafkaFlow Consumer

## Overview

KafkaFlow is an open source Kafka Client, built on top of Confluent Kafka Client, which makes setup and parallel processing easy and simple to follow.

Repo: https://github.com/Farfetch/kafkaflow

## Goals

While KafkaFlow makes things simpler, we need to configure it to our needs and some of this configuration is not clearly documented. These are our goals:

[X] - Parallel processing of partitions
[X] - Process messages in order as per the guaranteed order per message key
[X] - Extensive & clear logging
[~] - Only commit (complete) messages once processed, never skip messages
[~] - Resume on re-balance/exception

### Parallel processing of partitions
Done by using Workers

### Process messages in order as per the guaranteed order per message key
This is handled by default by the ByteSum strategy. However if you want to maintain order by partitiion you need to use: 
`.WithWorkerDistributionStrategy<PartitionKeyDistributionStrategy>()`

> **_NOTE:_**  ByteSum is the default strategy. This maintains order per message key not per partition. This is OK as it makes sure all messages are completed up to a point and commits that offset, if any messsages inbetween are not committed it will not commit any offest beyond that message.

### Extensive & clear logging

As a batch is received and then completed the following key information is logged:
- topic
- partition
- min offset of the batch
- max offset of the batch
- debug logging can be enabled to log the contents of messages (need to check for performance with high volumes)

### Only commit (complete) messages once processed

This was achieved by this setting:
`.WithManualMessageCompletion()`

And then in the batch message processor as each message is processed call:
`messageContext.ConsumerContext.Complete();`

This performs well as it completes it locally and the offsets are committed back to Kafka every x seconds (this is also configurable).

### Resume on re-balance/exception or failed message

We can do two things here:
1. Have a try block in the consumer code and decide what we want to do. Log it and move on, or initifite loop until it passes, etc.
2. We can try use middleware similar to the pause example and keep going back to the earliest message which isn't complete:
references:
https://farfetch.github.io/kafkaflow/docs/getting-started/samples/#pause-consumer-on-error
https://github.com/Farfetch/kafkaflow/tree/master/samples/KafkaFlow.Sample.PauseConsumerOnError
