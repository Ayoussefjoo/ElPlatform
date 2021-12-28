using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Shared.Models
{
    public class MediaCategoryObjVM
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string ParantNameAr { get; set; }
        public string ParantNameEn { get; set; }
        public bool IsActive { get; set; }
        public int? ParantId { get; set; }
        public MediaCategoryObjVM ParantItem { get; set; }
        
    }
    public class MediaCategoryRequestVM
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public int ParantId { get; set; }
    }
}
