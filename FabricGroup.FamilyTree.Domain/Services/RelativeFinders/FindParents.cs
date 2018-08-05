using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindParents : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender? _parentGender;

        public FindParents(IRelationshipReadRepository relationshipReadRepository, Gender? parentGender = null)
            : base(relationshipReadRepository)
        {
            _parentGender = parentGender;
        }

        public List<Person> From(Person person)
        {
            return _relationshipReadRepository.GetParents(person.PersonId, _parentGender);
        }
    }
}
