using GenFx;
using GenFx.ComponentLibrary.Trees;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Trees.SingleNodeTreeCrossoverOperator and is intended
    ///to contain all GenFx.ComponentLibrary.Trees.SingleNodeTreeCrossoverOperator Unit Tests
    ///</summary>
    [TestClass()]
    public class SingleNodeTreeCrossoverOperatorTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that the Crossover method works correctly.
        /// </summary>
        [TestMethod]
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
            SingleNodeTreeCrossoverOperator op = new SingleNodeTreeCrossoverOperator();
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

            Assert.AreEqual(1, rootNode1.Value, "Wrong TreeNode.");
            Assert.AreEqual(3, rootNode1.ChildNodes.Count, "Incorrect number of children.");
            Assert.AreEqual(2, rootNode1.ChildNodes[0].Value, "Wrong TreeNode.");
            Assert.AreEqual(7, rootNode1.ChildNodes[1].Value, "Wrong TreeNode.");
            Assert.AreEqual(1, rootNode1.ChildNodes[1].ChildNodes.Count, "Incorrect number of children.");
            Assert.AreEqual(8, rootNode1.ChildNodes[1].ChildNodes[0].Value, "Wrong TreeNode.");
            Assert.AreEqual(1, rootNode1.ChildNodes[1].ChildNodes[0].ChildNodes.Count, "Incorrect number of children.");
            Assert.AreEqual(9, rootNode1.ChildNodes[1].ChildNodes[0].ChildNodes[0].Value, "Wrong TreeNode.");
            Assert.AreEqual(5, rootNode1.ChildNodes[2].Value, "Wrong TreeNode.");

            Assert.AreEqual(6, rootNode2.Value, "Wrong TreeNode.");
            Assert.AreEqual(1, rootNode2.ChildNodes.Count, "Incorrect number of children.");
            Assert.AreEqual(3, rootNode2.ChildNodes[0].Value, "Wrong TreeNode.");
            Assert.AreEqual(1, rootNode2.ChildNodes[0].ChildNodes.Count, "Incorrect number of children.");
            Assert.AreEqual(4, rootNode2.ChildNodes[0].ChildNodes[0].Value, "Wrong TreeNode.");
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
                        Assert.AreEqual(5, maxValue, "TreeSize is incorrect.");
                        return 2;
                    case 2:
                        Assert.AreEqual(4, maxValue, "TreeSize is incorrect.");
                        return 1;
                    default:
                        Assert.Fail("GetRandomValue should not be called this many times.");
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
