using System;
using System.ComponentModel;
using Xunit;

namespace TestCommon.Helpers
{
    /// <summary>
    /// Contains helper methods related to handling property change notifications.
    /// </summary>
    public static class PropertyChangedHelper
    {
        /// <summary>
        /// Verifies a property change notification is raised when setting a property and
        /// that the property was set correctly.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="obj">Object containing the property to verify.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="newValue">Value to set the property to.</param>
        /// <param name="setProperty">Delegate which sets the property.</param>
        /// <param name="getProperty">Delegate which gets the property.</param>
        public static void VerifyPropertyChangedEvent<T>(INotifyPropertyChanged obj, string propertyName, T newValue, Action<T> setProperty, Func<T> getProperty)
        {
            bool propertyChangedEventRaised = false;
            obj.PropertyChanged += (sender, e) =>
            {
                propertyChangedEventRaised = true;
                Assert.Equal(propertyName, e.PropertyName);
            };

            setProperty(newValue);

            Assert.True(propertyChangedEventRaised);

            if (newValue.GetType().IsValueType)
            {
                Assert.Equal(newValue, getProperty());
            }
            else
            {
                Assert.Equal(newValue, getProperty());
            }
        }
    }
}
