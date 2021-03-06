using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.DAL.Model
{
    public class MediaItemCategory
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int? ParantId { get; set; }

        [ForeignKey("ParantId")]
        public virtual MediaItemCategory ParantCategory { get; set; }
        public virtual ICollection<MediaItem> MediaItems { get; set; }
    }
}
