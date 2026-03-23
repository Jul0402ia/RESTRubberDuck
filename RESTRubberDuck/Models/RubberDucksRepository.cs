using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RESTRubberDuck.Models
{
    public class RubberDucksRepository : IRubberDucksRepository
    {
        private readonly RubberDucksDBContext dbContext;

       

        private readonly List<RubberDuck> rubberDucks = new List<RubberDuck>(); //in-memory liste til at gemme rubberducks (laver en kopi senere - beskyttelse) 
        private int nextID = 1; //bruges til at tildele unikke ID'er

        
        public RubberDucksRepository()
        {
           
            {
                AddRubberDuck(new RubberDuck { Design = "Moose", Price = 123 });
                AddRubberDuck(new RubberDuck { Design = "Swedish", Price = 45 });
                AddRubberDuck(new RubberDuck { Design = "Viking", Price = 334 });
            }
        }

        public IEnumerable<RubberDuck> GetAllRubberDucks()
        {
            return rubberDucks.Select(rubberDuck => new RubberDuck //returnerer en kopi af hver rubberduck for at beskytte den originale liste
            {
                ID = rubberDuck.ID,
                Design = rubberDuck.Design,
                Price = rubberDuck.Price
            }).ToList();
        }
        public IEnumerable<RubberDuck> Get(int? priceAtLeast = null, string? descriptionStartsWith = null, string? sortOrder = null)
        {
            IEnumerable<RubberDuck> result = new List<RubberDuck>(rubberDucks); //start med alle rubberducks
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

        public RubberDuck AddRubberDuck(RubberDuck rubberduck)
        {
            if (rubberduck == null)
            {
                throw new ArgumentNullException(nameof(rubberduck));
            }
            RubberDuck newDuck = new RubberDuck
            {
                ID = nextID++,
                Design = rubberduck.Design,
                Price = rubberduck.Price
            };

            rubberDucks.Add(newDuck);
            return new RubberDuck
            {
                ID = newDuck.ID,
                Design = newDuck.Design,
                Price = newDuck.Price
            };
        }
        public RubberDuck? GetRubberDuckByID(int id)
        {
            RubberDuck? duck = rubberDucks.FirstOrDefault(d => d.ID == id);

            if (duck == null)
            {
                return null;
            }

            return new RubberDuck
            {
                ID = duck.ID,
                Design = duck.Design,
                Price = duck.Price
            };
        }

        public RubberDuck? DeleteRubberDuck(int id)
        {
            RubberDuck? rubberDuck = rubberDucks.FirstOrDefault(d => d.ID == id);

            if (rubberDuck != null)
            {
                rubberDucks.Remove(rubberDuck);

                return new RubberDuck
                {
                    ID = rubberDuck.ID,
                    Design = rubberDuck.Design,
                    Price = rubberDuck.Price
                };
            }

            return null;
        }
        public RubberDuck? UpdateRubberDuck(int id, RubberDuck updatedRubberDuck)
        {
            RubberDuck? rubberDuck = rubberDucks.FirstOrDefault(d => d.ID == id);

            if (rubberDuck != null)
            {
                rubberDuck.Design = updatedRubberDuck.Design;
                rubberDuck.Price = updatedRubberDuck.Price;

                return new RubberDuck
                {
                    ID = rubberDuck.ID,
                    Design = rubberDuck.Design,
                    Price = rubberDuck.Price
                };
            }

            return null;
        }
    }

}
