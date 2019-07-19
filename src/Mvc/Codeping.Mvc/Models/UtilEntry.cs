using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codeping.Utils.Mvc
{
    public abstract class UtilEntry
    {
        [Key]
        public int Id { get; set; }
    }
}
