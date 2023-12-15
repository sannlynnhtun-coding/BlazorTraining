using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTraining.Models
{
    public static class ApiEndpoints
    {
        public static string GetBlogs { get; } = "api/blog/list";
        public static string CreateBlog { get; } = "api/blog/create";
        public static string UpdateBlog { get; set; } = "api/blog/update";
       
    }
}
