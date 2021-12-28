using ElPlatform.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.App.Components
{
    public partial class MediaItemCarsList
    {
        private bool _isBusy = false;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        [Parameter]
        public Func<int,int,Task<PagedList<MediaItemObjVM>>> FetchMediaItems { get; set; }
        private PagedList<MediaItemObjVM> _mediaItems = new ();
        protected async override Task OnInitializedAsync()
        {
            _isBusy = false;
            _mediaItems = await FetchMediaItems.Invoke(_pageNumber, _pageSize);
            _isBusy = true;
        }
    }
}
