using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Metadata;
using Data.Database.Dapper.Models;
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

        #region ConstructorShouldNotAddProvidersWithoutMetadataToDictionary |

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
        #region ConstructorShouldNotAddProvidersWithEmptyMetadataToDictionary |

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
        #region ConstructorShouldAddProvidersWithMetadataToDictionary |

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
        #region ConstructorShouldExtendProvidersWhenInDictionary |

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

        // Private subclasses
        #region TestFilter |

        private class TestFilter : DataFilter<TestFilter, TestData>
        {
        }

        #endregion
        #region Test       |

        private class TestData
        {
        }

        #endregion
    }
}
