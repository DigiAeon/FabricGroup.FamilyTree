using System;

namespace FabricGroup.FamilyTree.Infrastructure.Repositories
{
    public class RelationshipWriteRepository : IRelationshipWriteRepository
    {
        private readonly FamilyTreeContext _familyTreeContext;

        public RelationshipWriteRepository(FamilyTreeContext familyTreeContext)
        {
            _familyTreeContext = familyTreeContext;
        }
    }
}
