﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sc.Edu.Web.Models;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace Sc.Edu.Web.Controllers
{
    public class CarouselController : Controller
    {
        // GET: Carousel
        public ActionResult Index()
        {
            var model = new CarouselModel();
            List<Slide> slides = new List<Slide>();
            var dataSource = RenderingContext.Current?.Rendering.Item;

            MultilistField slidesField = dataSource.Fields["Slides"];

            var slideCountParam = RenderingContext.Current?.Rendering.Parameters["SlideCount"];
            int.TryParse(slideCountParam, out int result);
            int slideCount = result == 0 ? 2 : result;

            if (slidesField.Count > 0)
            {
                var slideItems = slidesField.GetItems();
                foreach (var slideItem in slideItems.Take(slideCount))
                {
                    var titleField = slideItem.Fields["Title"];
                    var title = titleField.Value;

                    var subTitle = new MvcHtmlString(FieldRenderer.Render(slideItem,
                            "Sub_title"));

                    var image = new MvcHtmlString(FieldRenderer.Render(slideItem,
                        "Image"));

                    var callToAction = new MvcHtmlString(FieldRenderer.Render(slideItem,
                        "Call_To_Action", "class=btn animated fadeInUp" ));

                    slides.Add(new Slide
                    {
                        Title =title,
                        SubTitle = subTitle,
                        Image = image,
                        CallToAction = callToAction
                    });
                }

                model.Slides = slides;
            }
            return View(model);
        }
    }
}