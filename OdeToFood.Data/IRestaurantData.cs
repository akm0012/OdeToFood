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
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
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

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}