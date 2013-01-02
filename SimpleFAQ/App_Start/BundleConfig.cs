using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace SimpleFAQ
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/js").Include(
				"~/Content/javascript/accordion.js",
				"~/Content/javascript/buttonset.js",
				"~/Content/javascript/carousel.js",
				"~/Content/javascript/dropdown.js",
				"~/Content/javascript/input-control.js",
				"~/Content/javascript/pagecontrol.js",
				"~/Content/javascript/rating.js",
				"~/Content/javascript/slider.js",
				"~/Content/javascript/tile-drag.js",
				"~/Content/javascript/tile-slider.js"));

			bundles.Add(new StyleBundle("~/bundles/css").Include(
				"~/Content/css/modern.css"));
		}
	}
}