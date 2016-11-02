using Maxxor.PCL.MxResults;
using Maxxor.PCL.MxResults.Interfaces;
using Maxxor.PCL.Tests.Builders.Base;

namespace Maxxor.PCL.Tests.Builders.MxResultBuilders
{

    public class MxResultOfTypeBuilder<T> : BaseMxBuilder
    {
        private T _value;
        private bool _isSuccess;
        private MxError _error;

        public MxResultOfTypeBuilder()
        {
            _error = MxError.Create(this, MxErrorCondition.Unspecified);
        }

        public MxResult<T> Create()
        {
            return new MxResult<T>(_value, _isSuccess, _error);
        }

        public MxResultOfTypeBuilder<T> With_IsSuccess(bool isSuccess)
        {
            _isSuccess = isSuccess;
            return this;
        }

        public MxResultOfTypeBuilder<T> With_Error(MxError error)
        {
            _isSuccess = false;
            _error = error;
            return this;
        }

        public MxResultOfTypeBuilder<T> With_Value(T value)
        {
            _isSuccess = true;
            _value = value;
            return this;
        }

        public MxResultOfTypeBuilder<T> With_Error_ClassName(string className)
        {
            _isSuccess = false;
            _error.ClassName = className;
            return this;
        }

        public MxResultOfTypeBuilder<T> With_Error_Type(IMxErrorCondition errorCondition)
        {
            _isSuccess = false;
            _error.ErrorCondition = errorCondition;
            return this;
        }

        public MxResultOfTypeBuilder<T> With_Sender(object sender)
        {
            _error.ClassName = sender.GetType().Name;
            return this;
        }
    }
    
}