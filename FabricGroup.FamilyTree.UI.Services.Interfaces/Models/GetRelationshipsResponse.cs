using System.Collections.Generic;

namespace FabricGroup.FamilyTree.UI.Services.Interfaces.Models
{
    public class GetRelationshipsResponse
    {
        public List<RelationshipDetails> Relationships { get; set; }
    }

    public class RelationshipDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
