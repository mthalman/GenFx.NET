using GenFx.ComponentLibrary.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeEntity{TNode}"/> class.
    /// </summary>
    [TestClass]
    public class TreeEntityBaseTest
    {
        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CopyTo()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            entity.Age = 10;
            entity.SetRootNode(new TreeNode());
            TestTreeEntity newEntity = new TestTreeEntity();
            newEntity.Initialize(algorithm);
            entity.CopyTo(newEntity);

            Assert.AreEqual(entity.Age, newEntity.Age, "Entity class members not copied correctly.");
            Assert.IsNotNull(entity.RootNode, "RootNode not copied.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CopyToNullEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the GetSize method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_GetSize()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);

            int size = entity.GetSize();
            Assert.AreEqual(0, size, "Incorrect size.");

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            size = entity.GetSize();
            Assert.AreEqual(4, size, "Incorrect size.");
        }

        /// <summary>
        /// Tests that the GetPrefixTree method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_GetPrefixTree()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            int loopCount = 0;
            IEnumerable<TreeNode> nodes = entity.GetPrefixTree();
            foreach (TreeNode node in nodes)
            {
                switch (loopCount)
                {
                    case 0:
                        Assert.AreSame(entity.RootNode, node, "Incorrect node.");
                        break;
                    case 1:
                        Assert.AreSame(entity.RootNode.ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 2:
                        Assert.AreSame(entity.RootNode.ChildNodes[0].ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 3:
                        Assert.AreSame(entity.RootNode.ChildNodes[1], node, "Incorrect node.");
                        break;
                    default:
                        Assert.Fail("More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the GetPostfixTree method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_GetPostfixTree()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            int loopCount = 0;
            IEnumerable<TreeNode> nodes = entity.GetPostfixTree();
            foreach (TreeNode node in nodes)
            {
                switch (loopCount)
                {
                    case 0:
                        Assert.AreSame(entity.RootNode.ChildNodes[0].ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 1:
                        Assert.AreSame(entity.RootNode.ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 2:
                        Assert.AreSame(entity.RootNode.ChildNodes[1], node, "Incorrect node.");
                        break;
                    case 3:
                        Assert.AreSame(entity.RootNode, node, "Incorrect node.");
                        break;
                    default:
                        Assert.Fail("More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the SetRootNode method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_SetRootNode()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            TreeNode childNode = new TreeNode();
            PrivateObject nodeAccessor = new PrivateObject(childNode);
            nodeAccessor.SetField("parentNode", node);

            TreeNode grandChildNode = new TreeNode();

            node.ChildNodes.Add(childNode);
            childNode.ChildNodes.Add(grandChildNode);

            entity.SetRootNode(childNode);

            Assert.AreSame(childNode, entity.RootNode, "RootNode not set correctly.");
            Assert.IsNull(childNode.ParentNode, "ParentNode should be null.");
            Assert.AreSame(entity, childNode.Tree, "Tree not set correctly.");
            Assert.AreSame(entity, grandChildNode.Tree, "Tree not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null node.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_SetRootNode_NullNode()
        {
            TestTreeEntity tree = new TestTreeEntity();
            AssertEx.Throws<ArgumentNullException>(() => tree.SetRootNode(null));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_Serialization()
        {
            TestTreeEntity entity = new TestTreeEntity();

            PrivateObject privObj = new PrivateObject(entity, new PrivateType(typeof(TreeEntityBase)));
            TreeNode node = new TreeNode();
            privObj.SetField("rootNode", node);

            TestTreeEntity result = (TestTreeEntity)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.IsInstanceOfType(result.RootNode, typeof(TreeNode));
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CompareTo_Equal()
        {
            TestTreeEntity entity1 = CreateTestTree();
            TestTreeEntity entity2 = CreateTestTree();

            int result = entity1.CompareTo(entity2);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CompareTo_GreaterTree()
        {
            TestTreeEntity entity1 = CreateTestTree();
            entity1.RootNode.ChildNodes.RemoveAt(0);
            TestTreeEntity entity2 = CreateTestTree();

            int result = entity1.CompareTo(entity2);
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CompareTo_LesserTree()
        {
            TestTreeEntity entity1 = CreateTestTree();
            TestTreeEntity entity2 = CreateTestTree();
            entity2.RootNode.ChildNodes.RemoveAt(0);

            int result = entity1.CompareTo(entity2);
            Assert.AreEqual(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CompareTo_Null()
        {
            TestTreeEntity entity1 = CreateTestTree();

            int result = entity1.CompareTo(null);
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid entity to <see cref="TreeEntityBase.CompareTo"/>.
        /// </summary>
        [TestMethod]
        public void TreeEntityBase_CompareTo_InvalidEntity()
        {
            TestTreeEntity entity1 = CreateTestTree();
            AssertEx.Throws<ArgumentException>(() => entity1.CompareTo(new MockEntity()));
        }

        private static TestTreeEntity CreateTestTree()
        {
            TestTreeEntity entity = new TestTreeEntity();
            TreeNode root = new TreeNode();
            root.Value = 1;
            entity.SetRootNode(root);

            TreeNode node2 = new TreeNode();
            node2.Value = 2;
            root.ChildNodes.Add(node2);

            TreeNode node3 = new TreeNode();
            node3.Value = 3;
            root.ChildNodes.Add(node3);

            TreeNode node4 = new TreeNode();
            node4.Value = 4;
            node3.ChildNodes.Add(node4);

            return entity;
        }

        [DataContract]
        private class TestTreeEntity : TreeEntityBase
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

        }
    }
}
