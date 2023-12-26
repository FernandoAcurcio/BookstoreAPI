using System;

namespace Core.Entities
{
    public class SalesItem : BaseEntity
    {
        public string Description { get; set; }
        public string SalesUnit { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}