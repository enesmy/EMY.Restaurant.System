﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMY.Restaurant.Presentation.Web.Components.FooresRestaurant
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_Footer"));
        }

    }
}
