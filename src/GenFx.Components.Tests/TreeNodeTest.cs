using GenFx.Components.Trees;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeNode"/> class.
    ///</summary>
    public class TreeNodeTest
    {
        /// <summary>
        /// Tests that the AppendChild method works correctly.
        /// </summary>
        [Fact]
        public void TreeNode_AppendChild()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);

            TreeNode child = new TreeNode();
            node.AppendChild(child);
            Assert.Same(child, node.ChildNodes[0]);
            Assert.Same(node, child.ParentNode);
            Assert.Same(entity, child.Tree);
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeNode_AppendChild_NullNode()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentNullException>(() => node.AppendChild(null));
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeNode_AppendChildCore_NullNode()
        {
            TreeNode node = new TreeNode();
            PrivateObject accessor = new PrivateObject(node);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("AppendChildCore", (TreeNode)null));
        }

        /// <summary>
        /// Tests that the InsertChild method works correctly.
        /// </summary>
        [Fact]
        public void TreeNode_InsertChild()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);

            TreeNode child1 = new TreeNode();
            TreeNode child2 = new TreeNode();
            node.InsertChild(0, child1);
            node.InsertChild(0, child2);

            Assert.Same(child2, node.ChildNodes[0]);
            Assert.Same(node, child2.ParentNode);
            Assert.Same(entity, child2.Tree);
            Assert.Same(child1, node.ChildNodes[1]);
            Assert.Same(node, child1.ParentNode);
            Assert.Same(entity, child1.Tree);
        }

        /// <summary>
        /// Tests that an exception is thrown when a child is inserted with a parent not assigned to a tree.
        /// </summary>
        [Fact]
        public void TreeNode_InsertChild_NoTree()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();

            TreeNode child1 = new TreeNode();
            Assert.Throws<InvalidOperationException>(() => node.InsertChild(0, child1));
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeNode_InsertChild_NullNode()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentNullException>(() => node.InsertChild(0, null));
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeNode_InsertChildCore_NullNode()
        {
            TreeNode node = new TreeNode();
            PrivateObject accessor = new PrivateObject(node);
            Assert.Throws<ArgumentNullException>(() => accessor.Invoke("InsertChildCore", 0, (TreeNode)null));
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [Fact]
        public void TreeNode_Clone()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity();
            newEntity.Initialize(algorithm);
            TreeNode newParent = new TreeNode();
            newEntity.SetRootNode(newParent);
            TreeNode clone = node.Clone(newEntity, newParent);

            Assert.NotSame(node, clone);
            Assert.NotSame(node.ChildNodes[0], clone.ChildNodes[0]);
            Assert.Same(newEntity, clone.Tree);
            Assert.Same(newParent, clone.ParentNode);
            Assert.Equal(node.Value, clone.Value);
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeNode_Clone_NullTree()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentNullException>(() => node.Clone(null, new TreeNode()));
        }

        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [Fact]
        public void TreeNode_CopyTo()
        {
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity();
            newEntity.Initialize(algorithm);
            TreeNode newParent = new TreeNode();
            newEntity.SetRootNode(newParent);
            TreeNode newNode = new TreeNode();
            node.CopyTo(newNode, newEntity, newParent);

            Assert.Same(newEntity, newNode.Tree);
            Assert.Same(newParent, newNode.ParentNode);
            Assert.NotSame(node.ChildNodes[0], newNode.ChildNodes[0]);
            Assert.Equal(node.Value, newNode.Value);
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null node.
        /// </summary>
        [Fact]
        public void TreeNode_CopyTo_NullNode()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentNullException>(() => node.CopyTo(null, new TestTreeEntity(), new TreeNode()));
        }

        /// <summary>
        /// Tests an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeNode_CopyTo_NullTree()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentNullException>(() => node.CopyTo(new TreeNode(), null, new TreeNode()));
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [Fact]
        public void TreeNode_Serialization()
        {
            TreeNode node = new TreeNode
            {
                Value = 3,
                ParentNode = new TreeNode(),
                Tree = new TestTreeEntity()
            };
            node.ChildNodes.Add(new TreeNode());

            TreeNode result = (TreeNode)SerializationHelper.TestSerialization(node, new Type[] {
                typeof(TestTreeEntity)
            });

            Assert.Equal(node.Value, result.Value);
            Assert.IsType<TreeNode>(node.ParentNode);
            Assert.IsType<TestTreeEntity>(node.Tree);
            Assert.IsType<TreeNode>(node.ChildNodes[0]);
        }

        /// <summary>
        /// Tests that an exception is thrown when trying to set <see cref="TreeNode.Value"/> to an invalid value.
        /// </summary>
        [Fact]
        public void TreeNode_InvalidValue()
        {
            TreeNode node = new TreeNode();
            Assert.Throws<ArgumentException>(() => node.Value = new TreeNodeTest());
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            return algorithm;
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
