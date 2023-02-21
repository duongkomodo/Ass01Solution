using AutoMapper.Execution;
using DataAccess.Dao;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {

    public interface IOrderBusiness {
        List<Order> LoadAllOrders();

        bool AddOrder(Order Order);
        bool UpdateOrder(Order Order);

        bool RemoveOrder(Order Order);

    }
    public class OrderBusinessLogic :IOrderBusiness {
        public bool AddOrder(Order order) {
            order.OrderId = OrderDao.Instance.getMaxOrderId() + 1;

            return OrderDao.Instance.AddOrder(order);
        }

        public List<Order> LoadAllOrders() {
          return OrderDao.Instance.LoadAllOrders();
        }

        public bool RemoveOrder(Order order) {
            return OrderDao.Instance.RemoveOrder(order);
        }

        public bool UpdateOrder(Order order) {
            return OrderDao.Instance.UpdateOrder(order);
        }
    }
}
