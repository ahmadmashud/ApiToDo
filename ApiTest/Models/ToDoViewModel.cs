using System.ComponentModel.DataAnnotations;
using System;
namespace ApiTest.Models
{
    public class ToDoViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Completed Presentage")]
        public int CompletedPresentage { get; set; }

        [Display(Name = "Expiry Date")]
        public  DateTime ExpiryDate { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; }
    }
}
