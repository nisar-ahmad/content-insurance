using System.Collections.Generic;

namespace Insurance.Models.Content
{
    /// <summary>
    /// Category of Renter's items
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Category Id (Primary Key)
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Cateogry Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of items in this category (navigation property)
        /// </summary>
        public IList<Item> Items { get; set; }
    }
}