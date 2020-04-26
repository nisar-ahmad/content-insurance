using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance.Models.Content
{
    /// <summary>
    /// A high-valued item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Item Id
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// Name of item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of item
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Category Id to which this item belongs
        /// </summary>
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Category Navigation Property
        /// </summary>
        public Category Category { get; set; }
    }
}
