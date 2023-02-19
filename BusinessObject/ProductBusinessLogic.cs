using DataAccess.Dao;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {

    public interface IProductBusiness {
        List<Product> LoadAllProducts();

        bool AddProduct(Product Product);
        bool UpdateProduct(Product Product);

        bool RemoveProduct(Product Product);
    }

    public class ProductBusinessLogic :IProductBusiness {
        public bool AddProduct(Product Product) {
            Product.ProductId = ProductDao.Instance.getMaxProductId() + 1;
            return ProductDao.Instance.AddProduct(Product);
        }

        public List<Product> LoadAllProducts() {
            return ProductDao.Instance.LoadAllProducts();
        }

        public bool RemoveProduct(Product Product) {
            return ProductDao.Instance.RemoveProduct(Product);
        }

        public bool UpdateProduct(Product Product) {
            return ProductDao.Instance.UpdateProduct(Product);
        }
    }
}
