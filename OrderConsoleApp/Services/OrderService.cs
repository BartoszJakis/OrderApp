using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using OrderConsoleApp.Repostiory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace OrderConsoleApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrder(Order order)
        {


            await _orderRepository.AddOrder(order);
        }

        public async Task MoveToWarehouse(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            if (order.Payment == PaymentType.CashWhenDelivered && order.Price >= 2500)
            {              
                await _orderRepository.UpdateOrder(orderId, OrderStatus.ReturnedToClient);
                throw new InvalidOperationException($"Order {orderId} returned to client due to high cash payment.");
            }
            else
            {
                await _orderRepository.UpdateOrder(orderId, OrderStatus.InWarehouse);
            }
            
        }

        public async Task MoveToShipment(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            if (order.OrderStatus != OrderStatus.InWarehouse)
            {
                throw new InvalidOperationException("Order must be in warehouse before shipping.");
            }

            await _orderRepository.UpdateOrder(orderId, OrderStatus.InShipment);
            await Task.Delay(5000);
            await _orderRepository.UpdateOrder(orderId, OrderStatus.Closed);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetOrders();
        }


    }
}