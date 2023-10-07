using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class SubMenu
    {
        public int SubMenuId { get; set; }
        public string? Name { get; set; }
        public int? SubOrder { get; set; }
        public string? Icon { get; set; }
        public int? MenuId { get; set; }
        public string? Url { get; set; }

        public virtual Menu? Menu { get; set; }
    }
}
