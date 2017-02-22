using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace TestCommon.Mocks
{
    [DataContract]
    public class MockEntity : GeneticEntity
    {
        public string Identifier;

        [ConfigurationProperty]
        public int TestProperty { get; set; }

        public override string Representation
        {
            get { return this.Identifier; }
        }
        
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            this.Identifier = "11111";
        }
        
        public override void CopyTo(GeneticEntity entity)
        {
            base.CopyTo(entity);
            ((MockEntity)entity).Identifier = this.Identifier;
        }

        public override int CompareTo(GeneticEntity other)
        {
            return Object.ReferenceEquals(this, other) ? 0 : 1;
        }
    }

    [DataContract]
    public class MockEntity2 : GeneticEntity
    {
        public override string Representation
        {
            get { throw new Exception(); }
        }

        public override int CompareTo(GeneticEntity other)
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(GeneticEntity entity)
        {
            throw new Exception();
        }
    }

    [DataContract]
    public class DerivedMockEntity : MockEntity
    {
    }
}
