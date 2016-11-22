using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.Base;

namespace Maxxor.PCL.Tests.Builders.MxResultBuilders
{
    [ExcludeFromCodeCoverage]
    public class MxErrorBuilder : BaseMxBuilder
    {
        private Exception _exception;
        private readonly Dictionary<string, string> _errorData = new Dictionary<string, string>();
        private string _class = "";
        private string _method = "";
        private MxErrorCondition _mxErrorCondition = MxErrorCondition.Unspecified;

        public MxError Create()
        {
            var error = MxError.Create(this, _mxErrorCondition, _exception);
            error.AdditionalData = _errorData;
            if (_class != "") error.ClassName = _class;
            if (_method != "") error.MethodName = _method;
            return error;
        }

        public MxErrorBuilder With_MxErrorCondition(MxErrorCondition errorCondition)
        {
            _mxErrorCondition = errorCondition;
            return this;
        }
        public MxErrorBuilder With_Exception(Exception exception)
        {
            _exception = exception;
            return this;
        }

        public MxErrorBuilder With_ErrorData(string key, string value)
        {
            _errorData.Add(key, value);
            return this;
        }

        public MxErrorBuilder With_Class(string myclassname)
        {
            _class = myclassname;
            return this;
        }

        public MxErrorBuilder With_Method(string mymethodname)
        {
            _method = mymethodname;
            return this;
        }

    }
}