using GenFx.Wpf.ViewModels;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumViewModel"/> class.
    /// </summary>
    public class EnumViewModelTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void EnumViewModel_Ctor()
        {
            EnumViewModel viewModel = new EnumViewModel(FitnessSortOption.Entity, "Test");
            Assert.Equal(FitnessSortOption.Entity, viewModel.Value);
            Assert.Equal("Test", viewModel.DisplayName);
        }
    }
}
