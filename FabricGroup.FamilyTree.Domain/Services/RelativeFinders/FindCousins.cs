using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindCousins : RelativeFinderBase, IRelativeFinder
    {
        public FindCousins(IRelationshipReadRepository relationshipReadRepository)
            : base(relationshipReadRepository)
        {
        }

        public List<Person> From(Person person)
        {
            var result = new List<Person>();

            var parents = _relationshipReadRepository.GetParents(person.PersonId);

            if (parents == null || parents.Count <= 0)
            {
                return result;
            }

            var personIdsOfParents = parents.Select(x => x.PersonId).ToList();

            var couplesOfGrandParents = _relationshipReadRepository.GetCouplesByChildren(personIdsOfParents);

            if (couplesOfGrandParents == null || couplesOfGrandParents.Count <= 0)
            {
                return result;
            }

            var coupleIdsOfGrandParents = couplesOfGrandParents.Select(x => x.CoupleId).ToList();

            var siblingsOfParents = _relationshipReadRepository.GetChildren(
                                        coupleIdsOfGrandParents,
                                        null,
                                        personIdsOfParents
                                    );

            if (siblingsOfParents == null || siblingsOfParents.Count <= 0)
            {
                return result;
            }

            var couplesOfSiblingsOfParents = _relationshipReadRepository.GetCouples(
                    siblingsOfParents.Select(x => x.PersonId).ToList()
                );

            if (couplesOfSiblingsOfParents == null || couplesOfSiblingsOfParents.Count <= 0)
            {
                return result;
            }

            var cousins = _relationshipReadRepository.GetChildren(
                                couplesOfSiblingsOfParents.Select(x => x.CoupleId).ToList()
                            );

            if (cousins == null || cousins.Count <= 0)
            {
                return result;
            }

            return cousins;
        }
    }
}
