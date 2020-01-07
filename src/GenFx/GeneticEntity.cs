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
    public abstract class GeneticEntity : GeneticComponentWithAlgorithm, IComparable<GeneticEntity>,
        IComparable, IEquatable<GeneticEntity>
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
            if (this.Algorithm?.FitnessEvaluator != null)
            {
                this.rawFitnessValue = this.scaledFitnessValue = await this.Algorithm.FitnessEvaluator.EvaluateFitnessAsync(this);
            }
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
            clone.IsInitialized = true;
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
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.CopyConfigurationStateTo(entity);

            entity.Age = this.Age;
            entity.rawFitnessValue = this.rawFitnessValue;
            entity.scaledFitnessValue = this.scaledFitnessValue;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: This object is less than <paramref name="other"/>.
        ///  * Zero: This object is equal to <paramref name="other"/>.
        ///  * Greater than zero: This object is greater than <paramref name="other"/>.
        ///  </returns>
        public abstract int CompareTo(GeneticEntity other);

        /// <summary>
        /// Compares the current object with another object.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings:
        ///  * Less than zero: This object is less than <paramref name="obj"/>.
        ///  * Zero: This object is equal to <paramref name="obj"/>
        ///  * Greater than zero: This object is greater than <paramref name="obj"/>.
        ///  </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is GeneticEntity entity))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ObjectIsWrongType, typeof(GeneticEntity)), nameof(obj));
            }

            return this.CompareTo(entity);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to <paramref name="obj"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is GeneticEntity entity))
            {
                return false;
            }

            return this.Equals(entity);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to <paramref name="other"/>; otherwise, false.</returns>
        public bool Equals(GeneticEntity other)
        {
            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Compares two <see cref="GeneticEntity"/> objects for equality.
        /// </summary>
        /// <param name="obj1">The first <see cref="GeneticEntity"/> object to compare.</param>
        /// <param name="obj2">The second <see cref="GeneticEntity"/> object to compare.</param>
        /// <returns>true if the two <see cref="GeneticEntity"/> objects are equal; otherwise, false.</returns>
        public static bool operator ==(GeneticEntity obj1, GeneticEntity obj2)
        {
            return ComparisonHelper.CompareObjects(obj1, obj2) == 0;
        }

        /// <summary>
        /// Compares two <see cref="GeneticEntity"/> objects for inequality.
        /// </summary>
        /// <param name="obj1">The first <see cref="GeneticEntity"/> object to compare.</param>
        /// <param name="obj2">The second <see cref="GeneticEntity"/> object to compare.</param>
        /// <returns>true if the two <see cref="GeneticEntity"/> objects are not equal; otherwise, false.</returns>
        public static bool operator !=(GeneticEntity obj1, GeneticEntity obj2)
        {
            return ComparisonHelper.CompareObjects(obj1, obj2) != 0;
        }

        /// <summary>
        /// Compares two <see cref="GeneticEntity"/> objects to determine if <paramref name="obj1"/> is less than <paramref name="obj2"/>.
        /// </summary>
        /// <param name="obj1">The first <see cref="GeneticEntity"/> object to compare.</param>
        /// <param name="obj2">The second <see cref="GeneticEntity"/> object to compare.</param>
        /// <returns>true if <paramref name="obj1"/> is less than <paramref name="obj2"/>; otherwise, false.</returns>
        public static bool operator <(GeneticEntity obj1, GeneticEntity obj2)
        {
            return ComparisonHelper.CompareObjects(obj1, obj2) < 0;
        }

        /// <summary>
        /// Compares two <see cref="GeneticEntity"/> objects to determine if <paramref name="obj1"/> is greater than <paramref name="obj2"/>.
        /// </summary>
        /// <param name="obj1">The first <see cref="GeneticEntity"/> object to compare.</param>
        /// <param name="obj2">The second <see cref="GeneticEntity"/> object to compare.</param>
        /// <returns>true if <paramref name="obj1"/> is greater than <paramref name="obj2"/>; otherwise, false.</returns>
        public static bool operator >(GeneticEntity obj1, GeneticEntity obj2)
        {
            return ComparisonHelper.CompareObjects(obj1, obj2) > 0;
        }

        public static bool operator <=(GeneticEntity left, GeneticEntity right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >=(GeneticEntity left, GeneticEntity right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
