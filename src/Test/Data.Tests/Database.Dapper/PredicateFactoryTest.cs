using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Metadata;
using Data.Database.Dapper.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable ObjectCreationAsStatement

namespace Data.Tests.Database.Dapper
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PredicateFactoryTest
    {
        private List<IFilterMetadataProvider> _providers;
        private Mock<IEnumerable<IFilterMetadataProvider>> _providersMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _providers = new List<IFilterMetadataProvider>();
            _providersMock = new Mock<IEnumerable<IFilterMetadataProvider>>(MockBehavior.Strict);
            _providersMock.Setup(m => m.GetEnumerator()).Returns(() => _providers.GetEnumerator());
        }

        // Constructor
        #region ConstructorShouldNotAddProvidersWithoutMetadataToDictionary

        [TestMethod]
        public void ConstructorShouldNotAddProvidersWithoutMetadataToDictionary()
        {
            // Arrange
            var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock.SetupGet(p => p.Metadata).Returns(default(List<FilterMetadata>));

            _providers.Add(providerMock.Object);

            // Act
            new PredicateFactory(_providersMock.Object);

            // Assert
            _providersMock.Verify(m => m.GetEnumerator(), Times.Once);
            providerMock.VerifyGet(p => p.Metadata, Times.Once);
        }

        #endregion
        #region ConstructorShouldNotAddProvidersWithEmptyMetadataToDictionary

        [TestMethod]
        public void ConstructorShouldNotAddProvidersWithEmptyMetadataToDictionary()
        {
            // Arrange
            var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata>());

            _providers.Add(providerMock.Object);

            // Act
            new PredicateFactory(_providersMock.Object);

            // Assert
            _providersMock.Verify(m => m.GetEnumerator(), Times.Once);
            providerMock.VerifyGet(p => p.Metadata, Times.Exactly(2));
        }

        #endregion
        #region ConstructorShouldAddProvidersWithMetadataToDictionary

        [TestMethod]
        public void ConstructorShouldAddProvidersWithMetadataToDictionary()
        {
            // Arrange
            var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata> {new FilterMetadata()});
            providerMock.SetupGet(p => p.Type).Returns(typeof(TestFilter));

            _providers.Add(providerMock.Object);

            // Act
            new PredicateFactory(_providersMock.Object);

            // Assert
            _providersMock.Verify(m => m.GetEnumerator(), Times.Once);
            providerMock.VerifyGet(p => p.Type, Times.Exactly(2));
            providerMock.VerifyGet(p => p.Metadata, Times.Exactly(3));
        }

        #endregion
        #region ConstructorShouldExtendProvidersWhenInDictionary

        [TestMethod]
        public void ConstructorShouldExtendProvidersWhenInDictionary()
        {
            // Arrange
            var providerMock1 = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock1.SetupGet(p => p.Type).Returns(typeof(TestFilter));
            providerMock1.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata> { new FilterMetadata() });

            var providerMock2 = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock2.SetupGet(p => p.Type).Returns(typeof(TestFilter));
            providerMock2.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata> { new FilterMetadata() });

            _providers.Add(providerMock1.Object);
            _providers.Add(providerMock2.Object);

            // Act
            new PredicateFactory(_providersMock.Object);

            // Assert
            _providersMock.Verify(m => m.GetEnumerator(), Times.Once);
            providerMock2.VerifyGet(p => p.Type, Times.Exactly(2));
            providerMock2.VerifyGet(p => p.Metadata, Times.Exactly(3));
        }

        #endregion

        // GetPredicate
        #region GetPredicateShouldReturnNullIfFilterIsNull

        [TestMethod]
        public void GetPredicateShouldReturnNullIfFilterIsNull()
        {
            // Arrange
            var predicateFactory = new PredicateFactory(_providersMock.Object);

            // Act
            var predicate = predicateFactory.GetPredicate<TestFilter, TestData>(null);

            // Assert
            predicate.Should().BeNull();
        }

        #endregion
        #region GetPredicateShouldReturnNullIfNoMetadataIsFoundForFilterType

        [TestMethod]
        public void GetPredicateShouldReturnNullIfNoMetadataIsFoundForFilterType()
        {
            // Arrange
            //var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            //providerMock.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata> { new FilterMetadata() });
            //providerMock.SetupGet(p => p.Type).Returns(typeof(TestFilter));

            //_providers.Add(providerMock.Object);

            var predicateFactory = new PredicateFactory(_providersMock.Object);

            // Act
            var predicate = predicateFactory.GetPredicate<TestFilter, TestData>(new TestFilter());

            // Assert
            predicate.Should().BeNull();
        }

        #endregion
        #region GetPredicateShouldReturnNullIfNoPredicatesAreSetForFilter

        [TestMethod]
        public void GetPredicateShouldReturnNullIfNoPredicatesAreSetForFilter()
        {
            // Arrange
            var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata> { new FilterMetadata<TestFilter, TestData>() });
            providerMock.SetupGet(p => p.Type).Returns(typeof(TestFilter));

            _providers.Add(providerMock.Object);

            var predicateFactory = new PredicateFactory(_providersMock.Object);

            // Act
            var predicate = predicateFactory.GetPredicate<TestFilter, TestData>(new TestFilter());

            // Assert
            predicate.Should().BeNull();
        }

        #endregion
        #region GetPredicateShouldReturnPredicateGroupIfMetadataIsFoundForFilterType

        [TestMethod]
        public void GetPredicateShouldReturnPredicateGroupIfMetadataIsFoundForFilterType()
        {
            // Arrange
            var providerMock = new Mock<IFilterMetadataProvider>(MockBehavior.Strict);
            providerMock.SetupGet(p => p.Metadata).Returns(new List<FilterMetadata>
            {
                new FilterMetadata<TestFilter, TestData>
                {
                    FilterValue = filter => filter.Property,
                    FilterExpression = data => data.Property,
                    FilterType = Operator.Like
                }
            });
            providerMock.SetupGet(p => p.Type).Returns(typeof(TestFilter));

            _providers.Add(providerMock.Object);

            var predicateFactory = new PredicateFactory(_providersMock.Object);

            // Act
            var predicate = predicateFactory.GetPredicate<TestFilter, TestData>(new TestFilter
            {
                Property = "PropertyValue"
            });

            // Assert
            predicate.Should().NotBeNull();
            predicate.Should().BeOfType<PredicateGroup>();

            var predicateGroup = (PredicateGroup) predicate;
            predicateGroup.Predicates.Should().NotBeNull().And.HaveCount(1);
            predicateGroup.Predicates.First().Should().BeAssignableTo<IFieldPredicate>();

            var fieldPredicate = (IFieldPredicate) predicateGroup.Predicates.First();
            fieldPredicate.PropertyName.Should().Be(nameof(TestData.Property));
            fieldPredicate.Value.Should().Be("PropertyValue");
            fieldPredicate.Operator.Should().Be(Operator.Like);
        }

        #endregion

        // Private subclasses
        #region TestFilter

        private class TestFilter : DataFilter<TestFilter, TestData>
        {
            public string Property { get; set; }
        }

        #endregion
        #region Test

        private class TestData
        {
            public string Property { get; set; }
        }

        #endregion
    }
}
