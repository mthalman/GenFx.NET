using GenFx.UI.Properties;
using System.Collections.Generic;

namespace GenFx.UI.ViewModels
{
    internal static class EnumsViewModel
    {
        public static EnumViewModel FitnessTypeScaled = new EnumViewModel(FitnessType.Scaled, Resources.FitnessType_Scaled);
        public static EnumViewModel FitnessTypeRaw = new EnumViewModel(FitnessType.Raw, Resources.FitnessType_Raw);

        public static EnumViewModel FitnessSortByEntity = new EnumViewModel(FitnessSortOption.Entity, Resources.FitnessSortOption_Entity);
        public static EnumViewModel FitnessSortByFitness = new EnumViewModel(FitnessSortOption.Fitness, Resources.FitnessSortOption_Fitness);

        private static IEnumerable<EnumViewModel> fitnessTypes = new EnumViewModel[]
        {
            FitnessTypeScaled,
            FitnessTypeRaw
        };

        private static IEnumerable<EnumViewModel> fitnessSortOptions = new EnumViewModel[]
        {
            FitnessSortByEntity,
            FitnessSortByFitness
        };

        public static IEnumerable<EnumViewModel> FitnessTypes
        {
            get { return EnumsViewModel.fitnessTypes; }
        }

        public static IEnumerable<EnumViewModel> FitnessSortOptions
        {
            get { return EnumsViewModel.fitnessSortOptions; }
        }
    }
}
