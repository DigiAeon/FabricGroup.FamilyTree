using FabricGroup.FamilyTree.Domain.Repositories.Interfaces;
using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Services.RelativeFinders
{
    internal class FindInLaws : RelativeFinderBase, IRelativeFinder
    {
        private readonly Gender _inLawsGender;

        public FindInLaws(IRelationshipReadRepository relationshipReadRepository, Gender inLawsGender)
            : base(relationshipReadRepository)
        {
            _inLawsGender = inLawsGender;
        }

        public List<Person> From(Person person)
        {
            var result = new List<Person>();

            result.AddRange(GetSiblingsOfSpouse(person));
            result.AddRange(GetPartnerOfSiblings(person));

            return result;
        }

        private List<Person> GetSiblingsOfSpouse(Person person)
        {
            var result = new List<Person>();

            var couple = _relationshipReadRepository.GetCouple(person.PersonId);

            if (couple == null)
            {
                return result;
            }

            var personIdOfSpouse = couple.PersonIdOfPartner1 == person.PersonId
                                    ? couple.PersonIdOfPartner2
                                    : couple.PersonIdOfPartner1;

            var coupleOfSpouseParents = _relationshipReadRepository.GetCoupleByChild(personIdOfSpouse);

            if (coupleOfSpouseParents == null)
            {
                return result;
            }

            var spouseSiblings = _relationshipReadRepository.GetChildren(
                coupleOfSpouseParents.CoupleId,
                _inLawsGender,
                new List<int> { personIdOfSpouse }
                );

            if (spouseSiblings != null)
            {
                result.AddRange(spouseSiblings);
            }

            return result;
        }

        private List<Person> GetPartnerOfSiblings(Person person)
        {
            var result = new List<Person>();

            var coupleOfParents = _relationshipReadRepository.GetCoupleByChild(person.PersonId);

            if (coupleOfParents == null)
            {
                return result;
            }

            var partnersOfSiblings = _relationshipReadRepository.GetPartnerOfChildren(
                    coupleOfParents.CoupleId,
                    _inLawsGender,
                    new List<int> { person.PersonId }
                );

            if (partnersOfSiblings != null)
            {
                result.AddRange(partnersOfSiblings);
            }

            return result;
        }
    }
}
