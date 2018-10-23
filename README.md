[![Build status](https://thinkability.visualstudio.com/DDDSample/_apis/build/status/DDDSample-ASP.NET%20Core-CI)](https://thinkability.visualstudio.com/DDDSample/_build/latest?definitionId=-1) ![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)

# DDD Sample for Marten and Kafka
This sample is still in it's early stages. But great stuff is coming! (at least it's planned :-o )

But for now, I have a working structure for command handlers, eventsourced domain model and one eventhandler for updating a readmodel.

## Technologies used
I'm playing a bit around with these technologies for now:
* [Marten](http://jasperfx.github.io/marten/) (a really clever document store abstraction over Postgres). It's used for storing the eventstream from the domain events
* [Kafka](https://github.com/confluentinc/confluent-kafka-dotnet/) for distributing events in a distributed way. My local Kafka is running on docker using the wurstmeister/kafka image. Please know that I am completely new to Kafka, so this is a very preliminar implementation.
* [Lamar](https://jasperfx.github.io/lamar/) (the new beginning of StructureMap) for IoC

## Roadmap
I'm still working out the structure of this project, but eventually, I'm hoping to have a good starter template with some pluggable infrastructure for a DDD application (a bounded context)

For now, I'm definately working on:
* Polishing the last bits
* Tests, tests, tests

## I love champagne!
If you've been looking a bit at the code, you've probably stumbled upon a nice little champagne domain! The champagne comes from a personal hobby of mine - and the [Champagne Moments platform](https://champagnemoments.eu) I'm also working on at the moment (where we actually use some of this stuff).

## Contributing
I would love some help along the way - feel free to reach out to me here @GitHub or catch me @ larslb@thinkability.dk.

Cheers!
Lars
