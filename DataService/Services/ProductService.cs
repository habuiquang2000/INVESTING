using DataService.Caches;
using DataService.ExecuteQueries;
using DataService.Utils;
using BaseLib.Dtos.Product;
using BaseLib.Models;
using System.Data;
using BaseLib.Dtos;

namespace DataService.Services;

public class ProductService
{
    static readonly Dictionary<string, string> productMapper = new()
    {
        ["Id"] = "_id",
        ["Name"] = "name",
        ["Price"] = "price",
        ["Description"] = "description",
    };
    #region C
    public static async Task<ERequest> CreateOne(PrductCreateDto product)
    {
        try
        {
            //if (string.IsNullOrEmpty(customerModel.fullname)) throw new Exception("Tên khách mời không được bỏ trống!");

            //int id = int.Parse(customerDAO.InsertOne(customerModel));
            DataTable dt = await ProductQuery.InsertOneAsync(new ProductModel()
            {
                Id = Uuid.GenerateWithDateTime,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            });

            List<ProductModel> newProducts = new();
            foreach (DataRow row in dt.Rows)
            {
                ProductModel newProduct = row.ToModel<ProductModel>(productMapper);
                newProducts.Add(newProduct);
                ProductCache.ProductList.Add(newProduct);
            }

            return new ERequest()
            {
                Code = (long)ERequest.CODE.SUCCESS,
                Message = ERequest.CODE.SUCCESS.ToString(),
                Data = newProducts.First()
            };
        }
        catch (Exception ex)
        {
            return new ERequest()
            {
                Code = ((long)ERequest.CODE.ERROR),
                Message = ex.Message,
                Data = ERequest.RESPONSE_DATA_NULL
            };
        }
    }
    #endregion

    #region R
    public static async Task<ERequest> ReadMany(string search = "")
    {
        try
        {
            if (ProductCache.ProductList == null || ProductCache.ProductList.Count == 0)
            {
                List<ProductModel>? newList = (await ProductQuery
                    .GetAllAsync(""))
                    .ToList<ProductModel>(productMapper);
                //newList.AddRange(
                //    collection: from DataRow row in table.Rows
                //                let item = row.ToModel
                //                select item);
                ProductCache.ProductList = newList!;
            }

            List<ProductModel> productList = ProductCache
                .ProductList
                .Where(product => (product.Name ?? "")
                    .ToLower()
                    .Contains(search.ToLower())
                )
                .ToList();

            return new ERequest()
            {
                Code = (long)ERequest.CODE.SUCCESS,
                Message = ERequest.CODE.SUCCESS.ToString(),
                Data = productList
            };
        }
        catch (Exception ex)
        {
            return new ERequest()
            {
                Code = ((long)ERequest.CODE.ERROR),
                Message = ex.Message,
                Data = ERequest.RESPONSE_DATA_NULL
            };
        }
    }
    #endregion

    #region U
    #endregion

    #region D
    #endregion
}
