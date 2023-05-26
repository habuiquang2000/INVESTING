using BaseLib.Models;
using System.Data;

namespace DataService.Caches;

public class ProductCache
{
    private static List<ProductModel>? productList = null;
    public static List<ProductModel> ProductList {
        get
        {
            productList ??= new();
            return productList;
        }
        set
        {
            productList ??= new();
            productList = value;
        }
    }
}
