using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class Detail : PageModel
    {
        private readonly IRestaurantData restaurantData;

        // Here we are binging some TempData. The spelling of "Message" is important, as that ths Key used to store 
        // the data. (See Edit.cs model)
        [TempData]
        public string Message { get; set; }
        
        public Restaurant Restaurant { get; set; }

        public Detail(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            
            return Page();
        }
    }
}