using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.BAL.ViewModels
{
    public class MediaItemRequestVM
    {
        public int Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string URL { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public int MediaTypeId { get; set; }
        public int MediaCategoryId { get; set; }
    }
}
