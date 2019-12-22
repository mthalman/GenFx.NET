using GenFx.ComponentLibrary.Trees;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeEntity{TNode}"/> class.
    /// </summary>
    public class TreeEntityBaseTest
    {
        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [Fact]
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

            Assert.Equal(entity.Age, newEntity.Age);
            Assert.NotNull(entity.RootNode);
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [Fact]
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
            Assert.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the GetSize method works correctly.
        /// </summary>
        [Fact]
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
            Assert.Equal(0, size);

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            size = entity.GetSize();
            Assert.Equal(4, size);
        }

        /// <summary>
        /// Tests that the GetPrefixTree method works correctly.
        /// </summary>
        [Fact]
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
                        Assert.Same(entity.RootNode, node);
                        break;
                    case 1:
                        Assert.Same(entity.RootNode.ChildNodes[0], node);
                        break;
                    case 2:
                        Assert.Same(entity.RootNode.ChildNodes[0].ChildNodes[0], node);
                        break;
                    case 3:
                        Assert.Same(entity.RootNode.ChildNodes[1], node);
                        break;
                    default:
                        Assert.True(false, "More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the GetPostfixTree method works correctly.
        /// </summary>
        [Fact]
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
                        Assert.Same(entity.RootNode.ChildNodes[0].ChildNodes[0], node);
                        break;
                    case 1:
                        Assert.Same(entity.RootNode.ChildNodes[0], node);
                        break;
                    case 2:
                        Assert.Same(entity.RootNode.ChildNodes[1], node);
                        break;
                    case 3:
                        Assert.Same(entity.RootNode, node);
                        break;
                    default:
                        Assert.True(false, "More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the SetRootNode method works correctly.
        /// </summary>
        [Fact]
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

            Assert.Same(childNode, entity.RootNode);
            Assert.Null(childNode.ParentNode);
            Assert.Same(entity, childNode.Tree);
            Assert.Same(entity, grandChildNode.Tree);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeEntityBase_SetRootNode_NullNode()
        {
            TestTreeEntity tree = new TestTreeEntity();
            Assert.Throws<ArgumentNullException>(() => tree.SetRootNode(null));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void TreeEntityBase_Serialization()
        {
            TestTreeEntity entity = new TestTreeEntity();

            PrivateObject privObj = new PrivateObject(entity, new PrivateType(typeof(TreeEntityBase)));
            TreeNode node = new TreeNode();
            privObj.SetField("rootNode", node);

            TestTreeEntity result = (TestTreeEntity)SerializationHelper.TestSerialization(entity, new Type[0]);

            Assert.IsType<TreeNode>(result.RootNode);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityBase_CompareTo_Equal()
        {
            TestTreeEntity entity1 = CreateTestTree();
            TestTreeEntity entity2 = CreateTestTree();

            int result = entity1.CompareTo(entity2);
            Assert.Equal(0, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityBase_CompareTo_GreaterTree()
        {
            TestTreeEntity entity1 = CreateTestTree();
            entity1.RootNode.ChildNodes.RemoveAt(0);
            TestTreeEntity entity2 = CreateTestTree();

            int result = entity1.CompareTo(entity2);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityBase_CompareTo_LesserTree()
        {
            TestTreeEntity entity1 = CreateTestTree();
            TestTreeEntity entity2 = CreateTestTree();
            entity2.RootNode.ChildNodes.RemoveAt(0);

            int result = entity1.CompareTo(entity2);
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityBase.CompareTo"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityBase_CompareTo_Null()
        {
            TestTreeEntity entity1 = CreateTestTree();

            int result = entity1.CompareTo(null);
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid entity to <see cref="TreeEntityBase.CompareTo"/>.
        /// </summary>
        [Fact]
        public void TreeEntityBase_CompareTo_InvalidEntity()
        {
            TestTreeEntity entity1 = CreateTestTree();
            Assert.Throws<ArgumentException>(() => entity1.CompareTo(new MockEntity()));
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
