using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal abstract class RelativeFinderBase
    {
        protected readonly IRelationshipReadRepository _relationshipReadRepository;

        public RelativeFinderBase(IRelationshipReadRepository relationshipReadRepository)
        {
            _relationshipReadRepository = relationshipReadRepository;
        }
    }
}
