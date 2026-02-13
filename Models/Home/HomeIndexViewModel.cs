using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingStore.Repository;
using X.PagedList;
using System.Collections.Generic;
using X.PagedList.Extensions;

namespace OnlineShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        private readonly SafainDbContext _context; // Private field for the context
        public IPagedList<TblProduct> ListOfProducts { get; set; }

        // Constructor now accepts SafainDbContext as a parameter
        public HomeIndexViewModel(SafainDbContext context)
        {
            _context = context; // Assign context to the private field
        }

        // CreateModel for fetching products with search and pagination
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            // Create parameter for search query, using DBNull.Value for null values
            var searchParam = new SqlParameter("@search", search ?? (object)DBNull.Value);

            // Query the database using FromSqlRaw, passing the parameter for the stored procedure
            var products = _context.TblProducts
                                   .FromSqlRaw("EXEC dbo.GetBySearch @search", searchParam)
                                   .ToList()  // Only need to call .ToList() once
                                   .ToPagedList(page ?? 1, pageSize);  // Apply pagination

            // Return a new instance of HomeIndexViewModel with populated products
            return new HomeIndexViewModel(_context) { ListOfProducts = products };
        }
    }
}
