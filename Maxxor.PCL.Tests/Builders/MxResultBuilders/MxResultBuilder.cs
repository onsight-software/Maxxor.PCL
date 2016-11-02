using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.Base;

namespace Maxxor.PCL.Tests.Builders.MxResultBuilders
{

    [ExcludeFromCodeCoverage]
    public class MxResultBuilder : BaseMxBuilder
    {
        private bool _isSuccess;
        private MxError _error;
        private object _sender;

        public MxResultBuilder()
        {
            _error = MxError.Create(this, MxErrorCondition.Unspecified);
            _sender = this;
        }

        public MxResult Create()
        {
            if (_isSuccess)
                return MxResult.Ok();
            return MxResult.Fail(_sender, _error.ErrorCondition);
        }

        public MxResultBuilder With_IsSuccess(bool isSuccess)
        {
            _isSuccess = isSuccess;
            return this;
        }

        public MxResultBuilder With_Error(MxError errorMessage)
        {
            _error = errorMessage;
            return this;
        }

        public MxResultBuilder With_Sender(object sender)
        {
            _sender = sender;
            return this;
        }

    }
    
}