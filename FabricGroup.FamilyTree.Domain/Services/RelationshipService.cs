using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Services.Interfaces;
using FabricGroup.FamilyTree.Domain.Services.Models;
using FabricGroup.FamilyTree.Domain.Services.RelativeFinders;
using FabricGroup.FamilyTree.Infrastructure.Repositories;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IRelationshipReadRepository _relationshipReadRepository;
        private readonly IRelationshipWriteRepository _relationshipWriteRepository;
        private readonly RelativeFinderProvider _relativeFindersProvider;

        public RelationshipService(IRelationshipReadRepository relationshipReadRepository, IRelationshipWriteRepository relationshipWriteRepository)
        {
            _relationshipReadRepository = relationshipReadRepository;
            _relationshipWriteRepository = relationshipWriteRepository;

            _relativeFindersProvider = new RelativeFinderProvider(relationshipReadRepository);
        }

        public List<RelativeDetails> FindRelatives(Relationships relationship, string name)
        {
            var result = new List<RelativeDetails>();

            var persons = _relationshipReadRepository.GetPersonsByName(name);

            if (persons == null || persons.Count <= 0)
            {
                return result;
            }

            foreach (var person in persons)
            {
                result.Add(new RelativeDetails
                {
                    Person = person,
                    Relatives = _relativeFindersProvider.GetRelativeFinder(relationship).From(person)
                });
            }

            return result;
        }
    }
}
