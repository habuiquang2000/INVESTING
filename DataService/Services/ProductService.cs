using DataService.Caches;
using DataService.ExecuteQueries;
using DataService.Utils;
using BaseLib.Dtos.Product;
using BaseLib.Models;
using System.Data;

namespace DataService.Services
{
    public class ProductService : BaseService
    {
        #region C
        public static Task<ProductModel> CreateOne(PrductCreateDto product)
        {
            //if (string.IsNullOrEmpty(customerModel.fullname)) throw new Exception("Tên khách mời không được bỏ trống!");

            //int id = int.Parse(customerDAO.InsertOne(customerModel));
            DataTable dt = ProductQuery.InsertOne(new ProductModel()
            {
                Id = Uuid.GenerateWithDateTime,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            });

            List<ProductModel> newProducts = new();
            foreach (DataRow row in dt.Rows)
            {
                ProductModel newProduct = new()
                {
                    Id = $"{row["_id"]}",
                    Name = $"{row["name"]}",
                    Price = double.Parse($"{row["price"]}"),
                    Description = $"{row["description"]}",
                };
                newProducts.Add(newProduct);
                ProductCache.ProductList.Add(newProduct);
            }

            return Task.FromResult<ProductModel>(newProducts.First());
        }
        #endregion

        #region R
        public static List<ProductModel> ReadMany(string search = "")
        {
            if(ProductCache.ProductList == null || ProductCache.ProductList.Count == 0)
            {
                DataTable table = ProductQuery.GetAll("");
                List<ProductModel> newList = new();
                newList.AddRange(
                    collection: from DataRow row in table.Rows
                                let item = new ProductModel()
                                {
                                    Id = $"{row["_id"]}",
                                    Name = $"{row["name"]}",
                                    Price = double.Parse($"{row["price"]}"),
                                    Description = $"{row["description"]}",
                                }
                                select item);
                ProductCache.ProductList = newList;
            }

            List<ProductModel> productList = ProductCache
                .ProductList
                .Where(product => (product.Name ?? "")
                    .ToLower()
                    .Contains(search.ToLower())
                )
                .ToList();

            return productList;
        }
        #endregion

        #region U
        #endregion

        #region D
        #endregion
    }
}
