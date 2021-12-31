using NSubstitute;
using NUnit.Framework;
using Sc.Edu.Web.Domain;
using Sitecore.Mvc.Presentation;

namespace Sc.Edu.Tests.Unit
{
    [TestFixture]
    public class BasicContentExtractorTests
    {
        [Test]
        public void GetSlidesAmount_RenderingParametersEmpty_ReturnsTwo()
        {
            var sut = new BasicContentExtractor();

            var rendering = Substitute.For<Sitecore.Mvc.Presentation.Rendering>();
            rendering.Parameters.Returns(new RenderingParameters("SlideCount=3"));
            //rendering.Parameters["SlideCount"].Returns("3");

            var slideCount = sut.GetSlidesAmount(rendering);

            Assert.That(slideCount, Is.EqualTo(3));
        }
    }
}