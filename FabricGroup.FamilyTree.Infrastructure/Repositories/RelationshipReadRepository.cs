using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace FabricGroup.FamilyTree.Infrastructure.Repositories
{
    public class RelationshipReadRepository : IRelationshipReadRepository
    {
        private readonly FamilyTreeContext _familyTreeContext;

        public RelationshipReadRepository(FamilyTreeContext familyTreeContext)
        {
            _familyTreeContext = familyTreeContext;
        }

        public List<Person> GetPersonsByName(string name)
        {
            return _familyTreeContext.PersonList
                .Where(x => string.Compare(x.Name, name, true) == 0)
                .ToList();
        }

        public Couple GetCoupleByChild(int personIdOfChild)
        {
            var child = _familyTreeContext.ChildList.FirstOrDefault(x => x.PersonId == personIdOfChild);

            if (child == null)
            {
                return null;
            }

            return _familyTreeContext.CoupleList.FirstOrDefault(x => x.CoupleId == child.CoupleId);
        }

        public List<Couple> GetCouplesByChildren(List<int> personIdsOfChildren)
        {
            var result = new List<Couple>();

            var children = _familyTreeContext.ChildList
                            .Where(x => personIdsOfChildren.Contains(x.PersonId))
                            .ToList();

            if (children == null || children.Count <= 0)
            {
                return result;
            }

            var coupleIds = children.Select(x => x.CoupleId).Distinct().ToList();

            return _familyTreeContext.CoupleList
                .Where(x => coupleIds.Contains(x.CoupleId))
                .ToList();
        }

        public Couple GetCouple(int personIdOfPartner)
        {
            return _familyTreeContext.CoupleList
                .FirstOrDefault(x => 
                    x.PersonIdOfPartner1 == personIdOfPartner
                    || x.PersonIdOfPartner2 == personIdOfPartner
                );
        }

        public List<Couple> GetCouples(List<int> personIdsOfPartner)
        {
            return _familyTreeContext.CoupleList
                .Where(x => 
                    personIdsOfPartner.Contains(x.PersonIdOfPartner1)
                    || personIdsOfPartner.Contains(x.PersonIdOfPartner2)
                ).ToList();
        }

        public List<Person> GetParents(int personIdOfChild, Gender? parentGender = null)
        {
            var result = new List<Person>();

            var couple = GetCoupleByChild(personIdOfChild);

            if (couple == null)
            {
                return result;
            }

            result = _familyTreeContext.PersonList.Where(x => 
                                (x.PersonId == couple.PersonIdOfPartner1 || x.PersonId == couple.PersonIdOfPartner2)
                                && (!parentGender.HasValue || x.GenderId == parentGender.Value)
                            ).ToList();

            return result;
        }

        public List<Person> GetChildren(int coupleId, Gender? childrenGender = null, List<int> excludePersonIdsOfChildren = null)
        {
            excludePersonIdsOfChildren = excludePersonIdsOfChildren ?? new List<int>();

            var personIdsOfChildren = _familyTreeContext.ChildList
                                .Where(x => 
                                        x.CoupleId == coupleId
                                        && !excludePersonIdsOfChildren.Contains(x.PersonId)
                                )
                                .ToList()
                                .Select(x => x.PersonId)
                                .ToList();

            return _familyTreeContext.PersonList
                .Where(x => 
                            personIdsOfChildren.Contains(x.PersonId)
                            && (!childrenGender.HasValue || x.GenderId == childrenGender.Value)
                        ).ToList();
        }

        public List<Person> GetPartnerOfChildren(int coupleId, Gender? partnerGender = null, List<int> excludePersonIdsOfChildren = null)
        {
            excludePersonIdsOfChildren = excludePersonIdsOfChildren ?? new List<int>();

            var personIdsOfChildren = _familyTreeContext.ChildList
                                .Where(x =>
                                        x.CoupleId == coupleId
                                        && !excludePersonIdsOfChildren.Contains(x.PersonId)
                                )
                                .ToList()
                                .Select(x => x.PersonId)
                                .ToList();

            var partnerIdOfChildren = _familyTreeContext.CoupleList
                                .Where(x =>
                                    personIdsOfChildren.Contains(x.PersonIdOfPartner1)
                                    || personIdsOfChildren.Contains(x.PersonIdOfPartner2)
                                )
                                .SelectMany(x => new List<int> { x.PersonIdOfPartner1, x.PersonIdOfPartner2 })
                                .Where(x => !personIdsOfChildren.Contains(x))
                                .ToList();

            return _familyTreeContext.PersonList
                        .Where(x =>
                            partnerIdOfChildren.Contains(x.PersonId)
                            && (!partnerGender.HasValue || x.GenderId == partnerGender.Value)
                        ).ToList();
        }

        public List<Person> GetChildren(List<int> coupleIds, Gender? childrenGender = null, List<int> excludePersonIdsOfChildren = null)
        {
            excludePersonIdsOfChildren = excludePersonIdsOfChildren ?? new List<int>();

            var personIdsOfChildren = _familyTreeContext.ChildList
                                .Where(x =>
                                        coupleIds.Contains(x.CoupleId)
                                        && !excludePersonIdsOfChildren.Contains(x.PersonId)
                                )
                                .ToList()
                                .Select(x => x.PersonId)
                                .ToList();

            return _familyTreeContext.PersonList
                .Where(x =>
                            personIdsOfChildren.Contains(x.PersonId)
                            && (!childrenGender.HasValue || x.GenderId == childrenGender.Value)
                        ).ToList();
        }
    }
}
