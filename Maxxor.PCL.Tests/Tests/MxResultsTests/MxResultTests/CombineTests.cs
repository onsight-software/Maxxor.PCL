using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Maxxor.PCL.Exceptions;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class CombineTests : BaseUnitTest
    {

        [TestFixture]
        public class ComibineList : BaseUnitTest
        {
            [Test]
            public void SHOULD_return_Success_with_list_of_Values_for_List_of_successes()
            {
                //Arrange
                var list = new List<MxResult> { MxResult.Ok(), MxResult.Ok(), MxResult.Ok() };

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsSuccess);
                Assert.AreEqual(typeof(MxResult), sut.GetType());
            }

            [Test]
            public void SHOULD_return_Failure_with_updated_Error_for_list_containing_failure()
            {
                //Arrange
                var list = new List<MxResult>
                {
                    MxResult.Ok(),
                    MxResult.Fail(this, MxErrorCondition.Cancelled),
                    MxResult.Fail(this, MxErrorCondition.Crash)
                };

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Cancelled));
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(sut.Error.SourceError.MethodName, Is.EqualTo("SHOULD_return_Failure_with_updated_Error_for_list_containing_failure"));
            }

            [Test]
            public void SHOULD_return_Success_for_empty_list_with_empty_list()
            {
                //Arrange
                var list = new List<MxResult>();

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsSuccess);
                Assert.AreEqual(typeof(MxResult), sut.GetType());
            }

            [Test]
            public void IF_Result_is_mistakenly_included_as_first_parameter_instead_of_sender_SHOULD_throw_exception()
            {
                //Arrange
                var wrongSender = MxResult.Ok();

                //Act
                Exception expectedException = new Exception();
                try
                {
                    MxResult.Combine(wrongSender, new List<MxResult>());
                }
                catch (Exception e)
                {
                    expectedException = e;
                }

                //Assert
                Assert.That(expectedException, Is.TypeOf<MxInvalidSenderException>());
                Assert.That(expectedException.Message, Is.EqualTo("An instance of MxResult cannot be used as the sender"));
            }
        }

        [TestFixture]
        public class ComibineParams : BaseUnitTest
        {
            [Test]
            public void SHOULD_return_Success_with_list_of_Values_for_List_of_successes()
            {
                //Act
                var sut = MxResult.Combine(this, MxResult.Ok(), MxResult.Ok(), MxResult.Ok());

                //Assert
                Assert.That(sut.IsSuccess);
                Assert.AreEqual(typeof(MxResult), sut.GetType());
            }

            [Test]
            public void SHOULD_return_Failure_with_updated_Error_for_list_containing_failure()
            {
                //Act
                var sut = MxResult.Combine(this,
                    MxResult.Ok(),
                    MxResult.Fail(this, MxErrorCondition.Cancelled),
                    MxResult.Fail(this, MxErrorCondition.Crash));

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Cancelled));
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(sut.Error.SourceError.MethodName, Is.EqualTo("SHOULD_return_Failure_with_updated_Error_for_list_containing_failure"));
            }

            [Test]
            public void IF_Result_is_mistakenly_included_as_first_parameter_instead_of_sender_SHOULD_throw_exception()
            {
                //Arrange
                var wrongSender = MxResult.Ok();

                //Act
                Exception expectedException = new Exception();
                try
                {
                    MxResult.Combine(wrongSender, MxResult.Ok(), MxResult.Ok(1));
                }
                catch (Exception e)
                {
                    expectedException = e;
                }

                //Assert
                Assert.That(expectedException, Is.TypeOf<MxInvalidSenderException>());
                Assert.That(expectedException.Message, Is.EqualTo("An instance of MxResult cannot be used as the sender"));
            }

        }

        [TestFixture]
        public class ComibineListOfT : BaseUnitTest
        {
            [Test]
            public void SHOULD_return_Success_with_list_of_Values_for_List_of_successes()
            {
                //Arrange
                var list = new List<MxResult<int>> {MxResult.Ok(1), MxResult.Ok(2), MxResult.Ok(3)};

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsSuccess);
                Assert.AreEqual(typeof(MxResult<List<int>>), sut.GetType());
                Assert.AreEqual(typeof(List<int>), sut.Value.GetType());
                Assert.That(sut.Value.SequenceEqual(new[] {1, 2, 3}));
            }
            
            [Test]
            public void SHOULD_return_Failure_with_updated_Error_for_list_containing_failure()
            {
                //Arrange
                var list = new List<MxResult<int>>
                {
                    MxResult.Ok(1),
                    MxResult.Fail<int>(this, MxErrorCondition.Cancelled),
                    MxResult.Ok(2)
                };

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Cancelled));
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(sut.Error.SourceError.MethodName, Is.EqualTo("SHOULD_return_Failure_with_updated_Error_for_list_containing_failure"));
            }

            [Test]
            public void SHOULD_return_Success_for_empty_list_with_empty_list()
            {
                //Arrange
                var list = new List<MxResult<int>>();

                //Act
                var sut = MxResult.Combine(this, list);

                //Assert
                Assert.That(sut.IsSuccess);
                Assert.AreEqual(typeof(MxResult<List<int>>), sut.GetType());
                Assert.AreEqual(typeof(List<int>), sut.Value.GetType());
                Assert.That(sut.Value, Is.Empty);
            }

            [Test]
            public void IF_Result_is_mistakenly_included_as_first_parameter_instead_of_sender_SHOULD_throw_exception()
            {
                //Arrange
                var wrongSender = MxResult.Ok(1);

                //Act
                Exception expectedException = new Exception();
                try
                {
                    MxResult.Combine(wrongSender, new List<MxResult<int>>());
                }
                catch (Exception e)
                {
                    expectedException = e;
                }

                //Assert
                Assert.That(expectedException, Is.TypeOf<MxInvalidSenderException>());
                Assert.That(expectedException.Message, Is.EqualTo("An instance of MxResult cannot be used as the sender"));
            }

            [Test]
            public void IF_list_of_Results_is_mistakenly_included_as_only_SHOULD_throw_exception()
            {
                //Arrange
                //Act
                Exception expectedException = new Exception();
                try
                {
                    MxResult.Combine(new List<MxResult<int>>());
                }
                catch (Exception e)
                {
                    expectedException = e;
                }

                //Assert
                Assert.That(expectedException, Is.TypeOf<MxInvalidSenderException>());
                Assert.That(expectedException.Message, Is.EqualTo("An instance of MxResult cannot be used as the sender"));
            }
        }


    }
}