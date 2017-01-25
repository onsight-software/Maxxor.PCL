using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Maxxor.PCL.Tests.Tests.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseUnitTest
    {
        [SetUp]
        public virtual void Start()
        {
            MyFixture = new Fixture();
        }

        public Fixture MyFixture;
        private static Random _generator = new Random();
        public float AnyFloat => MyFixture.Create<float>();
        public double AnyDouble => MyFixture.Create<double>();
        
    }
}