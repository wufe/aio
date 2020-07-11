# Ghoul

Small continous integration software.

- Portable
- Task based
- Supports hooks (github)

***

## Architecture

> **Domain driven clean architecture**  

Consists of a loose blending of DDD and Clean Architecture.

The core functionality resides in the **domain layer**.  
It is wrapped and accessed by an **application layer** which uses the **persistence layer** together with an **infrastructure layer** to link contents for the **presentation layer**, the one directly linked with the UI.


### Domain layer

Consists of:

- Domain **entities**
- Domain **services** for the orchestration
- **Value objects**

For simplicity the concepts of "aggregate" will not be introduced in the first instance.  
It might be a later improvement while doing another iteration.

### Application layer

Wraps the communication with the domain layer, accessing *directly to domain entities/services* while executing commands and *directly to persistence models/repositories* while executing queries.

The mapping from/to the persistence models to/from the domain entities will be applied from the Automapper lib.

Here comes the concept of **Commands** and **Queries**.  
They represent retrieval/write logic wrapped into objects.

The commands will be responsible for CUD operations, while queries for read operations.

They will be dispatched from the presentation layer and be performed on the application layer, which holds direct links to persistence and domain layers.