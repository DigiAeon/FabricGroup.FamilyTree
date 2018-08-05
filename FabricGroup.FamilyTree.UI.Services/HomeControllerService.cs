using FabricGroup.FamilyTree.Common;
using FabricGroup.FamilyTree.Domain.Services.Interfaces;
using FabricGroup.FamilyTree.Domain.Services.Models;
using FabricGroup.FamilyTree.UI.Services.Interfaces;
using FabricGroup.FamilyTree.UI.Services.Interfaces.Models;
using System.Linq;

namespace FabricGroup.FamilyTree.UI.Services
{
    public class HomeControllerService : IHomeControllerService
    {
        private readonly IRelationshipService _relationshipService;

        public HomeControllerService(IRelationshipService relationshipService)
        {
            _relationshipService = relationshipService;
        }

        public GetRelationshipsResponse GetRelationships()
        {
            return new GetRelationshipsResponse
            {
                Relationships = EnumHelper.NameDescriptionToDictionary<Relationships>()
                                .ToList()
                                .Select(x => new RelationshipDetails
                                {
                                    Name = x.Key,
                                    Description = x.Value
                                })
                                .ToList()
            };            
        }

        public FindRelativeResponse FindRelatives(FindRelativeRequest request)
        {
            if (!EnumHelper.IsValid<Relationships>(request.RelationshipName))
            {
                return null;
            }

            var realtionship = EnumHelper.Parse<Relationships>(request.RelationshipName);

            var relatives = _relationshipService.FindRelatives(realtionship, request.PersonName);

            if (relatives == null)
            {
                return null;
            }

            return new FindRelativeResponse
            {
                RelativeInfo = relatives
                                .ToList()
                                .Select(x => new RelativeInfo
                                {
                                    Person = new PersonInfo
                                    {
                                        PersonId = x.Person.PersonId,
                                        Name = x.Person.Name
                                    },
                                    Relatives = x.Relatives
                                                .Select(r => new PersonInfo
                                                {
                                                    PersonId = r.PersonId,
                                                    Name = r.Name
                                                }).
                                                ToList()
                                })
                                .ToList()
            };
        }
    }
}
