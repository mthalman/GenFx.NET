# Create a Hello World Genetic Algorithm

In this tutorial, we'll create a very simple genetic algorithm (GA) to help you learn the basics of how to configure and execute a GA using GenFx.

In this GA, the goal will be to generate a binary string that contains only true bit values.

## 1: Create Your Project
First, create a C# Console Application project named SimpleBinaryString.  Then you'll want to add the GenFx.NET NuGet package to your project.

## 2: Create the Fitness Evalutor
Now that we've got our project initialized with GenFx, let's first define the GA's [fitness evaluator](../documentation/base_components.md#fitness-evaluator).  The implementation will be simple: just count the number of 'true' bits in the binary string entity.  Add a new C# file named MyFitnessEvaluator.cs and paste in this code:

    using GenFx;
    using GenFx.Components.Lists;
    using GenFx.Validation;
    using System.Linq;
    using System.Threading.Tasks;

    namespace SimpleBinaryString
    {
        [RequiredGeneticEntity(typeof(BinaryStringEntity))]
        internal class MyFitnessEvaluator : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                BinaryStringEntity binStrEntity = (BinaryStringEntity)entity;

                // The entity's fitness is equal to the number of "true" bits it contains
                // (i.e. a bit representing a 1 value).
                return Task.FromResult<double>(binStrEntity.Count(bit => bit == true));
            }
        }
    }
 
 Let's examine some of the code here...
 * Notice that the class has a [RequiredGeneticEntityAttribute](xref:GenFx.Validation.RequiredGeneticEntityAttribute) attribute, specifying the [BinaryStringEntity](xref:GenFx.Components.Lists.BinaryStringEntity) type. This informs GenFx that this fitness evaluator requires the GA to use a [BinaryStringEntity](xref:GenFx.Components.Lists.BinaryStringEntity) in order to function properly.  GenFx will validate that the GA is configured with that entity type at runtime.
 * In order for this class to be recognized as a fitness evaluator in GenFx, it derives from [FitnessEvaluator](xref:GenFx.FitnessEvaluator).
 * The [FitnessEvaluator](xref:GenFx.FitnessEvaluator) base class is abstract and so this derived class must implement the [EvaluateFitnessAsync](xref:GenFx.FitnessEvaluator.EvaluateFitnessAsync(GenFx.GeneticEntity)) method. This method returns a `System.Double` value representing the fitness value of the entity.  A fitness value is whatever you want it to be; it's specific to the problem you're intending to solve.
 * The method implementation is able to infer that the entity being passed to it is of type [BinaryStringEntity](xref:GenFx.Components.Lists.BinaryStringEntity) since it had declared the [RequiredGeneticEntityAttribute](xref:GenFx.Validation.RequiredGeneticEntityAttribute) attribute.  So the method casts it to that type in order to access the 'bits' of the entity.
 * The last line of the method uses LINQ to count the number of 'true' bits in the entity.  Since this method uses the Task-based asynchronous programming model, it returns `Task<double>`.  Since this method isn't executing any asynchronous behavior, we just wrap the result value we want to return in a `Task`.

## 3. Configure the Algorithm
Now it's time to actually configure our GA with all the components that it should use. First, let's update the `Main` method in your Program.cs file to handle the execution of some asynchronous code. Update the code in your Program.cs file so that it looks like this:

    static void Main(string[] args)
    {
        RunAlgorithmAsync().Wait();
    }

    private static async Task RunAlgorithmAsync()
    {
    }

The rest of the code that we'll be adding should be placed inside the `RunAlgorithmAsync` method. Add the following code to that method:

    SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm
    {
        MinimumEnvironmentSize = 1,
        FitnessEvaluator = new MyFitnessEvaluator(),
        GeneticEntitySeed = new BinaryStringEntity
        {
            MinimumStartingLength = 20,
            MaximumStartingLength = 20,
            IsFixedSize = true
        },
        PopulationSeed = new SimplePopulation
        {
            MinimumPopulationSize = 100
        },
        SelectionOperator = new FitnessProportionateSelectionOperator
        {
            SelectionBasedOnFitnessType = FitnessType.Raw
        },
        CrossoverOperator = new SinglePointCrossoverOperator
        {
            CrossoverRate = 0.8
        },
        MutationOperator = new UniformBitMutationOperator
        {
            MutationRate = 0.01
        },
        Terminator = new FitnessTargetTerminator
        {
            FitnessTarget = 20,
            FitnessValueType = FitnessType.Raw
        }
    };

First, notice that [SimpleGeneticAlgorithm](xref:GenFx.Components.Algorithms.SimpleGeneticAlgorithm) is the type of algorithm being created. GenFx defines several algorithm types and this is the most basic of them. Now let's go through each of the [configuration properties](../documentation/component_model.md#configuration) that are set on the algorithm object:
* [MinimumEnvironmentSize](xref:GenFx.GeneticAlgorithm.MinimumEnvironmentSize): This is configuring the GA so that its environment contains only one population of entities.  GenFx supports more than one, but for purposes of simplicity, we'll only use a single population.
* [FitnessEvaluator](xref:GenFx.GeneticAlgorithm.FitnessEvaluator): This is configuring the GA to use the fitness evalutor that was defined above.
* [GeneticEntitySeed](xref:GenFx.GeneticAlgorithm.GeneticEntitySeed): This is configuring the GA to use a [BinaryStringEntity](xref:GenFx.Components.Lists.BinaryStringEntity). In addition, that entity is configured with the following:
    * [MinimumStartingLength](xref:GenFx.Components.Lists.ListEntityBase.MinimumStartingLength): Indicates the minimum length the binary string should have when first being initialized with a random set of bits.
    * [MaximumStartingLength](xref:GenFx.Components.Lists.ListEntityBase.MaximumStartingLength): Indicates the maximum length the binary string can have when first being initialized with a random set of bits.
    * [IsFixedSize](xref:GenFx.Components.Lists.ListEntityBase.IsFixedSize): Indicates that the binary string is a fixed size; the number of bits cannot increase or decrease once its been initialized.
