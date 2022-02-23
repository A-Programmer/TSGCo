using System;

namespace Project.BlazorApp.Models
{
    public class GeneralInformation
    {
        //General Info
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }

        //Header variables
        public string HeaderPhoneNumber { get; set; }
        
        //Slider
        public int MaxSlidesCount { get; set; }

        //Home About Intro Section
        public string HomeAboutIntroTitle { get; set; }
        public string HomeAboutIntroContent { get; set; }
        public string HomeAboutIntroImageUrl { get; set; }

        //Industries Section
        public string IndustriesSectionTitle { get; set; }
        public string IndustriesSectionText { get; set; }

        //Home Features Section
        public string HomeFeaturesTitle { get; set; }
        public string HomeFeaturesText { get; set; }

        //Home Customers Commets Section
        public string CustomersCommentTitle { get; set; }


    }
}