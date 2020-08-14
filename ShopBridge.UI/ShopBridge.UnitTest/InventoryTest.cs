using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ShopBridge.Repository.Entities;
using ShopBridge.Repository.Interfaces;
using ShopBridge.Service.Interfaces;
using ShopBridge.Service.Service;

namespace ShopBridge.UnitTest
{
    [TestClass]
    public class InventoryTest
    {
        #region Global Variables
        private IInventoryService _inventoryServiceMock;
        private IInventoryRepository _inventoryRepositoryMock;
        #endregion

        #region Constructor
        public InventoryTest()
        {
            _inventoryServiceMock = Substitute.For<IInventoryService>();
            _inventoryRepositoryMock = Substitute.For<IInventoryRepository>();
        }
        #endregion

        [TestMethod]
        public void InvokesInventoryRepositoryGetByIdAsync_Return_Not_Null_Result()
        {
            //Arrange
            _inventoryRepositoryMock.GetByIdAsync(Arg.Any<int>())
                .Returns(Task.FromResult<Inventory>(new Inventory()));

            var inventoryServiceMock = new InventoryService(_inventoryRepositoryMock);

            //Act
            var result = inventoryServiceMock.GetByIdAsync(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]        
        public void InvokesInventoryRepositoryGetByIdAsync_Return_Null_Result()
        {
            //Arrange
            _inventoryRepositoryMock.GetByIdAsync(Arg.Any<int>())
                .Returns(Task.FromResult<Inventory>(null));

            var inventoryServiceMock = new InventoryService(_inventoryRepositoryMock);

            //Act
            var result = inventoryServiceMock.GetByIdAsync(-1);

            //Assert
            Assert.IsNull(result.Result);
        }

        [TestCleanup]        
        public void CleanUp()
        {
            _inventoryServiceMock = null;
            _inventoryRepositoryMock = null;
        }
    }
}
