using Insurance.Models.Content;

namespace Insurance.Data.EFCore.Repositories
{
    /// <summary>
    /// Category Repository
    /// </summary>
    public class CategoryRepository : RepositoryBase<Item, InsuranceDBContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CategoryRepository(InsuranceDBContext context) : base(context)
        {
        }
    }
}
