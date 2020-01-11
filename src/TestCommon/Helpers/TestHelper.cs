using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestCommon.Helpers
{
    public static class TestHelper
    {
        public static T WaitForResult<T>(T expectedResult, Func<T> getResult)
        {
            var result = Policy
                .HandleResult<T>(result => !result.Equals(expectedResult))
                .WaitAndRetry(5, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .Execute(getResult);
            Assert.Equal(expectedResult, result);
            return result;
        }

        public static void WaitForPropertyChanged(INotifyPropertyChanged source, Tuple<string, object>[] propertyValues, Action execute)
        {
            bool isComplete = false;
            Queue<Tuple<string, object>> remainingProperties = new Queue<Tuple<string, object>>(propertyValues);
            void propertyChangedHandler(object sender, PropertyChangedEventArgs args)
            {
                Tuple<string, object> current = remainingProperties.Peek();
                if (args.PropertyName == current.Item1 &&
                    new PrivateObject(sender).GetProperty(args.PropertyName).Equals(current.Item2))
                {
                    remainingProperties.Dequeue();

                    if (!remainingProperties.Any())
                    {
                        isComplete = true;
                    }
                }
            };

            source.PropertyChanged += propertyChangedHandler;

            try
            {
                execute();

                WaitForResult(true, () => isComplete);
            }
            finally
            {
                source.PropertyChanged -= propertyChangedHandler;
            }
        }
    }
}
