using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Menu
    {
        public Menu()
        {
            SubMenus = new HashSet<SubMenu>();
        }

        public int MenuId { get; set; }
        public string? Name { get; set; }
        public int? MenuOrder { get; set; }
        public string? Icon { get; set; }

        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