* [PopulationSeed](xref:GenFx.GeneticAlgorithm.PopulationSeed): This is configuring the GA to use a [SimplePopulation](xref:GenFx.Components.Populations.SimplePopulation) to contain the genetic entities.
* [SelectionOperator](xref:GenFx.GeneticAlgorithm.SelectionOperator): This is configuring the GA to use a [FitnessProportionateSelectionOperator](xref:GenFx.Components.SelectionOperators.FitnessProportionateSelectionOperator) to select entities from the population to move to the next generation. This type of selection operator bases an entities chances of being selected on its fitness value; the better its fitness value, the better chance it has of being selected. In addition, the selection operator is configured with the following:
    * [SelectionBasedOnFitnessType](xref:GenFx.SelectionOperator.SelectionBasedOnFitnessType): Indicates that the operator should use the entity's raw fitness value to base its selection logic from. To keep things simple in this GA, we're only using raw fitness values; there is no fitness scaling going on.
* [CrossoverOperator](xref:GenFx.GeneticAlgorithm.CrossoverOperator): This is configuring the GA to use a [SinglePointCrossoverOperator](xref:GenFx.Components.Lists.SinglePointCrossoverOperator) to perform crossover operations.  This operator type works with list-based entities to choose a single list element position and swaps the elements on either side of that point between the two entities.  In addition, the crossover operator is configured with the following:
    * [CrossoverRate](xref:GenFx.CrossoverOperator.CrossoverRate): Indicates the percentage chance that a crossover will occur for a given set of parent genetic entities.
* [MutationOperator](xref:GenFx.GeneticAlgorithm.MutationOperator): This is configuring the GA to use a [UniformBitMutationOperator](xref:GenFx.Components.Lists.UniformBitMutationOperator) to perform mutation operations.  This operator type works by potentially flipping the bit value for each bit in a binary string.  In addition, the crossover operator is configured with the following:
    * [MutationRate](xref:GenFx.MutationOperator.MutationRate): Indicates the percentage chance that a mutation will occur for each value unit within a genetic entity.  Since we're using a binary string, that means each bit in the binary string would have a chance to become mutated.
* [Terminator](xref:GenFx.GeneticAlgorithm.Terminator): This is configuring the GA to use a [FitnessTargetTerminator](xref:GenFx.Components.Terminators.FitnessTargetTerminator) which terminates the GA once the specified fitness value has been reached by any entity. In addition, the terminator is configured with the following:
    * [FitnessTarget](xref:GenFx.Components.Terminators.FitnessTargetTerminator.FitnessTarget): Indicates the fitness value an entity must have in order for GA to stop.
    * [FitnessValueType](xref:GenFx.Components.Terminators.FitnessTargetTerminator.FitnessValueType): Indicates the type of fitness value to check.  In this case, we only care about raw fitness values.

## 4. Execute the Algorithm and Process Results
Now it's time to see this GA in action.  Let's execute it and see what the results give us. Append this code to what you've already added in the `RunAlgorithmAsync` method:

    algorithm.GenerationCreated += 
        (sender, e) => Console.WriteLine("Generation: {0}", algorithm.CurrentGeneration);

    await algorithm.InitializeAsync();
    await algorithm.RunAsync();

    IEnumerable<GeneticEntity> top10Entities =
        algorithm.Environment.Populations[0].Entities.GetEntitiesSortedByFitness(
            FitnessType.Raw, FitnessEvaluationMode.Maximize)
        .Reverse()
        .Take(10);

    Console.WriteLine();
    Console.WriteLine("Top 10 entities:");
    foreach (GeneticEntity entity in top10Entities)
    {
        Console.WriteLine("Bits: " + entity.Representation + ", Fitness: " + entity.RawFitnessValue);
    }

    Console.ReadLine();

A few notes about this code:
* The [GenerationCreated](xref:GenFx.GeneticAlgorithm.GenerationCreated) event is being subscribed to here so that we can see in the console output what generation is currently being executed.
* Before an algorithm can be run, it must be initialized by calling the [InitializeAsync](xref:GenFx.GeneticAlgorithm.InitializeAsync) method. Once it's been initialized you can call the [RunAsync](xref:GenFx.GeneticAlgorithm.RunAsync) method, in which case the algorithm is run until terminated by the configured terminator, or the [StepAsync](xref:GenFx.GeneticAlgorithm.StepAsync) method which runs the algorithm to produce only a single generation each time it is called.
* When the algorithm finishes, we collect the top 10 entities with the best fitness values.  Since the terminator was in charge of terminating the algorithm, we know that at least one of those entities has met our goal of containing all 'true' bits.
* The top 10 entities are then printed out to the console using their string representations and fitness values.

That's it!  You've just executed your first genetic algorithm with GenFx!