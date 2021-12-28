using ElPlatform.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.App.Components
{
    public partial class MediaItemCard
    {
        [Parameter]
        public MediaItemObjVM MediaItem { get; set; }
        [Parameter]
        public EventCallback<MediaItemObjVM> OnViewClicked { get; set; }
        [Parameter]
        public EventCallback<MediaItemObjVM> OnEditClicked { get; set; }
        [Parameter]
        public EventCallback<MediaItemObjVM> OnDeleteClicked { get; set; }
    }
}
