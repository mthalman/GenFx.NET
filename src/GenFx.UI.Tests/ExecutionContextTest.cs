using Moq;
using System;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionContext"/> class.
    /// </summary>
    public class ExecutionContextTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void ExecutionContext_Ctor()
        {
            GeneticAlgorithm algorithm = Mock.Of<GeneticAlgorithm>();
            ExecutionContext context = new ExecutionContext(algorithm);

            Assert.Same(algorithm, context.GeneticAlgorithm);
            Assert.Null(context.AlgorithmException);
            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionContext.AlgorithmException"/> property works correctly.
        /// </summary>
        [Fact]
        public void ExecutionContext_AlgorithmException()
        {
            GeneticAlgorithm algorithm = Mock.Of<GeneticAlgorithm>();
            ExecutionContext context = new ExecutionContext(algorithm);

            PropertyChangedHelper.VerifyPropertyChangedEvent(
                context, nameof(ExecutionContext.AlgorithmException), new ArgumentException(),
                v => context.AlgorithmException = v, () => context.AlgorithmException);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionContext.ExecutionState"/> property works correctly.
        /// </summary>
        [Fact]
        public void ExecutionContext_ExecutionState()
        {
            GeneticAlgorithm algorithm = Mock.Of<GeneticAlgorithm>();
            ExecutionContext context = new ExecutionContext(algorithm);

            PropertyChangedHelper.VerifyPropertyChangedEvent(
                context, nameof(ExecutionContext.ExecutionState), ExecutionState.Running,
                v => context.ExecutionState = v, () => context.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionContext.ExecutionState"/> resets the <see cref="ExecutionContext.AlgorithmException"/> property
        /// when set to idle.
        /// </summary>
        [Fact]
        public void ExecutionContext_ExecutionState_ResetException()
        {
            GeneticAlgorithm algorithm = Mock.Of<GeneticAlgorithm>();
            ExecutionContext context = new ExecutionContext(algorithm);

            InvalidOperationException exception = new InvalidOperationException();
            context.AlgorithmException = exception;

            context.ExecutionState = ExecutionState.Running;
            Assert.Same(exception, context.AlgorithmException);

            context.ExecutionState = ExecutionState.Idle;
            Assert.Null(context.AlgorithmException);
        }
    }
}
