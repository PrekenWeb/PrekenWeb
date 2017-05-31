using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Business.Services;
using Data.Database.Dapper.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ServiceTest
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IGateway<TestDataObject, TestDataFilter>> _gatewayMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _gatewayMock = new Mock<IGateway<TestDataObject, TestDataFilter>>(MockBehavior.Strict);
        }

        #region GetShouldMapFilterAndReturnMappedData

        [TestMethod]
        public async Task GetShouldMapFilterAndReturnMappedData()
        {
            // Arrange
            var businessFilter = new TestBusinessFilter();
            var dataFilter = new TestDataFilter();
            IEnumerable<TestDataObject> dataObjects = new List<TestDataObject>();
            IEnumerable<TestObject> businessObjects = new List<TestObject>();

            Expression<Func<IMapper, TestDataFilter>> mapFilterExpression = m => m.Map<TestBusinessFilter, TestDataFilter>(It.Is<TestBusinessFilter>(f => Equals(f, businessFilter)));
            Expression<Func<IMapper, IEnumerable<TestObject>>> mapDataToBusinessExpression = m => m.Map<IEnumerable<TestDataObject>, IEnumerable<TestObject>>(It.Is<IEnumerable<TestDataObject>>(d=>Equals(d, dataObjects)));
            Expression<Func<IGateway<TestDataObject, TestDataFilter>, Task<IEnumerable<TestDataObject>>>> getDataExpression = g => g.Get(It.Is<TestDataFilter>(f=>Equals(f, dataFilter)));

            _mapperMock.Setup(mapFilterExpression).Returns(dataFilter);
            _mapperMock.Setup(mapDataToBusinessExpression).Returns(businessObjects);
            _gatewayMock.Setup(getDataExpression).Returns(Task.FromResult(dataObjects));

            var service = new Service<TestObject, TestBusinessFilter, TestDataObject, TestDataFilter>(_mapperMock.Object, _gatewayMock.Object);

            // Act
            var actual = await service.Get(businessFilter);

            // Assert
            Assert.AreEqual(businessObjects, actual);

            _mapperMock.Verify(mapFilterExpression, Times.Once);
            _mapperMock.Verify(mapDataToBusinessExpression, Times.Once);
            _gatewayMock.Verify(getDataExpression, Times.Once);

        }

        #endregion
        #region GetSingleShouldReturnMappedData

        [TestMethod]
        public async Task GetSingleShouldReturnMappedData()
        {
            // Arrange
            const int id = 1;
            var dataObject = new TestDataObject();
            var businessObject = new TestObject();

            Expression<Func<IMapper, TestObject>> mapDataToBusinessExpression = m => m.Map<TestDataObject, TestObject>(It.Is<TestDataObject>(d => Equals(d, dataObject)));
            Expression<Func<IGateway<TestDataObject, TestDataFilter>, Task<TestDataObject>>> getSingleDataExpression = g => g.GetSingle(It.Is<int>(i => Equals(i, id)));

            _mapperMock.Setup(mapDataToBusinessExpression).Returns(businessObject);
            _gatewayMock.Setup(getSingleDataExpression).Returns(Task.FromResult(dataObject));

            var service = new Service<TestObject, TestBusinessFilter, TestDataObject, TestDataFilter>(_mapperMock.Object, _gatewayMock.Object);

            // Act
            var actual = await service.GetSingle(id);

            // Assert
            Assert.AreEqual(businessObject, actual);

            _mapperMock.Verify(mapDataToBusinessExpression, Times.Once);
            _gatewayMock.Verify(getSingleDataExpression, Times.Once);

        }

        #endregion
        #region AddShouldAddMappedBusinessObjectAndReturnId

        [TestMethod]
        public async Task AddShouldAddMappedBusinessObjectAndReturnId()
        {
            // Arrange
            var businessObject = new TestObject();
            var dataObject = new TestDataObject();
            const int expectedId = 1;

            Expression<Func<IMapper, TestDataObject>> mapBusinessToDataExpression = m => m.Map<TestObject, TestDataObject>(It.Is<TestObject>(obj => Equals(obj, businessObject)));
            Expression<Func<IGateway<TestDataObject, TestDataFilter>, Task<int>>> addDataExpression = g => g.Add(It.Is<TestDataObject>(obj => Equals(obj, dataObject)));

            _mapperMock.Setup(mapBusinessToDataExpression).Returns(dataObject);
            _gatewayMock.Setup(addDataExpression).Returns(Task.FromResult(expectedId));

            var service = new Service<TestObject, TestBusinessFilter, TestDataObject, TestDataFilter>(_mapperMock.Object, _gatewayMock.Object);

            // Act
            var actualId = await service.Add(businessObject);

            // Assert
            Assert.AreEqual(expectedId, actualId);

            _mapperMock.Verify(mapBusinessToDataExpression, Times.Once);
            _gatewayMock.Verify(addDataExpression, Times.Once);

        }

        #endregion
        #region UpdateShouldUpdateMappedBusinessObjectAndReturnSuccess

        [TestMethod]
        public async Task UpdateShouldUpdateMappedBusinessObjectAndReturnSuccess()
        {
            // Arrange
            var businessObject = new TestObject();
            var dataObject = new TestDataObject();
            const bool expectedResult = true;

            Expression<Func<IMapper, TestDataObject>> mapBusinessToDataExpression = m => m.Map<TestObject, TestDataObject>(It.Is<TestObject>(obj => Equals(obj, businessObject)));
            Expression<Func<IGateway<TestDataObject, TestDataFilter>, Task<bool>>> updateDataExpression = g => g.Update(It.Is<TestDataObject>(obj => Equals(obj, dataObject)));

            _mapperMock.Setup(mapBusinessToDataExpression).Returns(dataObject);
            _gatewayMock.Setup(updateDataExpression).Returns(Task.FromResult(expectedResult));

            var service = new Service<TestObject, TestBusinessFilter, TestDataObject, TestDataFilter>(_mapperMock.Object, _gatewayMock.Object);

            // Act
            var actualResult = await service.Update(businessObject);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

            _mapperMock.Verify(mapBusinessToDataExpression, Times.Once);
            _gatewayMock.Verify(updateDataExpression, Times.Once);

        }

        #endregion
        #region DeleteShouldDeleteMappedBusinessObjectAndReturnSuccess

        [TestMethod]
        public async Task DeleteShouldDeleteMappedBusinessObjectAndReturnSuccess()
        {
            // Arrange
            var businessObject = new TestObject();
            var dataObject = new TestDataObject();
            const bool expectedResult = true;

            Expression<Func<IMapper, TestDataObject>> mapBusinessToDataExpression = m => m.Map<TestObject, TestDataObject>(It.Is<TestObject>(obj => Equals(obj, businessObject)));
            Expression<Func<IGateway<TestDataObject, TestDataFilter>, Task<bool>>> deleteDataExpression = g => g.Delete(It.Is<TestDataObject>(obj => Equals(obj, dataObject)));

            _mapperMock.Setup(mapBusinessToDataExpression).Returns(dataObject);
            _gatewayMock.Setup(deleteDataExpression).Returns(Task.FromResult(expectedResult));

            var service = new Service<TestObject, TestBusinessFilter, TestDataObject, TestDataFilter>(_mapperMock.Object, _gatewayMock.Object);

            // Act
            var actualResult = await service.Delete(businessObject);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

            _mapperMock.Verify(mapBusinessToDataExpression, Times.Once);
            _gatewayMock.Verify(deleteDataExpression, Times.Once);

        }

        #endregion
    }

    public class TestObject { }
    public class TestBusinessFilter { }
    public class TestDataObject { }
    public class TestDataFilter { }
}
