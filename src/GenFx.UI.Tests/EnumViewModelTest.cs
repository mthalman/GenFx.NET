using GenFx.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumViewModel"/> class.
    /// </summary>
    [TestClass]
    public class EnumViewModelTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void EnumViewModel_Ctor()
        {
            EnumViewModel viewModel = new EnumViewModel(FitnessSortOption.Entity, "Test");
            Assert.AreEqual(FitnessSortOption.Entity, viewModel.Value);
            Assert.AreEqual("Test", viewModel.DisplayName);
        }
    }
}
