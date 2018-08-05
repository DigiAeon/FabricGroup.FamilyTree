using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindGrandChildren : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender? _childrenGender;

        public FindGrandChildren(IRelationshipReadRepository relationshipReadRepository, Gender? childrenGender = null)
            : base(relationshipReadRepository)
        {
            _childrenGender = childrenGender;
        }

        public List<Person> From(Person person)
        {
            var couple = _relationshipReadRepository.GetCouple(person.PersonId);

            if (couple == null)
            {
                return new List<Person>();
            }

            var children = _relationshipReadRepository.GetChildren(couple.CoupleId);

            var childrenIds = children.Select(x => x.PersonId).ToList();

            var couples = _relationshipReadRepository.GetCouples(childrenIds);

            return _relationshipReadRepository.GetChildren(
                    couples.Select(x => x.CoupleId).ToList(),
                    _childrenGender
                   );
        }
    }
}
