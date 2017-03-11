using GenFx.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumsViewModel"/> class.
    /// </summary>
    [TestClass]
    public class EnumsViewModelTest
    {
        /// <summary>
        /// Tests that the <see cref="EnumsViewModel.FitnessTypes"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void EnumsViewModel_FitnessTypes()
        {
            CollectionAssert.AreEqual(new EnumViewModel[] {
                EnumsViewModel.FitnessTypeScaled,
                EnumsViewModel.FitnessTypeRaw,
            }, EnumsViewModel.FitnessTypes.ToList());
        }

        /// <summary>
        /// Tests that the <see cref="EnumsViewModel.FitnessSortOptions"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void EnumsViewModel_FitnessSortOptions()
        {
            CollectionAssert.AreEqual(new EnumViewModel[] {
                EnumsViewModel.FitnessSortByEntity,
                EnumsViewModel.FitnessSortByFitness,
            }, EnumsViewModel.FitnessSortOptions.ToList());
        }

        /// <summary>
        /// Tests that the static properties for individual <see cref="EnumViewModel"/> objects work correctly.
        /// </summary>
        [TestMethod]
        public void EnumsViewModel_IndividualProperties()
        {
            Assert.AreEqual(FitnessSortOption.Entity, EnumsViewModel.FitnessSortByEntity.Value);
            Assert.AreEqual(FitnessSortOption.Fitness, EnumsViewModel.FitnessSortByFitness.Value);
            Assert.AreEqual(FitnessType.Raw, EnumsViewModel.FitnessTypeRaw.Value);
            Assert.AreEqual(FitnessType.Scaled, EnumsViewModel.FitnessTypeScaled.Value);
        }
    }
}
