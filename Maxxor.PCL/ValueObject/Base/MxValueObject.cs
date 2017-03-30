using System;
using System.Collections.Generic;
using System.Reflection;

namespace Maxxor.PCL.ValueObject.Base
{
    public abstract class MxValueObject<T> : IEquatable<T> where T : MxValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }
            var other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            var fields = GetFields();

            var startValue = 17;
            var multiplier = 59;

            var hashCode = startValue;

            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                unchecked
                {
                    if (value != null)
                        hashCode = hashCode * multiplier + value.GetHashCode();
                }
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            var t = GetType();

            if ((object) other == null)
            {
                return false;
            }
            
            var otherType = other.GetType();

            if (t != otherType)
                return false;

            var fields = GetFields();

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            var t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetRuntimeFields());

                t = t.GetTypeInfo().BaseType;
            }

            return fields;
        }

        public static bool operator ==(MxValueObject<T> x, MxValueObject<T> y)
        {
            if ((object)x == null && (object)y == null)
            {
                return true;
            }
            return x.Equals(y);
        }

        public static bool operator !=(MxValueObject<T> x, MxValueObject<T> y)
        {
            if ((object)x == null && (object)y == null)
            {
                return false;
            }
            return !(x == y);
        }
    }
}