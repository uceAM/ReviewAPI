﻿namespace ReviewAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //join tables
        public ICollection<PokemonCategory> PokemonCategories { get; set; }
}
}
