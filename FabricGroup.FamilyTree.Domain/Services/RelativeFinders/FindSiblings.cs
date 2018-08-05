using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindSiblings : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender? _siblingsGender;

        public FindSiblings(IRelationshipReadRepository relationshipReadRepository, Gender? siblingsGender = null)
            : base(relationshipReadRepository)
        {
            _siblingsGender = siblingsGender;
        }

        public List<Person> From(Person person)
        {
            var couple = _relationshipReadRepository.GetCoupleByChild(person.PersonId);

            if (couple == null)
            {
                return new List<Person>();
            }

            return _relationshipReadRepository.GetChildren(
                couple.CoupleId,
                _siblingsGender,
                new List<int> { person.PersonId });
        }
    }
}
