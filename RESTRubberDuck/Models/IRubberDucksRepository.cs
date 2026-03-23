using System.ComponentModel;

namespace RESTRubberDuck.Models
{
    public interface IRubberDucksRepository
    {
        RubberDuck AddRubberDuck(RubberDuck duck);

        IEnumerable<RubberDuck> Get(
            int? priceAtLeast = null, string? descriptionStartsWith = null, string? sortOrder = null); 

        IEnumerable<RubberDuck> GetAllRubberDucks();
        RubberDuck? GetRubberDuckByID(int id);
        RubberDuck? DeleteRubberDuck(int id);
        RubberDuck? UpdateRubberDuck(int id, RubberDuck updatedDuck);
    }
}
