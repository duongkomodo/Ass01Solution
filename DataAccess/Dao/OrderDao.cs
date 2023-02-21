using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao {
    public class OrderDao {
        private static OrderDao instance;

        public static OrderDao Instance {
            get {
                if (OrderDao.instance == null) {
                    OrderDao.instance = new OrderDao();

                }
                return OrderDao.instance;
            }
            private set => instance = value;
        }

        private OrderDao() {
        }

        public bool AddOrder(Order newOrder) {

            try {

                DataProvider.Instance.DB.Orders.Add(newOrder);
                DataProvider.Instance.SaveChange();


                return true;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }


        }

        public bool UpdateOrder(Order updateOrder) {

            try {

                var currentOrder = DataProvider.Instance.DB.Orders
                    .Where(x => x.OrderId.Equals(updateOrder.OrderId)).FirstOrDefault();

                if (currentOrder != null) {
                    currentOrder = updateOrder;
                    DataProvider.Instance.DB.Orders.Update(currentOrder);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        public bool RemoveOrder(Order removeOrder) {

            try {

                var currentOrder = DataProvider.Instance.DB.Orders
                    .Where(x => x.OrderId.Equals(removeOrder.OrderId)).FirstOrDefault();

                if (currentOrder != null) {

                    OrderDetailDao.Instance.RemoveAll(null,currentOrder.OrderId);

                    DataProvider.Instance.DB.Orders.Remove(removeOrder);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }
        public int getMaxOrderId() {

            try {
                int maxId = DataProvider.Instance.DB.Orders.Max(x => x.OrderId);

                return maxId;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return -1;
            }


        }
        public List<Order>? LoadAllOrders() {

            try {

                return DataProvider.Instance.DB.Orders.ToList();

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return new List<Order>();
            }
        }
    }
}
