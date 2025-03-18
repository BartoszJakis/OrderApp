using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;

using OrderConsoleApp.Repostiory;
using OrderConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderApp.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private OrderService _orderService;

        [TestInitialize]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldAddOrder()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductName = "Kostka Rubika",
                Payment = PaymentType.Card,
                Price = 100,
                Client = ClientType.Person,
                Address = "Testowa 10",
                OrderStatus = OrderStatus.New
            };

            await _orderService.CreateOrder(order);

            _orderRepositoryMock.Verify(repo => repo.AddOrder(order), Times.Once);
        }

        [TestMethod]
        public async Task MoveToWarehouseAsync_ShouldUpdateOrderStatus()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                ProductName = "Kostka Rubika",
                Payment = PaymentType.Card,
                Price=100,
                Client = ClientType.Person,
                Address = "Testowa 10",
                OrderStatus = OrderStatus.New
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);

            await _orderService.MoveToWarehouse(orderId);

            _orderRepositoryMock.Verify(repo => repo.UpdateOrder(orderId, OrderStatus.InWarehouse), Times.Once);
        }

        [TestMethod]
        public async Task MoveToShipmentAsync_ShouldUpdateOrderStatus()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                ProductName = "Kostka Rubika",
                Payment = PaymentType.Card,
                Price = 200,
                Client = ClientType.Person,
                Address = "Testowa 10",
                OrderStatus = OrderStatus.InWarehouse
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);

            await _orderService.MoveToShipment(orderId);

            _orderRepositoryMock.Verify(repo => repo.UpdateOrder(orderId, OrderStatus.InShipment), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.UpdateOrder(orderId, OrderStatus.Closed), Times.Once);
        }


        [TestMethod]
        public async Task MoveToWarehouseAsync_ShouldReturnToClient_WhenCashPaymentExceedsLimit()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                ProductName = "Drogi Produkt",
                Payment = PaymentType.CashWhenDelivered,
                Price = 3000,
                Client = ClientType.Company,
                Address = "Ekskluzywna 1",
                OrderStatus = OrderStatus.New
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _orderService.MoveToWarehouse(orderId));

            _orderRepositoryMock.Verify(repo => repo.UpdateOrder(orderId, OrderStatus.ReturnedToClient), Times.Once);
        }

    }
}
