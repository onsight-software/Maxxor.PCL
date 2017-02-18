namespace Maxxor.PCL.ValueObject.Base
{
    public abstract class MxEnum<T> : MxValueObject<MxEnum<T>>
    {
        protected readonly string Name;

        protected MxEnum(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}