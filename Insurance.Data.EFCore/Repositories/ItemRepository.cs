using Insurance.Models.Content;

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
    }
}
