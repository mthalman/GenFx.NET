using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for genetic entities in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// A genetic entity in a genetic algorithm represents the "organism" which undergoes evolution.  All the genetic
    /// operators such as the <see cref="SelectionOperator"/>, <see cref="CrossoverOperator"/>, and
    /// <see cref="MutationOperator"/> act upon genetic entities to bring about change in the system.
    /// </remarks>
    [DataContract]
    public abstract class GeneticEntity : GeneticComponentWithAlgorithm
    {
        [DataMember]
        private double rawFitnessValue;

        [DataMember]
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
        /// Gets or sets the number of generations this entity has survived without being altered.
        /// </summary>
        /// <value>The number of generations this entity has survived without being altered.</value>
        [DataMember]
        public int Age
        {
            get; set;
        }

        /// <summary>
        /// Gets the fitness value of the entity before being scaled by the <see cref="FitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the entity before being scaled by the <see cref="FitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </remarks>
        /// <seealso cref="FitnessEvaluator"/>
        public double RawFitnessValue
        {
            get { return this.rawFitnessValue; }
        }

        /// <summary>
        /// Gets or sets the fitness value of the entity after it has been scaled by the <see cref="FitnessScalingStrategy"/>.
        /// </summary>
        /// <value>The fitness value of the entity after it has been scaled by the <see cref="FitnessScalingStrategy"/>.</value>
        /// <remarks>
        /// <para>
        /// The fitness value is a relative measurement of how close a entity is to meeting the goal
        /// of the genetic algorithm.
        /// </para>
        /// <para>
        /// This value is equal to <see cref="RawFitnessValue"/> if a 
        /// <see cref="FitnessScalingStrategy"/> is not being used.
        /// </para>
        /// </remarks>
        /// <seealso cref="FitnessEvaluator"/>
        /// <seealso cref="FitnessScalingStrategy"/>
        public double ScaledFitnessValue
        {
            get { return this.scaledFitnessValue; }
            set { this.scaledFitnessValue = value; }
        }
        
        /// <summary>
        /// Evaluates the <see cref="RawFitnessValue"/> of the entity.
        /// </summary>
        public async Task EvaluateFitnessAsync()
        {
            this.rawFitnessValue = this.scaledFitnessValue = await this.Algorithm.FitnessEvaluator.EvaluateFitnessAsync(this);
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
            if (!Enum.IsDefined(typeof(FitnessType), fitnessType))
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
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);
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
        /// Returns a clone of this entity.
        /// </summary>
        /// <returns>A clone of this entity.</returns>
        public GeneticEntity Clone()
        {
            GeneticEntity clone = (GeneticEntity)this.CreateNew();
            clone.Algorithm = this.Algorithm;
            this.CopyTo(clone);
            return clone;
        }

        /// <summary>
        /// Copies the state from this instance to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to copy the state of this instance
        /// to the entity passed in.
        /// </para>
        /// <para>
        /// <b>Notes to inheritors:</b> When overriding this method, it is necessary to call the
        /// <b>CopyTo</b> method of the base class.  It is not necessary to copy the state of properties that
        /// are adorned with the <see cref="ConfigurationPropertyAttribute"/>; these properties have their state
        /// automatically copied as part of the base implementation of this method.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void CopyTo(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.CopyConfigurationStateTo(entity);

            entity.Age = this.Age;
            entity.rawFitnessValue = this.rawFitnessValue;
            entity.scaledFitnessValue = this.scaledFitnessValue;
        }
    }
}
