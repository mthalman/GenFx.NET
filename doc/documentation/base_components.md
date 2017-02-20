# GenFx Base Components

GenFx implements a set of base components for the features that are common to genetic algorithms.

List of components:
* [Genetic Algorithm](#genetic-algorithm)
* [Genetic Entity](#genetic-entity)
* [Population](#population)
* [Fitness Evaluator](#fitness-evaluator)
* [Selection Operator](#selection-operator)
* [Crossover Operator](#crossover-operator)
* [Mutation Operator](#mutation-operator)
* [Elitism Strategy](#elitism-strategy)
* [Fitness Scaling Strategy](#fitness-scaling-strategy)
* [Terminator](#terminator)
* [Metric](#metric)
* [Plugin](#plugin)

## Genetic Algorithm
The genetic algorithm component is the root component that is used to configure and execute a GA. It is configured to reference all the other components that are to be used during execution. It defines the logic which drives the execution of the GA as well as providing extension points that can be used to respond to the algorithm's lifecycle.

### Genetic Algorithm Lifecyle
The lifecycle of a genetic algorithm consists of the following phases:
1. **Initialize**  
   Performs the necessary validation steps and generates the initial environment.
2. **Calculate fitness**  
   Calculates the fitness of the initial environment.
3. **Create a new generation**  
   Consists of a set of sub-phases which can vary according to the type of algorithm used and its configuration:
   1. **Select entities**  
      Uses the selection operator to choose entities that are to either be moved to the next generation or modified according to other operators.
   2. **Apply operators**  
      Applies operators such as crossover and mutation to the selected entities.  These operators can potentially modify the state of those entities based on randomization.
   3. **Calculate fitness**  
      Calculates the fitness of the new generation.
4. **Check for termination**  
   Terminates the algorithm if the configured condition has been met.

## Genetic Entity
A genetic entity represents the unit that undergoes evolution within a GA.  It contains state which represents the encoding of the solution the GA is intended to test.  For example, a genetic entity could be a list of integers where the list of integers represent the ordered set of cities which are to be traveled in an algorithm tackling the classic Traveling Salesman problem.

## Population
A population is simply a collection of genetic entities.  A genetic algorithm can contain multiple populations in its environment. Typically, the genetic entities contained within a particular entity evolve amongst themselves, isolated from other populations.  But it is possible for there to be intermixing between populations via logic controlled by the genetic algorithm.

## Fitness Evaluator
 A fitness evaluator calculates the fitness of a genetic entity. The fitness of an entity is a relative measurement of how close it is to meeting the goal of the genetic algorithm.  For example, a genetic algorithm that uses binary strings to reach a goal of an entity with all ones in its string might use a fitness evaluator that counts the number of ones in a binary string as the fitness value.

 In GenFx, the fitness evaluator is responsible for calculating the _raw_ fitness value. GenFx also has the concept of a _scaled_ fitness value which is optionally calculated by a [fitness scaling strategy](#fitness-scaling-strategy).

 ## Selection Operator
 A selection operator is responsible for selecting entities to be used for populating the next generation. Typically, the selection is based on fitness of the entity where entities with better fitness values have a greater chance of being selected.

 ## Crossover Operator
 A crossover operator acts upon two or more entities to produce offspring which are then added to the next generation.  A crossover operator combines parts of the parent entities to produce one or more child entities.  A crossover operator doesn't always take action; it defines a crossover rate value which determines the percentage chance that a crossover actually occurs for a given set of parents. The crossover operator is optional when executing a GA in GenFx.

 ## Mutation Operator
 A mutation operator acts upon a single entity by modifying it in some way, typically randomly.  For example, a mutation operator could mutate a binary string by changing a bit value from 0 to 1. A mutation operator doesn't always take action; it defines a mutation rate value which determines the percentage chance that a mutation actually occurs.  This mutation rate applies to each component (or allele in GA theory language) of the entity, meaning that each component has a chance to become mutated (in the case of a binary string, a bit would be what is referred to as a component). The mutation operator is optional when executing a GA in GenFx.

 ## Elitism Strategy
 An elitism strategy is responsible for determining which entities, if any, are allowed to be added to the next generation without any modification. Typically, the intent of such a strategy is to allow a small amount of entities that have good fitness values to be passed along so that their good fitness is not potentially lost through the crossover and mutation operators. The elitism strategy is optional when executing a GA in GenFx.

 ## Fitness Scaling Strategy
 A fitness scaling strategy applies a calculation, using the raw fitness values of the entities in a population, to assign a scaled fitness value to each entity. Fitness scaling is a technique that can be used to prevent the problem of premature convergence where too much emphasis is placed on the highly fit genetic entities in early generations causing a loss of diversity. Thus, it provides the ability to control the magnitude of fitness differences within a population. The elitism strategy is optional when executing a GA in GenFx.

 ## Terminator
 A terminator is simply a way to determine when the genetic algorithm should finish. A terminator is optional when executing a GA in GenFx but the lack of one means that the algorithm will execute indefinitely.

 ## Metric
 A metric is a component which tracks data about the state of the algorithm's populations during execution.  Metrics provide a good way to determine how well a genetic algorithm is running and useful for troubleshooting and analysis.

 ## Plugin
 A plugin is simply a component which provides custom behavior in response to events within the genetic algorithm lifecycle.  For example, a plugin could store all entity state for each generation in a database so that it can be analyzed later.
