# KafkaFlow Consumer

## Overview

KafkaFlow is an open source Kafka Client, built on top of Confluent Kafka Client, which makes setup and parallel processing easy and simple to follow.

Repo: https://github.com/Farfetch/kafkaflow

## Goals

While KafkaFlow makes things simpler, we need to configure it to our needs and some of this configuration is not clearly documented. There are our goals:

[ ] - Parallel processing of partitions
[ ] - Process messages in order as per the guaranteed order per partition
[X] - Only commit (complete) messages once processed, never skip messages
[X] - Extensive & clear logging
[ ] - Resume on re-balance/exception


### Parallel processing of partitions
TODO

### Process messages in order as per the guaranteed order per partition
TODO 

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
