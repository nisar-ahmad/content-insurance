using Insurance.Models.Content;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Data.EFCore.Repositories
{
    /// <summary>
    /// Item Repository
    /// </summary>
    public class ItemRepository : RepositoryBase<Item, InsuranceDBContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ItemRepository(InsuranceDBContext context) : base(context)
        {
        }

        /// <summary>
        /// Returns all items with categories
        /// </summary>
        /// <returns></returns>
        public override async Task<List<Item>> GetAll()
        {
            return await _context.Items.Include(item => item.Category).OrderBy(item => item.Category.Name).ToListAsync();
        }

        /// <summary>
        /// Override Add to attach navigation property of Category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<Item> Add(Item item)
        {
            await base.Add(item);
            await _context.Entry(item).Reference(i => i.Category).LoadAsync();
            return item;
        }
    }
}
