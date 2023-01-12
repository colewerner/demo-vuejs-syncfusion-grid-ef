using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Database;
using Northwind.Database.Models;
using Syncfusion.EJ2.Base;
using VueSyncFusionWebApi.Model;

namespace VueSyncFusionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly NorthwindDbContext northwindDbContext;

        #region Constructors

        public ProductController(NorthwindDbContext northwindDbContext)
        {
            this.northwindDbContext = northwindDbContext;
        }

        #endregion

        #region Instance Methods

        [HttpPost]
        [Route("GetProducts")]
        public async Task<JsonResult> GetProducts([FromBody] DataManagerRequest dm)
        {
            try
            {
                var productQueryable = northwindDbContext.Products.Select(x => new ProductModel
                {
                    CategoryId = x.Category.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    ProductName = x.ProductName,
                    SupplierName = x.Supplier.CompanyName,
                    QuantityPerUnit = x.QuantityPerUnit,
                    Discontinued = x.Discontinued,
                    ProductId = x.ProductId,
                    ReorderLevel = x.ReorderLevel,
                    SupplierId = x.Supplier.SupplierId,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    UnitsOnOrder = x.UnitsOnOrder
                });

                var operation = new QueryableOperation();


                if (dm.Search != null
                    && dm.Search.Count > 0)
                    productQueryable = operation.PerformSearching(dataSource: productQueryable,
                        searchFilter: dm.Search); //Search

                if (dm.Sorted != null
                    && dm.Sorted.Count > 0) //Sorting
                    productQueryable = operation.PerformSorting(dataSource: productQueryable,
                        sortedColumns: dm.Sorted);

                if (dm.Where != null
                    && dm.Where.Count > 0) //Filtering
                    productQueryable = operation.PerformFiltering(dataSource: productQueryable,
                        whereFilter: dm.Where,
                        condition: dm.Where[index: 0]
                            .Operator);

                var count = await productQueryable.CountAsync();

                if (dm.Skip != 0)
                    productQueryable = operation.PerformSkip(dataSource: productQueryable,
                        skip: dm.Skip); //Paging

                if (dm.Take != 0)
                    productQueryable = operation.PerformTake(dataSource: productQueryable,
                        take: dm.Take);

                var productHistory = await productQueryable.ToListAsync();

                return dm.RequiresCounts
                    ? new JsonResult(new
                    {
                        result = productHistory,
                        count
                    })
                    : new JsonResult(value: productHistory);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return dm.RequiresCounts
                    ? new JsonResult(new
                    {
                        result = new List<Product>(),
                        count = 0
                    })
                    : new JsonResult(new List<Product>());
            }
        }

        [HttpPost]
        [Route(template: "AddEditDeleteProduct")]
        public async Task<IActionResult> AddEditDeleteProduct([FromBody] CRUDModel<ProductModel> value)
        {
            try
            {
                Product product = null;

                var productQueryable = northwindDbContext.Products.AsQueryable();

                if (value.Action == "update")
                {
                    product = await productQueryable.FirstOrDefaultAsync(x => x.ProductId == value.Value.ProductId);
                }
                else if (value.Action == "remove")
                {
                    product = await productQueryable.FirstOrDefaultAsync(x =>
                        x.ProductId == Convert.ToInt32(value.Key));
                }

                if (product == null && (value.Action == "update" || value.Action == "remove"))
                {
                    return BadRequest($"Could not find product with id {value.Value.ProductId}");
                }

                if (value.Action == "update")
                {
                    product.CategoryId = value.Value.CategoryId;
                    product.Discontinued = value.Value.Discontinued;
                    product.SupplierId = value.Value.SupplierId;
                    product.ProductName = value.Value.ProductName;
                    product.ReorderLevel = value.Value.ReorderLevel;
                    product.UnitPrice = value.Value.UnitPrice;
                    product.QuantityPerUnit = value.Value.QuantityPerUnit;
                    product.UnitsInStock = value.Value.UnitsInStock;
                    product.UnitsOnOrder = value.Value.UnitsOnOrder;
                }
                else if (value.Action == "insert")
                {
                    //Insert record in database
                    Product newProduct = new Product()
                    {
                        CategoryId = value.Value.CategoryId,
                        Discontinued = value.Value.Discontinued,
                        SupplierId = value.Value.SupplierId,
                        ProductName = value.Value.ProductName,
                        ReorderLevel = value.Value.ReorderLevel,
                        UnitPrice = value.Value.UnitPrice,
                        QuantityPerUnit = value.Value.QuantityPerUnit,
                        UnitsInStock = value.Value.UnitsInStock,
                        UnitsOnOrder = value.Value.UnitsOnOrder
                    };
                }
                else if (value.Action == "remove")
                {
                    //Delete record in database
                    northwindDbContext.Products.Remove(product);
                }

                await northwindDbContext.SaveChangesAsync();

                return Json(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("GetSuppliers")]
        public async Task<JsonResult> GetSuppliers()
        {
            try
            {
                
                    var supplierQueryable = northwindDbContext.Suppliers.AsQueryable();

                    var companies = await supplierQueryable.Select(x => new
                        {
                            SupplierName = x.CompanyName,
                            SupplierId = x.SupplierId
                        })
                        .Distinct()
                        .ToListAsync();

                    return new JsonResult(companies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new JsonResult(new List<string>());
            }
        }

        [HttpPost]
        [Route("GetCategories")]
        public async Task<JsonResult> GetCategories()
        {
            try
            {

                var categoryQueryable = northwindDbContext.Categories.AsQueryable();

                var companies = await categoryQueryable.Select(x => new
                    {
                        CategoryName = x.CategoryName,
                        CategoryId = x.CategoryId
                    })
                    .Distinct()
                    .ToListAsync();

                return new JsonResult(companies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new JsonResult(new List<string>());
            }
        }

        #endregion
    }
}
