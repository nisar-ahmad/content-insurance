using Insurance.Models.Content;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Data.EFCore.Repositories
{
    /// <summary>
    /// Category Repository
    /// </summary>
    public class CategoryRepository : RepositoryBase<Category, InsuranceDBContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CategoryRepository(InsuranceDBContext context) : base(context)
        {           
        }

        /// <summary>
        /// Overriden to return sorted categories
        /// </summary>
        /// <returns></returns>
        public async override Task<List<Category>> GetAll()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
