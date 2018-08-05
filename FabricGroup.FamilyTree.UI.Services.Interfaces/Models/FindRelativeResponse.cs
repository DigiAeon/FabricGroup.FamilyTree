using System.Collections.Generic;

namespace FabricGroup.FamilyTree.UI.Services.Interfaces.Models
{
    public class FindRelativeResponse
    {
        public List<RelativeInfo> RelativeInfo { get; set; }
    }

    public class RelativeInfo
    {
        public PersonInfo Person { get; set; }

        public List<PersonInfo> Relatives { get; set; }
    }

    public class PersonInfo
    {
        public int PersonId { get; set; }

        public string Name { get; set; }
    }
}
