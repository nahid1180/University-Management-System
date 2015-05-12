using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementMVCandEF.Models
{
    [Table("GradeLetter")]
    public class GradeLetter
    {
        public int GradeLetterId { set; get; }
        public string Name { set; get; }
    }
}