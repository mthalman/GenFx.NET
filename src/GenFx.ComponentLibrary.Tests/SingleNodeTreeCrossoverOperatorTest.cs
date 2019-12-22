using GenFx.ComponentLibrary.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SingleNodeTreeCrossoverOperator"/> class.
    ///</summary>
    public class SingleNodeTreeCrossoverOperatorTest : IDisposable
    {
        public void Dispose()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [Fact]
        public void SingleNodeTreeCrossoverOperator_Crossover()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new FakeTreeEntity(),
                CrossoverOperator = new SingleNodeTreeCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };
            SingleNodeTreeCrossoverOperator op = (SingleNodeTreeCrossoverOperator)algorithm.CrossoverOperator;
            op.Initialize(algorithm);

            FakeTreeEntity entity1 = new FakeTreeEntity();
            entity1.Initialize(algorithm);
            entity1.SetRootNode(new FakeTreeNode(1));
            entity1.RootNode.ChildNodes.Add(new FakeTreeNode(2));
            entity1.RootNode.ChildNodes.Add(new FakeTreeNode(3));
            entity1.RootNode.ChildNodes[1].ChildNodes.Add(new FakeTreeNode(4));
            entity1.RootNode.ChildNodes.Add(new FakeTreeNode(5));

            FakeTreeEntity entity2 = new FakeTreeEntity();
            entity2.Initialize(algorithm);
            entity2.SetRootNode(new FakeTreeNode(6));
            entity2.RootNode.ChildNodes.Add(new FakeTreeNode(7));
            entity2.RootNode.ChildNodes[0].ChildNodes.Add(new FakeTreeNode(8));
            entity2.RootNode.ChildNodes[0].ChildNodes[0].ChildNodes.Add(new FakeTreeNode(9));

            RandomNumberService.Instance = new TestRandomUtil();

            IList<GeneticEntity> result = op.Crossover(new GeneticEntity[] { entity1, entity2 }).ToList();

            FakeTreeNode rootNode1 = (FakeTreeNode)((TreeEntityBase)result[0]).RootNode;
            FakeTreeNode rootNode2 = (FakeTreeNode)((TreeEntityBase)result[1]).RootNode;

            Assert.Equal(1, rootNode1.Value);
            Assert.Equal(3, rootNode1.ChildNodes.Count);
            Assert.Equal(2, rootNode1.ChildNodes[0].Value);
            Assert.Equal(7, rootNode1.ChildNodes[1].Value);
            Assert.Single(rootNode1.ChildNodes[1].ChildNodes);
            Assert.Equal(8, rootNode1.ChildNodes[1].ChildNodes[0].Value);
            Assert.Single(rootNode1.ChildNodes[1].ChildNodes[0].ChildNodes);
            Assert.Equal(9, rootNode1.ChildNodes[1].ChildNodes[0].ChildNodes[0].Value);
            Assert.Equal(5, rootNode1.ChildNodes[2].Value);

            Assert.Equal(6, rootNode2.Value);
            Assert.Single(rootNode2.ChildNodes);
            Assert.Equal(3, rootNode2.ChildNodes[0].Value);
            Assert.Single(rootNode2.ChildNodes[0].ChildNodes);
            Assert.Equal(4, rootNode2.ChildNodes[0].ChildNodes[0].Value);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing null parents.
        /// </summary>
        [Fact]
        public void SingleNodeTreeCrossoverOperator_GenerateCrossover_NullParents()
        {
            SingleNodeTreeCrossoverOperator op = new SingleNodeTreeCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateCrossover", (IList<GeneticEntity>)null));
        }

        private class TestRandomUtil : IRandomNumberService
        {
            private int callCount;
            public int GetRandomValue(int maxValue)
            {
                this.callCount++;

                switch (this.callCount)
                {
                    case 1:
                        Assert.Equal(5, maxValue);
                        return 2;
                    case 2:
                        Assert.Equal(4, maxValue);
                        return 1;
                    default:
                        Assert.False(true, "GetRandomValue should not be called this many times.");
                        return 0;
                }
            }

            public double GetDouble()
            {
                return new RandomNumberService().GetDouble();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeTreeNode : TreeNode<int>
        {
            public FakeTreeNode()
            { }

            public FakeTreeNode(int id)
            {
                this.Value = id;
            }

            public override TreeNode Clone(TreeEntityBase newTree, TreeNode newParentNode)
            {
                FakeTreeNode clone = new FakeTreeNode();
                this.CopyTo(clone, newTree, newParentNode);
                return clone;
            }
        }

        private class FakeTreeEntity : TreeEntity<FakeTreeNode>
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }
        }
    }
}
