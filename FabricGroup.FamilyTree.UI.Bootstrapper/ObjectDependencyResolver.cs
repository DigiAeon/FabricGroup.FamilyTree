using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Services;
using FabricGroup.FamilyTree.Domain.Services.Interfaces;
using FabricGroup.FamilyTree.Infrastructure.Repositories;
using FabricGroup.FamilyTree.UI.Services;
using FabricGroup.FamilyTree.UI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FabricGroup.FamilyTree.UI.Bootstrapper
{
    public static class ObjectDependencyResolver
    {
        public static void RegisterToServiceCollection(ref IServiceCollection services)
        {
            services.AddTransient(ftc => new FamilyTreeContext(FamilyTreeDatabaseHelper.GetFamilyTreeContextInMemoryOptions()));
            services.AddTransient<IRelationshipReadRepository, RelationshipReadRepository>();
            services.AddTransient<IRelationshipWriteRepository, RelationshipWriteRepository>();
            services.AddTransient<IRelationshipService, RelationshipService>();
            services.AddTransient<IHomeControllerService, HomeControllerService>();
        }
    }
}
