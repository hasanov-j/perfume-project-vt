using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GR_30321.UI.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "img-action, img-controller")]
    public class ImageTagHelper : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;

        public ImageTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HtmlAttributeName("img-controller")]
        public string ImgController { get; set; }

        [HtmlAttributeName("img-action")]
        public string ImgAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var path = _linkGenerator.GetPathByAction(ImgAction, ImgController);
            output.Attributes.SetAttribute("src", path);
        }
    }
}
