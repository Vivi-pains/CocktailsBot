//using CocktailsApi.Data;
using System;
using System.Threading.Tasks;

namespace CocktailsApi.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CocktailName{ get; set; }

        //public async Task DbAdd()
        //{
        //    using (var context = new MyDbContext())
        //    {
        //        context.Favorites.Add(this);
        //        await context.SaveChangesAsync();
        //    }
        //}

    }
}