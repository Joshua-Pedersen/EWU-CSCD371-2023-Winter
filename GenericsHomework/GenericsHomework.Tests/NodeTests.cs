using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenericsHomework.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void Node_CreateNode_HasValue()
        {
            // Arrange

            int testVal = 42;

            // Act

            Node<int> testNode = new(testVal);

            // Assert

            Assert.AreEqual(testVal, testNode.Value);
        }

        [TestMethod]
        public void Node_CreateNodeInt_ValueToString()
        {
            // Arrange

            int testVal = 1;

            // Act

            Node<int> testNode = new(testVal);

            // Assert

            Assert.AreEqual(testVal.ToString(), testNode.ToString());
        }

        [TestMethod]
        public void Node_CreateNodeDouble_ValueToString()
        {
            // Arrange

            double testVal = 3.14;

            // Act

            Node<double> testNode = new(testVal);

            // Assert

            Assert.AreEqual(testVal.ToString(), testNode.ToString());
        }

        [TestMethod]
        public void Node_CreateNodeString_ValueToString()
        {
            // Arrange

            string testVal = "this is a test";

            // Act

            Node<string> testNode = new(testVal);

            // Assert

            Assert.AreEqual(testVal.ToString(), testNode.ToString());
        }

        [TestMethod]
        public void Node_CreateNode_NextReturnsValue()
        {
            // Arrange

            int testVal = 13;

            // Act

            Node<int> testNode = new(testVal);

            // Assert

            Assert.AreEqual(testNode, testNode.Next);
        }

        [TestMethod]
        public void Node_AppendNode_NodeNextEqual()
        {
            // Arrange

            int testVal1 = 7;
            int testVal2 = 8;

            // Act

            Node<int> testNode = new(testVal1);
            Node<int> testNode2 = testNode.Append(testVal2);

            // Assert

            Assert.AreEqual(testNode.Next, testNode2);
        }

        [TestMethod]
        public void Node_Clear_CurrentNode()
        {
            // Arrange

            int testVal1 = 1;
            int testVal2 = 2;

            Node<int> testNode = new(testVal1);
            Node<int> testNode2 = testNode.Append(testVal2);

            // Act

            testNode.Clear();

            // Assert

            Assert.IsFalse(testNode.Next == testNode2);
          
        }

        [TestMethod]
        public void Node_Exists_NoDupe()
        {
            // Arrange

            int testVal1 = 10;
            int testVal2 = 20;
            int testVal3 = 30;

            Node<int> testNode = new(testVal1);
            Node<int> testNode2 = testNode.Append(testVal2);

            // Act

            bool existsResult = testNode.Exists(testVal3);

            // Assert

            Assert.IsFalse(existsResult);
        }

        [TestMethod]
        public void Node_Exists_WithDupe()
        {
            // Arrange

            int testVal1 = 10;
            int testVal2 = 20;
            int testVal3 = 30;

            Node<int> testNode = new(testVal1);
            Node<int> testNode2 = testNode.Append(testVal2);
            Node<int> testNode3 = testNode2.Append(testVal3);

            // Act

            // Assert

            Assert.IsTrue(testNode.Exists(testVal3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Dupelicate Value")]
        public void Node_AppendDupe_ThrowException()
        {
            // Arrange

            int testVal1 = 1;
            int testVal2 = 2;

            // Act

            Node<int> testNode = new(testVal1);
            Node<int> testNode2 = testNode.Append(testVal2);
            Node<int> testNode3 = testNode2.Append(testVal2);
        }
    }
}