using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Sites;

namespace Sc.Edu.Web.Domain
{
    public class BasicContentExtractor : IBasicContentExtractor
    {
        public int GetSlidesAmount(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            var slideCountParam = rendering.Parameters["SlideCount"];
            int.TryParse(slideCountParam, out int result);
            int slideCount = result == 0 ? 2 : result;

            return slideCount;
        }
    }

    public interface IBasicContentExtractor
    {
        int GetSlidesAmount(Sitecore.Mvc.Presentation.Rendering rendering);
    }
}