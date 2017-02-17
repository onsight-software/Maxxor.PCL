using System;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.ValueObject
{
    public abstract class MxEnum : MxValueObject<MxEnum>
    {
        private readonly string _typeName;

        protected MxEnum(string typeName)
        {
            _typeName = typeName;
        }

        public override string ToString()
        {
            return _typeName;
        }
    }
}