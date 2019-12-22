using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestCommon
{
    public class PrivateObject
    {
        public PrivateObject(object instance, PrivateType privateType = null)
        {
            this.Instance = instance;

            this.PrivateType = privateType ?? new PrivateType(instance.GetType());
        }

        public object Instance { get; }
        public PrivateType PrivateType { get; }

        public object Invoke(string methodName, params object[] args)
        {
            try
            {
                return this.PrivateType.Type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Invoke(this.Instance, args);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public object GetField(string fieldName)
        {
            return this.PrivateType.Type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .GetValue(this.Instance);
        }

        public object GetProperty(string propertyName)
        {
            return this.PrivateType.Type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .GetValue(this.Instance);
        }

        public void SetProperty(string propertyName, object value)
        {
            this.PrivateType.Type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(this.Instance, value);
        }

        public void SetField(string fieldName, object value)
        {
            this.PrivateType.Type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(this.Instance, value);
        }
    }
}
