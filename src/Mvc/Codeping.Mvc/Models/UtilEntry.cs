using System.ComponentModel.DataAnnotations;

namespace Codeping.Utils.Mvc
{
    public abstract class UtilEntry
    {
        [Key]
        public int Id { get; set; }
    }
}
