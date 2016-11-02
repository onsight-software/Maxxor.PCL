using System;
using System.Diagnostics.CodeAnalysis;
using Ploeh.AutoFixture;

namespace Maxxor.PCL.Tests.Helpers
{

    [ExcludeFromCodeCoverage]
    public static class MxGenerator
    {
        private static Fixture _myFixture;

        static MxGenerator()
        {
            _myFixture = new Fixture(); 
        }

        public static long AnyLong => _myFixture.Create<long>();
        public static string AnyString => _myFixture.Create<string>();
        public static string AnyGuid => Guid.NewGuid().ToString();
    }
}