using GenFx.UI.Converters;
using GenFx.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnumViewModelConverter"/> class.
    /// </summary>
    [TestClass]
    public class EnumViewModelConverterTest
    {
        /// <summary>
        /// Tests that the <see cref="EnumViewModelConverter.Convert"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void EnumViewModelConverter_Convert()
        {
            EnumViewModelConverter converter = new EnumViewModelConverter();

            List<Enum> enumValues = new List<Enum>();
            enumValues.AddRange(Enum.GetValues(typeof(FitnessType)).Cast<Enum>());
            enumValues.AddRange(Enum.GetValues(typeof(FitnessSortOption)).Cast<Enum>());

            object result;
            foreach (Enum val in enumValues)
            {
                result = converter.Convert(val, null, null, null);
                EnumViewModel viewModel = (EnumViewModel)result;
                Assert.AreEqual(val, viewModel.Value);
                Assert.IsTrue(!(String.IsNullOrEmpty(viewModel.DisplayName)));
            }
            
            result = converter.Convert(null, null, null, null);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests that the <see cref="EnumViewModelConverter.ConvertBack"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void EnumViewModelConverter_ConvertBack()
        {
            EnumViewModelConverter converter = new EnumViewModelConverter();
            object result;

            EnumViewModel viewModel = new EnumViewModel(FitnessType.Raw, "Test");
            result = converter.ConvertBack(viewModel, null, null, null);
            Assert.AreEqual(viewModel.Value, result);

            result = converter.ConvertBack(null, null, null, null);
            Assert.IsNull(result);
        }
    }
}
