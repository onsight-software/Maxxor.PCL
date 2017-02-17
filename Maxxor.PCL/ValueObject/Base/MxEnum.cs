namespace Maxxor.PCL.ValueObject.Base
{
    public abstract class MxEnum<T> : MxValueObject<MxEnum<T>>
    {
        protected readonly string TypeName;

        protected MxEnum(string typeName)
        {
            TypeName = typeName;
        }

        public override string ToString()
        {
            return TypeName;
        }
    }
}