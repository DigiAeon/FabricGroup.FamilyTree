using FabricGroup.FamilyTree.Domain.Services.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.Interfaces
{
    public interface IRelationshipService
    {
        List<RelativeDetails> FindRelatives(Relationships relationship, string personName);
    }
}
