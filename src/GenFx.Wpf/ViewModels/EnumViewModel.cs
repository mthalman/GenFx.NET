using System;

namespace GenFx.Wpf.ViewModels
{
    internal class EnumViewModel
    {
        public EnumViewModel(Enum value, string displayName)
        {
            this.Value = value;
            this.DisplayName = displayName;
        }

        public Enum Value { get; }

        public string DisplayName { get; }
    }
}
