using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.DbEntities
{
    public class ToDo : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompletedPresentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}
