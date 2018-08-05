using FabricGroup.FamilyTree.UI.Services.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.UI.Services.Interfaces
{
    public interface IHomeControllerService
    {
        GetRelationshipsResponse GetRelationships();

        FindRelativeResponse FindRelatives(FindRelativeRequest request);
    }
}
