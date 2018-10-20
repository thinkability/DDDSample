# DDD Sample for Marten and Kafka
This sample is still in it's early stages. But great stuff is coming! (at least it's planned :-o )

But for now, I have a working structure for command handlers, eventsourced domain model and one eventhandler for updating a readmodel.

## Technologies used
I'm playing a bit around with these technologies for now:
* Marten (a document store abstraction over Postgres). It's used for storing the eventstream from the domain events
* Kafka for distributing events in a distributed way. My local Kafka is running on docker using the wurstmeister/kafka image
* Lamar (the new beginning for StructureMap) for IoC

## Roadmap
I'm still working out the structure of this project, but eventually, I'm hoping to have a good starter template with some pluggable infrastructure for a DDD application (a bounded context)

For now, I'm definately working on:
* Polishing the last bits
* Tests, tests, tests

## Contributing
I would love some help along the way - feel free to reach out to me here @GitHub or catch me @ larslb@thinkability.dk.

Cheers!
Lars
