using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class List : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;

        // Note: [BindProperty] will look for "name="searchTerm"" in the HTML and bind it to this proerty. Normally only works on POST
        [BindProperty(SupportsGet = true)] 
        public string SearchTerm { get; set; }

        // These are the fields our UI has access to
        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        // This constructor is kinda magic
        // config = appsettings.Development.json
        // restrauntData = An interface whose implementation is being injected. Defined in "Startup.cs"
        public List(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }


        // This is called whenever a GET is called (aka page is loaded)
        // This is where you should get all the data you will need to populate the fields above that the UI will use.
        public void OnGet()
        {
            Message = config["message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}