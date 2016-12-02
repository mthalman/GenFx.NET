using System;
using System.Collections.Generic;
using System.Text;
using GenFx;

namespace GenFxTests.Mocks
{
    class MockEntity : GeneticEntity
    {
        internal string Identifier;

        public override string Representation
        {
            get { return this.Identifier; }
        }

        public MockEntity(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void InitializeCore()
        {
            this.Identifier = "11111";
        }

        public override GeneticEntity Clone()
        {
            MockEntity entity = new MockEntity(this.Algorithm);
            this.CopyTo(entity);
            return entity;
        }

        public override void CopyTo(GeneticEntity entity)
        {
            base.CopyTo(entity);
            ((MockEntity)entity).Identifier = this.Identifier;
        }
    }

    [Component(typeof(MockEntity))]
    class MockEntityConfiguration : GeneticEntityConfiguration
    {
    }

    class MockEntity2 : GeneticEntity
    {
        public override string Representation
        {
            get { throw new Exception(); }
        }

        public MockEntity2(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void InitializeCore()
        {
        }

        public override GeneticEntity Clone()
        {
            throw new Exception();
        }

        public override void CopyTo(GeneticEntity entity)
        {
            throw new Exception();
        }
    }

    [Component(typeof(MockEntity))]
    class MockEntity2Configuration : GeneticEntityConfiguration
    {
    }
}
