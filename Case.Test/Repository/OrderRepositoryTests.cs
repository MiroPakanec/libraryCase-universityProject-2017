using System;
using System.Threading.Tasks;
using Case.Core.Entity;
using Case.Core.Mapper;
using Case.Core.Repository;
using DataAccess.Queries.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Case.Test.Repository.Extensions.OrderRepository;
using Case.Test.TestUtils.Order;
using DataAccess.EntityMaps.Order;
using Moq;

namespace Case.Test.Repository
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private OrderRepository _sut;
        private Mock<IOrderAccess> _access;
        private Mock<IMapper<Order>> _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _access = new Mock<IOrderAccess>();
            _mapper = new Mock<IMapper<Order>>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
            _mapper = null;
            _access = null;
        }

        #region Insert


        [TestMethod]
        public async Task InsertOrder_ValidOrder_HappyPath()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(OrderTestUtils.GenerateValidOrder());

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Order cannot be invalid")]
        public async Task InsertOrder_InvalidOrder_Fails()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);
            
            //act
            await _sut.InsertAsync(OrderTestUtils.GenerateInvalidOrder());

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Order cannot be null")]
        public async Task InsertOrder_NullOrder_Fails()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsync(null);

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Never);
        }

        #endregion

        #region InsertWithId

        [TestMethod]
        public async Task InsertWithIdOrder_ValidOrder_HappyPath()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsyncWithIdReturn(OrderTestUtils.GenerateValidOrder());

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Order cannot be invalid")]
        public async Task InsertWithIdOrder_InvalidOrder_Fails()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsyncWithIdReturn(OrderTestUtils.GenerateInvalidOrder());

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Order cannot be null")]
        public async Task InsertWithIdOrder_NullOrder_Fails()
        {
            //arrange
            _mapper.WithMap(OrderMapTestUtils.GenerateValidOrderMap());
            _access.WithInsert(true);

            _sut = new OrderRepository(_access.Object, _mapper.Object);

            //act
            await _sut.InsertAsyncWithIdReturn(null);

            //assert
            _access.Verify(x => x.InsertWithIdReturn(It.IsAny<OrderMap>()), Times.Never);
        }

        #endregion
    }
}