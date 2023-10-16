using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoappAssignment.Models
{
    public class selectclassListmodel
    {
        public string Text { get; set; }
        [Key]
        public int Id { get; set; }
    }
}