using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.Models
{
    public class RelativeDetails
    {
        public Person Person { get; set; }

        public List<Person> Relatives { get; set; }
    }
}
