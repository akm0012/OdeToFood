using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;


        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Dagwood's Pizza", Location = "Norcross", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Moxie Burger", Location = "Roswell", Cuisine = CuisineType.None},
                new Restaurant {Id = 3, Name = "Heart of India", Location = "Atlanta", Cuisine = CuisineType.Indian}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from restaurant in restaurants
                orderby restaurant.Name
                select restaurant;
        }
    }
}