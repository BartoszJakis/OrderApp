using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using OrderConsoleApp.Repostiory;
using System;
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

        public void CreateOrder(Order order)
        {
          

            _orderRepository.AddOrder(order);
        }

        public void MoveToWarehouse(Guid orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            if (order.Payment.PaymentType == PaymentType.CashWhenDelivered && order.Payment.Price >= 2500)
            {
                order.OrderStatus = OrderStatus.ReturnedToClient;
                throw new InvalidOperationException($"Order {orderId} returned to client due to high cash payment.");
            }
            else
            {
                order.OrderStatus = OrderStatus.InWarehouse;
            }
        }

        public  void MoveToShipment(Guid orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            if (order.OrderStatus != OrderStatus.InWarehouse)
            {
                throw new InvalidOperationException("Order must be in warehouse before shipping.");
            }

            order.OrderStatus = OrderStatus.InShipment;

            
            Task.Delay(5000).ContinueWith(t =>
            {
                order.OrderStatus = OrderStatus.Closed;
            });
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

     
    }
}