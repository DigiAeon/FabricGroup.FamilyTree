using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindMaternalOrPaternal : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender _parentGender;
        private readonly Gender _siblingsAndInLawsGender;

        public FindMaternalOrPaternal(IRelationshipReadRepository relationshipReadRepository, Gender parentGender, Gender siblingsAndInLawsGender)
             : base(relationshipReadRepository)
        {
            _parentGender = parentGender;
            _siblingsAndInLawsGender = siblingsAndInLawsGender;
        }

        public List<Person> From(Person person)
        {
            var result = new List<Person>();

            var parents = _relationshipReadRepository.GetParents(person.PersonId, _parentGender);

            if (parents == null || parents.Count <= 0)
            {
                return result;
            }

            foreach (var parent in parents)
            {
                var coupleOfGrandParents = _relationshipReadRepository.GetCoupleByChild(parent.PersonId);

                if (coupleOfGrandParents != null)
                {
                    var siblings = _relationshipReadRepository.GetChildren(
                        coupleOfGrandParents.CoupleId,
                        _siblingsAndInLawsGender,
                        new List<int> { parent.PersonId });

                    if (siblings != null)
                    {
                        result.AddRange(siblings);
                    }

                    var inLawsOfSiblings = _relationshipReadRepository.GetPartnerOfChildren(
                        coupleOfGrandParents.CoupleId,
                        _siblingsAndInLawsGender,
                        new List<int> { parent.PersonId });

                    if (inLawsOfSiblings != null)
                    {
                        result.AddRange(inLawsOfSiblings);
                    }
                }
            }

            return result;
        }
    }
}
