using Microsoft.AspNetCore.Mvc.Rendering;
using Practical.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practical.ViewModels
{
    public class ProductVM
    {
        public Product Products { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
