using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao {
    public class OrderDetailDao {

        private static OrderDetailDao instance;

        public static OrderDetailDao Instance {
            get {
                if (OrderDetailDao.instance == null) {
                    OrderDetailDao.instance = new OrderDetailDao();

                }
                return OrderDetailDao.instance;
            }
            private set => instance = value;
        }

        private OrderDetailDao() {
        }

        public void RemoveAll(int? productId, int? orderId) {

            try {
                var listOrderDetail = DataProvider.Instance.DB.OrderDetails;

                if (productId != null) {
                    DataProvider.Instance.DB.OrderDetails
                        .RemoveRange(listOrderDetail.Where(x => x.ProductId.Equals(productId)));
                }

                if (orderId != null) {
                    DataProvider.Instance.DB.OrderDetails
                        .RemoveRange(listOrderDetail.Where(x => x.OrderId.Equals(orderId)));
                }
            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());

            }

        }
    }
}
