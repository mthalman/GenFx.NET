using GenFx.Wpf.ViewModels;
using System.Linq;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumsViewModel"/> class.
    /// </summary>
    public class EnumsViewModelTest
    {
        /// <summary>
        /// Tests that the <see cref="EnumsViewModel.FitnessTypes"/> property works correctly.
        /// </summary>
        [Fact]
        public void EnumsViewModel_FitnessTypes()
        {
            Assert.Equal(new EnumViewModel[] {
                EnumsViewModel.FitnessTypeScaled,
                EnumsViewModel.FitnessTypeRaw,
            }, EnumsViewModel.FitnessTypes.ToList());
        }

        /// <summary>
        /// Tests that the <see cref="EnumsViewModel.FitnessSortOptions"/> property works correctly.
        /// </summary>
        [Fact]
        public void EnumsViewModel_FitnessSortOptions()
        {
            Assert.Equal(new EnumViewModel[] {
                EnumsViewModel.FitnessSortByEntity,
                EnumsViewModel.FitnessSortByFitness,
            }, EnumsViewModel.FitnessSortOptions.ToList());
        }

        /// <summary>
        /// Tests that the static properties for individual <see cref="EnumViewModel"/> objects work correctly.
        /// </summary>
        [Fact]
        public void EnumsViewModel_IndividualProperties()
        {
            Assert.Equal(FitnessSortOption.Entity, EnumsViewModel.FitnessSortByEntity.Value);
            Assert.Equal(FitnessSortOption.Fitness, EnumsViewModel.FitnessSortByFitness.Value);
            Assert.Equal(FitnessType.Raw, EnumsViewModel.FitnessTypeRaw.Value);
            Assert.Equal(FitnessType.Scaled, EnumsViewModel.FitnessTypeScaled.Value);
        }
    }
}
