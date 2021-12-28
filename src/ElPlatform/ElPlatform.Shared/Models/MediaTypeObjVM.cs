using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Shared.Models
{
    public class MediaTypeObjVM
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
    }
    public class MediaTypeRequest : MediaTypeObjVM
    {

    }
}
