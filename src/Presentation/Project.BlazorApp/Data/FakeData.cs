using System;
using Project.BlazorApp.Models;

namespace Project.BlazorApp.Data
{
    public static class FakeData
    {
        public static List<MenuItem> GetFakeMenu()
        {
            var industriesId = Guid.NewGuid();
            return new List<MenuItem>()
            {
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 0,
                    Title = "Home",
                    Url = "/",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 1,
                    Title = "About",
                    Url = "/about",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = industriesId,
                    AppearanceOrder = 2,
                    Title = "Industries",
                    Url = "#",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 3,
                    Title = "News",
                    Url = "/news",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 4,
                    Title = "Announcements",
                    Url = "/announcements",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 5,
                    Title = "Contact",
                    Url = "/contact",
                    ParentId = null
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    AppearanceOrder = 0,
                    Title = "Oil",
                    Url = "/industries/oil",
                    ParentId = industriesId
                }
            };
        }
    
        public static List<Slide> GetSlides()
        {
            return new List<Slide>()
            {
                new Slide
                {
                    Id = Guid.NewGuid(),
                    Title = "The Best Move You Will Ever Make",
                    Text = "Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa",
                    PrimaryButtonText = "",
                    PrimaryButtonUrl = "#",
                    SecondaryButtonText = "",
                    SecondaryButtonUrl = "#",
                    AppearanceOrder = 0,
                    ImageUrl = "assets/images/slider/slide-3.jpg"
                },
                new Slide
                {
                    Id = Guid.NewGuid(),
                    Title = "The Best Move You Will Ever Make",
                    Text = "Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa",
                    PrimaryButtonText = "OUR SERVICES",
                    PrimaryButtonUrl = "#",
                    SecondaryButtonText = "MORE ABOUT US",
                    SecondaryButtonUrl = "#",
                    AppearanceOrder = 1,
                    ImageUrl = "assets/images/slider/slide-2.jpg"
                },
                new Slide
                {
                    Id = Guid.NewGuid(),
                    Title = "The Best Move You Will Ever Make",
                    Text = "Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa",
                    PrimaryButtonText = "Industries",
                    PrimaryButtonUrl = "#",
                    SecondaryButtonText = "",
                    SecondaryButtonUrl = "#",
                    AppearanceOrder = 2,
                    ImageUrl = "assets/images/slider/slide-1.jpg"
                }
            };
        }
    
        public static GeneralInformation GetGeneralInformation()
        {
            return new GeneralInformation
            {
                SiteTitle = "White Gold",
                SiteDescription = "White Gold factory",

                HeaderPhoneNumber = "+98-17-25844196",

                MaxSlidesCount = 3,

                HomeAboutIntroTitle = "We set the standards others try to live up to.",
                HomeAboutIntroContent = @"
                <video width='100%' controls>
                        <source src='assets/videos/intro.mp4' type='video/mp4'>
                    </video>
                    <p>It wasn't a dream. His room, a proper human room although a little too small, lay peacefully between its four familiar walls. A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright</p>
                    <div class='clearfix'>
                        <ul>
                            <li><i class='ti-check'></i> Cut out of an illustrated magazine</li>
                            <li><i class='ti-check'></i> Showed a lady fitted out</li>
                        </ul>
                        <ul>
                            <li><i class='ti-check'></i> Raising a heavy fur muff</li>
                            <li><i class='ti-check'></i> Magazine and housed in a nice</li>
                        </ul>
                    </div>

                ",
                HomeAboutIntroImageUrl = "assets/images/about.jpg",

                IndustriesSectionTitle = "Delivering the Best Global Industry Services.",
                IndustriesSectionText = @"<p>Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm </p>",

                HomeFeaturesTitle = "Why Choose Us!",
                HomeFeaturesText = @"<p>Hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him</p>",

                CustomersCommentTitle = "What people say's About us"

            };
        }

        public static List<CustomerComment> GetCustomerComments()
        {
            return new List<CustomerComment>()
            {
                new CustomerComment
                {
                    Id = Guid.NewGuid(),
                    FullName = "Michel Jhon",
                    ImageUrl = "assets/images/testimonials/img-1.jpg",
                    Role = "Manager of Automation",
                    Comment = "“Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer. Gregor then turned to look ”",
                    AppearanceOrder = 0
                },
                new CustomerComment
                {
                    Id = Guid.NewGuid(),
                    FullName = "Alaska",
                    ImageUrl = "assets/images/testimonials/img-2.jpg",
                    Role = "Business Officer",
                    Comment = "“Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer. Gregor then turned to look ”",
                    AppearanceOrder = 1
                },
                new CustomerComment
                {
                    Id = Guid.NewGuid(),
                    FullName = "Shain on",
                    ImageUrl = "assets/images/testimonials/img-3.jpg",
                    Role = "Manager of Automation",
                    Comment = "“Recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer. Gregor then turned to look ”",
                    AppearanceOrder = 2
                },
            };
        }

        public static List<Industry> GetIndustries()
        {
            return new List<Industry>()
            {
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-gear'></i>",
                    Title = "Mechanical Engineering",
                    Slug = "mechanical",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 0
                },
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-expansion'></i>",
                    Title = "Silos",
                    Slug = "silos",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 1
                },
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-oil'></i>",
                    Title = "Soybean Meal",
                    Slug = "soybean",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 2
                },
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-pharmacy'></i>",
                    Title = "Pharmaceutical Research",
                    Slug = "pharmacy",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 3
                },
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-paint-palette'></i>",
                    Title = "Painting &Protective",
                    Slug = "painting",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 4
                },
                new Industry
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-solar-energy'></i>",
                    Title = "Electrical & Power",
                    Slug = "electronical",
                    Description = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    Content = "Samsa was a travelling salesman and above it there hung a picture that he had recently cut out of antrt",
                    AppearanceOrder = 5
                }
            };
        }

        public static List<Feature> GetFeatures()
        {
            return new List<Feature>()
            {
                new Feature
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-network-1'></i>",
                    Title = "Professional Team",
                    Text = "Whole of her lower arm towards the viewer. Gregor then turned to look out the window",
                    AppearanceOrder = 0
                },
                new Feature
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-circular-label-with-certified-stamp'></i>",
                    Title = "Certified engineers",
                    Text = "Whole of her lower arm towards the viewer. Gregor then turned to look out the window",
                    AppearanceOrder = 1
                },
                new Feature
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-chip'></i>",
                    Title = "Latest Technology",
                    Text = "Whole of her lower arm towards the viewer. Gregor then turned to look out the window",
                    AppearanceOrder = 2
                },
                new Feature
                {
                    Id = Guid.NewGuid(),
                    Icon = "<i class='fi flaticon-24-hours'></i>",
                    Title = "27/7 Support",
                    Text = "Whole of her lower arm towards the viewer. Gregor then turned to look out the window",
                    AppearanceOrder = 3
                },
            };
        }

    }
}