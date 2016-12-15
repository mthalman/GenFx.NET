using GenFx.ComponentModel;
using System;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents the "organism" which undergoes evolution in the algorithm.
    /// </summary>
    public interface IGeneticEntity : IGeneticComponent
    {
        /// <summary>
        /// Gets or sets the number of generations this entity has survived without being altered.
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// Gets the fitness value of the entity after it has been scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double ScaledFitnessValue { get; }

        /// <summary>
        /// Gets the fitness value of the entity before being scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        double RawFitnessValue { get; }

        /// <summary>
        /// Returns a clone of this entity.
        /// </summary>
        IGeneticEntity Clone();
        
        /// <summary>
        /// Evaluates the <see cref="IGeneticEntity.RawFitnessValue"/> of the entity.
        /// </summary>
        Task EvaluateFitnessAsync();

        /// <summary>
        /// Returns the appropriate fitness value of the entity based on the the <paramref name="fitnessType"/>.
        /// </summary>
        /// <param name="fitnessType"><see cref="FitnessType"/> indicating which fitness value to
        /// return.</param>
        /// <returns>The appropriate fitness value of the entity.</returns>
        /// <exception cref="ArgumentException"><paramref name="fitnessType"/> value is undefined.</exception>
        double GetFitnessValue(FitnessType fitnessType);
    }

    /// <summary>
    /// Provides the abstract base class for genetic entities in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A genetic entity in a genetic algorithm represents the "organism" which undergoes evolution.  All the genetic
    /// operators such as the <see cref="ISelectionOperator"/>, <see cref="ICrossoverOperator"/>, and
    /// <see cref="IMutationOperator"/> act upon genetic entities to bring about change in the system.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.Entity"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class GeneticEntity<TEntity, TConfiguration> : GeneticComponentWithAlgorithm<TEntity, TConfiguration>, IGeneticEntity
        where TEntity : GeneticEntity<TEntity, TConfiguration>
        where TConfiguration : GeneticEntityConfiguration<TConfiguration, TEntity>
    {
        private double rawFitnessValue;
        private double scaledFitnessValue;

        /// <summary>
        /// When overriden in a derived class, gets the string representation of the entity.
        /// </summary>
        /// <value>The string representation of the entity.</value>
        public abstract string Representation
        {
            get;
        }

        /// <summary>
        /// Gets the number of generations this entity has survived without being altered.
        /// </summary>
        /// <value>The number of generations this entity has survived without being altered.</value>
        public int Age
        {
            get; set;
        }

        /// <summary>
        /// Gets the fitness value of the entity before being scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the entity before being scaled by the <see cref="IFitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </remarks>
        /// <seealso cref="IFitnessEvaluator"/>
        public double RawFitnessValue
        {
            get { return this.rawFitnessValue; }
        }

        /// <summary>
        /// Gets or sets the fitness value of the entity after it has been scaled by the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the entity after it has been scaled by the <see cref="IFitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// <para>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </para>
        /// <para>
        /// This value is equal to <see cref="RawFitnessValue"/> if a 
        /// <see cref="IFitnessScalingStrategy"/> is not being used.
        /// </para>
        /// </remarks>
        /// <seealso cref="IFitnessEvaluator"/>
        /// <seealso cref="IFitnessScalingStrategy"/>
        public double ScaledFitnessValue
        {
            get { return this.scaledFitnessValue; }
            set { this.scaledFitnessValue = value; }
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this entity.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected GeneticEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            this.Initialize();
        }

        /// <summary>
        /// Restores the state of this component.
        /// </summary>
        /// <param name="state">The state of the component to restore from.</param>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.Age = (int)state[nameof(this.Age)];
            this.rawFitnessValue = (double)state[nameof(this.rawFitnessValue)];
            this.scaledFitnessValue = (double)state[nameof(this.scaledFitnessValue)];
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.Age)] = this.Age;
            state[nameof(this.rawFitnessValue)] = this.rawFitnessValue;
            state[nameof(this.scaledFitnessValue)] = this.scaledFitnessValue;
        }

        /// <summary>
        /// Evaluates the <see cref="RawFitnessValue"/> of the entity.
        /// </summary>
        public async Task EvaluateFitnessAsync()
        {
            this.rawFitnessValue = this.scaledFitnessValue = await this.Algorithm.Operators.FitnessEvaluator.EvaluateFitnessAsync(this);
        }

        /// <summary>
        /// Returns the appropriate fitness value of the <b>GeneticEntity</b> based on the the <paramref name="fitnessType"/>.
        /// </summary>
        /// <param name="fitnessType"><see cref="FitnessType"/> indicating which fitness value to
        /// return.</param>
        /// <returns>The appropriate fitness value of the <b>GeneticEntity</b>.</returns>
        /// <exception cref="ArgumentException"><paramref name="fitnessType"/> value is undefined.</exception>
        public double GetFitnessValue(FitnessType fitnessType)
        {
            if (!FitnessTypeHelper.IsDefined(fitnessType))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(FitnessType), "fitnessType");
            }

            if (fitnessType == FitnessType.Raw)
            {
                return this.rawFitnessValue;
            }
            else
            {
                return this.scaledFitnessValue;
            }
        }

        /// <summary>
        /// Initializes the entity with its default data.
        /// </summary>
        public void Initialize()
        {
            this.InitializeCore();
        }

        /// <summary>
        /// Initializes the entity with its default data.
        /// </summary>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// entity be filled with random data to provide diversity in the population.
        /// </remarks>
        protected virtual void InitializeCore()
        {
            this.rawFitnessValue = 0;
            this.scaledFitnessValue = 0;
            this.Age = 0;
        }

        /// <summary>
        /// Returns the string representation of the entity.
        /// </summary>
        /// <returns>The string representation of the entity.</returns>
        public override string ToString()
        {
            return this.Representation;
        }
        
        /// <summary>
        /// When overriden by a derived class, returns a clone of this entity.
        /// </summary>
        /// <returns>A clone of this entity.</returns>
        /// <remarks>
        /// The <see cref="GeneticComponentWithAlgorithm{TComponent, TConfiguration}.Algorithm"/> reference is maintained in the returned entity.
        /// For all other state of the entity, a deep clone is used.
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="CopyTo(GeneticEntity{TEntity, TConfiguration})"/> method is also overriden.  In that case, the 
        /// suggested implementation of this method is the following:
        /// <code>
        /// <![CDATA[
        /// protected override GeneticEntity Clone()
        /// {
        ///     MyEntity myEntity = new MyEntity(this.Algorithm);
        ///     this.CopyTo(myEntity);
        ///     return myEntity;
        /// }
        /// ]]>
        /// </code>
        /// </remarks>
        public abstract GeneticEntity<TEntity, TConfiguration> Clone();

        IGeneticEntity IGeneticEntity.Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// Copies the state from this instance to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity{TEntity, TConfiguration}"/> to which state is to be copied.</param>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to copy the state of this instance
        /// to the entity passed in.
        /// </para>
        /// <para>
        /// <b>Notes to inheritors:</b> When overriding this method, it is necessary to call the
        /// <b>CopyTo</b> method of the base class.  This method should be used in conjunction with
        /// the <see cref="Clone()"/> method.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void CopyTo(GeneticEntity<TEntity, TConfiguration> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.SetAlgorithm(this.Algorithm);

            entity.Age = this.Age;
            entity.rawFitnessValue = this.rawFitnessValue;
            entity.scaledFitnessValue = this.scaledFitnessValue;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="IGeneticEntity"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    public interface IGeneticEntityConfiguration : IComponentConfiguration
    {
    }

    /// <summary>
    /// Represents the configuration of <see cref="GeneticEntity{TEntity, TConfiguration}"/>.
    /// </summary>
    public abstract class GeneticEntityConfiguration<TConfiguration, TEntity> : ComponentConfiguration<TConfiguration, TEntity>, IGeneticEntityConfiguration
        where TConfiguration : GeneticEntityConfiguration<TConfiguration, TEntity>
        where TEntity : GeneticEntity<TEntity, TConfiguration>
    {
    }
}
