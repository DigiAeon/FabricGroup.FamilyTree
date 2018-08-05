using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using FabricGroup.FamilyTree.Domain.Services.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class RelativeFinderProvider
    {
        private Dictionary<Relationships, IRelativeFinder> _relativeFinders = new Dictionary<Relationships, IRelativeFinder>();

        public RelativeFinderProvider(IRelationshipReadRepository _relationshipReadRepository)
        {
            _relativeFinders.Add(Relationships.Father, new FindParents(_relationshipReadRepository, Gender.Male));
            _relativeFinders.Add(Relationships.Mother, new FindParents(_relationshipReadRepository, Gender.Female));

            _relativeFinders.Add(Relationships.Brother, new FindSiblings(_relationshipReadRepository, Gender.Male));
            _relativeFinders.Add(Relationships.Sister, new FindSiblings(_relationshipReadRepository, Gender.Female));

            _relativeFinders.Add(Relationships.Son, new FindChildren(_relationshipReadRepository, Gender.Male));
            _relativeFinders.Add(Relationships.Daughter, new FindChildren(_relationshipReadRepository, Gender.Female));
            _relativeFinders.Add(Relationships.Children, new FindChildren(_relationshipReadRepository));

            _relativeFinders.Add(Relationships.GrandSon, new FindGrandChildren(_relationshipReadRepository, Gender.Male));
            _relativeFinders.Add(Relationships.GrandDaughter, new FindGrandChildren(_relationshipReadRepository, Gender.Female));
            _relativeFinders.Add(Relationships.GrandChildren, new FindGrandChildren(_relationshipReadRepository));

            _relativeFinders.Add(Relationships.MaternalUncle, new FindMaternalOrPaternal(_relationshipReadRepository, Gender.Female, Gender.Male));
            _relativeFinders.Add(Relationships.PaternalUncle, new FindMaternalOrPaternal(_relationshipReadRepository, Gender.Male, Gender.Male));
            _relativeFinders.Add(Relationships.MaternalAunt, new FindMaternalOrPaternal(_relationshipReadRepository, Gender.Female, Gender.Female));
            _relativeFinders.Add(Relationships.PaternalAunt, new FindMaternalOrPaternal(_relationshipReadRepository, Gender.Male, Gender.Female));

            _relativeFinders.Add(Relationships.SisterInLaw, new FindInLaws(_relationshipReadRepository, Gender.Female));
            _relativeFinders.Add(Relationships.BrotherInLaw, new FindInLaws(_relationshipReadRepository, Gender.Male));

            _relativeFinders.Add(Relationships.Cousin, new FindCousins(_relationshipReadRepository));
        }

        public IRelativeFinder GetRelativeFinder(Relationships relationshipId)
        {
            if (_relativeFinders.ContainsKey(relationshipId))
            {
                return _relativeFinders[relationshipId];
            }

            return null;
        }
    }
}
