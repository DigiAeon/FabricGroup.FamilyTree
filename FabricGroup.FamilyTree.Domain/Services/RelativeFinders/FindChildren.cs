using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindChildren : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender? _childrenGender;

        public FindChildren(IRelationshipReadRepository relationshipReadRepository, Gender? childrenGender = null)
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

            return _relationshipReadRepository.GetChildren(couple.CoupleId, _childrenGender);
        }
    }
}
