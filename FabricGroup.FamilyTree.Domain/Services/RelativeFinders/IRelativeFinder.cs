using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal interface IRelativeFinder
    {
        List<Person> From(Person person);
    }
}
