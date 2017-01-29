using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using System.Runtime.Serialization;

namespace GenFxTests.Mocks
{
    [DataContract]
    class MockEntity : GeneticEntity
    {
        internal string Identifier;

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
    }

    [DataContract]
    class MockEntity2 : GeneticEntity
    {
        public override string Representation
        {
            get { throw new Exception(); }
        }
        
        public override void CopyTo(GeneticEntity entity)
        {
            throw new Exception();
        }
    }
}
