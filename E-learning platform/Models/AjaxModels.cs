using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_platform.Models
{
    public class AjaxResult
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
    }

    public class AjaxOrder
    {
        [Required]
        public string Tab { get; set; }
        [Required]
        public List<AjaxOrderListElement> Order { get; set; }
    }

    public class AjaxOrderListElement
    {
        public int Id { get; set; }
        public int Order { get; set; }
    }
}