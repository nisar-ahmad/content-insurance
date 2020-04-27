namespace Insurance.Api.ViewModels
{
    /// <summary>
    /// Item ViewModel for SPA
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
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
        /// Category Id that this item belongs to
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Category that this item belongs to
        /// </summary>
        public string CategoryName { get; set; }
    }
}
