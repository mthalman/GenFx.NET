using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for genetic entities in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A genetic entity in a genetic algorithm represents the "organism" which undergoes evolution.  All the genetic
    /// operators such as the <see cref="SelectionOperator"/>, <see cref="CrossoverOperator"/>, and
    /// <see cref="MutationOperator"/> act upon genetic entities to bring about change in the system.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.Entity"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class GeneticEntity : GeneticComponent
    {
        private double rawFitnessValue;
        private double scaledFitnessValue;
        private int age;
        private GeneticAlgorithm algorithm;

        /// <summary>
        /// When overriden in a derived class, gets the string representation of the <see cref="GeneticEntity"/>.
        /// </summary>
        /// <value>The string representation of the <see cref="GeneticEntity"/>.</value>
        public abstract string Representation
        {
            get;
        }

        /// <summary>
        /// Gets the number of generations this <see cref="GeneticEntity"/> has survived without being altered.
        /// </summary>
        /// <value>The number of generations this <see cref="GeneticEntity"/> has survived without being altered.</value>
        public int Age
        {
            get { return this.age; }
            internal set { this.age = value; }
        }

        /// <summary>
        /// Gets the fitness value of the <see cref="GeneticEntity"/> before being scaled by the <see cref="FitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the <see cref="GeneticEntity"/> before being scaled by the <see cref="FitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </remarks>
        /// <seealso cref="FitnessEvaluator"/>
        [DisplayName("Raw Fitness Value")]
        public double RawFitnessValue
        {
            get { return this.rawFitnessValue; }
        }

        /// <summary>
        /// Gets or sets the fitness value of the <see cref="GeneticEntity"/> after it has been scaled by the <see cref="FitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the <see cref="GeneticEntity"/> after it has been scaled by the <see cref="FitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// <para>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </para>
        /// <para>
        /// This value is equal to <see cref="GeneticEntity.RawFitnessValue"/> if a 
        /// <see cref="FitnessScalingStrategy"/> is not being used.
        /// </para>
        /// </remarks>
        /// <seealso cref="FitnessEvaluator"/>
        /// <seealso cref="FitnessScalingStrategy"/>
        [DisplayName("Scaled Fitness Value")]
        public double ScaledFitnessValue
        {
            get { return this.scaledFitnessValue; }
            set { this.scaledFitnessValue = value; }
        }

        /// <summary>
        /// Gets the <see cref="GeneticAlgorithm"/> using this <see cref="GeneticEntity"/>.
        /// </summary>
        /// <value>
        /// The <see cref="GeneticAlgorithm"/> using this <see cref="GeneticEntity"/>.
        /// </value>
        public GeneticAlgorithm Algorithm
        {
            get { return this.algorithm; }
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.algorithm.ConfigurationSet.Entity; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="GeneticEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected GeneticEntity(GeneticAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            this.algorithm = algorithm;
         
            algorithm.ValidateComponentConfiguration(this);
        }

        /// <summary>
        /// Restores the state of this component.
        /// </summary>
        /// <param name="state">The state of the component to restore from.</param>
        protected internal override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.age = (int)state[nameof(this.age)];
            this.rawFitnessValue = (double)state[nameof(this.rawFitnessValue)];
            this.scaledFitnessValue = (double)state[nameof(this.scaledFitnessValue)];
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        protected override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.age)] = this.age;
            state[nameof(this.rawFitnessValue)] = this.rawFitnessValue;
            state[nameof(this.scaledFitnessValue)] = this.scaledFitnessValue;
        }

        /// <summary>
        /// Evaluates the <see cref="GeneticEntity.RawFitnessValue"/> of the <see cref="GeneticEntity"/>.
        /// </summary>
        internal async Task EvaluateFitnessAsync()
        {
            this.rawFitnessValue = this.scaledFitnessValue = await this.algorithm.Operators.FitnessEvaluator.EvaluateFitnessAsync(this);
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
        /// Initializes the <see cref="GeneticEntity"/> with its default data.
        /// </summary>
        public void Initialize()
        {
            this.InitializeCore();
        }

        /// <summary>
        /// Initializes the <see cref="GeneticEntity"/> with its default data.
        /// </summary>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="GeneticEntity"/> be filled with random data to provide diversity in the <see cref="Population"/>.
        /// </remarks>
        protected virtual void InitializeCore()
        {
            this.rawFitnessValue = 0;
            this.scaledFitnessValue = 0;
            this.age = 0;
        }

        /// <summary>
        /// Returns the string representation of the <see cref="GeneticEntity"/>.
        /// </summary>
        /// <returns>The string representation of the <see cref="GeneticEntity"/>.</returns>
        public override string ToString()
        {
            return this.Representation;
        }

        /// <summary>
        /// When overriden by a derived class, returns a clone of this <see cref="GeneticEntity"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="GeneticEntity"/>.</returns>
        /// <remarks>
        /// The <see cref="GeneticEntity.Algorithm"/> reference is maintained in the returned <see cref="GeneticEntity"/>.  
        /// For all other state of the <see cref="GeneticEntity"/>, a deep clone is used.
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="GeneticEntity.CopyTo(GeneticEntity)"/> method is also overriden.  In that case, the 
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
        public abstract GeneticEntity Clone();

        /// <summary>
        /// Copies the state from this <see cref="GeneticEntity"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to copy the state of <see cref="GeneticEntity"/>
        /// to the <see cref="GeneticEntity"/> passed in.
        /// </para>
        /// <para>
        /// <b>Notes to inheritors:</b> When overriding this method, it is necessary to call the
        /// <b>CopyTo</b> method of the base class.  This method should be used in conjunction with
        /// the <see cref="GeneticEntity.Clone()"/> method.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void CopyTo(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.age = this.age;
            entity.algorithm = this.algorithm;
            entity.rawFitnessValue = this.rawFitnessValue;
            entity.scaledFitnessValue = this.scaledFitnessValue;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="GeneticEntity"/>.
    /// </summary>
    [Component(typeof(GeneticEntity))]
    public abstract class GeneticEntityConfiguration : ComponentConfiguration
    {
    }
}
