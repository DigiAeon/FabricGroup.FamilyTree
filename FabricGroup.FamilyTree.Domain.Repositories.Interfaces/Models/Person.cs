namespace FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public Gender GenderId { get; set; }
    }
}
