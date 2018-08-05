using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Repositories.Interfaces
{
    public interface IRelationshipReadRepository
    {
        List<Person> GetPersonsByName(string name);

        Couple GetCoupleByChild(int personIdOfChild);

        List<Couple> GetCouplesByChildren(List<int> personIdsOfChildren);

        Couple GetCouple(int personIdOfPartner);

        List<Couple> GetCouples(List<int> personIds);

        List<Person> GetParents(int personId, Gender? parentGender = null);

        List<Person> GetChildren(int coupleId, Gender? childrenGender = null, List<int> excludePersonIdsOfChildren = null);        

        List<Person> GetChildren(List<int> coupleIds, Gender? childrenGender = null, List<int> excludePersonIdsOfChildren = null);

        List<Person> GetPartnerOfChildren(int coupleId, Gender? partnerGender, List<int> excludePersonIdsOfChildren = null);
    }
}
