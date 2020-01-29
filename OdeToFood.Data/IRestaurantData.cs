using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int Id);
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

        public Restaurant GetById(int Id)
        {
            return restaurants.SingleOrDefault(r => r.Id == Id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from restaurant in restaurants
                where string.IsNullOrEmpty(name) || restaurant.Name.StartsWith(name)
                orderby restaurant.Name
                select restaurant;
        }
    }
}