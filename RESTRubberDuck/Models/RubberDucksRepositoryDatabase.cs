using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RESTRubberDuck.Models
{
    public class RubberDucksRepositoryDatabase : IRubberDucksRepository
    {
        private readonly RubberDucksDBContext context;
        public RubberDucksRepositoryDatabase(RubberDucksDBContext context)
        {
            this.context = context;
        }
        public RubberDuck AddRubberDuck(RubberDuck duck)
        {
            if (duck == null)
            {
                throw new ArgumentNullException(nameof(duck));
            }
            context.RubberDucks.Add(duck);
            context.SaveChanges(); 
            return duck;
        }
        public IEnumerable<RubberDuck> GetAllRubberDucks()
        {
            return context.RubberDucks.ToList();
        }
        public RubberDuck? GetRubberDuckByID(int id)
        {
            return context.RubberDucks.Find(id);
        }
        public RubberDuck? DeleteRubberDuck(int id)
        {
            var duck = context.RubberDucks.Find(id);
            if (duck == null)
            {
                return null;
            }
            context.RubberDucks.Remove(duck);
            context.SaveChanges();
            return duck;
        }
        public RubberDuck? UpdateRubberDuck(int id, RubberDuck updatedDuck)
        {
            var duck = context.RubberDucks.Find(id);
            if (duck == null)
            {
                return null;
            }
            duck.Design = updatedDuck.Design;
            duck.Price = updatedDuck.Price;
            context.SaveChanges();
            return duck;
        }
        public IEnumerable<RubberDuck> Get(int? priceAtLeast = null, string? descriptionStartsWith = null, string? sortOrder = null)
        {
            IEnumerable<RubberDuck> result = new List<RubberDuck>(); //start med alle rubberducks
            if (priceAtLeast != null)
            {
                result = result.Where(rubberDuck => rubberDuck.Price >= priceAtLeast.Value); //filtrér på pris
            }
            if (descriptionStartsWith != null)
            {
                result = result.Where(d =>
                                   d.Design != null &&
                                   d.Design.StartsWith(descriptionStartsWith));
            }
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "price_asc":
                        result = result.OrderBy(rubberDuck => rubberDuck.Price);
                        break;

                    case "price_desc":
                        result = result.OrderByDescending(rubberDuck => rubberDuck.Price);
                        break;

                }
            }
            return result;
        }
    }
}
