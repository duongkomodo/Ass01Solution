using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao {
    public class ProductDao {
        private static ProductDao instance;

        public static ProductDao Instance {
            get {
                if (ProductDao.instance == null) {
                    ProductDao.instance = new ProductDao();

                }
                return ProductDao.instance;
            }
            private set => instance = value;
        }

        private ProductDao() {
        }


        public int getMaxProductId() {

            try {
                int maxId = DataProvider.Instance.DB.Products.Max(x => x.ProductId);

                return maxId;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return -1;
            }


        }
        public bool AddProduct(Product newProduct) {

            try {

                DataProvider.Instance.DB.Products.Add(newProduct);
                DataProvider.Instance.SaveChange();


                return true;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }


        }

        public bool UpdateProduct(Product updateProduct) {

            try {

                var currentProduct = DataProvider.Instance.DB.Products
                    .Where(x => x.ProductId.Equals(updateProduct.ProductId)).FirstOrDefault();

                if (currentProduct != null) {
                    currentProduct = updateProduct;
                    DataProvider.Instance.DB.Products.Update(currentProduct);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        public bool RemoveProduct(Product removeProduct) {

            try {

                var currentProduct = DataProvider.Instance.DB.Products
                    .Where(x => x.ProductId.Equals(removeProduct.ProductId)).FirstOrDefault();

                if (currentProduct != null) {

                    OrderDetailDao.Instance.RemoveAll(currentProduct.ProductId,null);

                    DataProvider.Instance.DB.Products.Remove(removeProduct);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        public List<Product>? LoadAllProducts() {

            try {

                return DataProvider.Instance.DB.Products.ToList();

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return new List<Product>();
            }
        }
    }
}
