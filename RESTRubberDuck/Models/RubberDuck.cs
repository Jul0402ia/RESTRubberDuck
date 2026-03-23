namespace RESTRubberDuck.Models
{
    public class RubberDuck
    {
        public int ID { get; set; }
        public string? Design { get; set; } // undgå runtime error ved at gøre Design nullable
        public int Price { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Design: {Design}, Price: {Price}";
        }
    }
}
