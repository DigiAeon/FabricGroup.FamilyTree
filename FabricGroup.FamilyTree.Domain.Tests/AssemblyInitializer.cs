using FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models;
using FabricGroup.FamilyTree.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FabricGroup.FamilyTree.Domain.Tests
{
    [TestClass]
    public class AssemblyInitializer
    {
        public static DbContextOptions<FamilyTreeContext> GetFamilyTreeContextInMemoryOptions()
        {
            return new DbContextOptionsBuilder<FamilyTreeContext>().UseInMemoryDatabase(typeof(AssemblyInitializer).AssemblyQualifiedName).Options;
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            using (var context = new FamilyTreeContext(GetFamilyTreeContextInMemoryOptions()))
            {
                context.PersonList.AddRange(_personList);
                context.CoupleList.AddRange(_coupleList);
                context.ChildList.AddRange(_childList);

                context.SaveChanges();
            }
        }

        private static List<Person> _personList = new List<Person>
        {
            new Person { PersonId = 1,  Name = "King Shan", GenderId = Gender.Male },
            new Person { PersonId = 2,  Name = "Queen Anga", GenderId = Gender.Female },

            new Person { PersonId = 3,  Name = "Ish", GenderId = Gender.Male },
            new Person { PersonId = 4,  Name = "Chit", GenderId = Gender.Male },
            new Person { PersonId = 5,  Name = "Ambi", GenderId = Gender.Female },
            new Person { PersonId = 6,  Name = "Vich", GenderId = Gender.Male },
            new Person { PersonId = 7,  Name = "Lika", GenderId = Gender.Female },
            new Person { PersonId = 8,  Name = "Satya", GenderId = Gender.Female },
            new Person { PersonId = 9,  Name = "Vyan", GenderId = Gender.Male },

            new Person { PersonId = 10,  Name = "Drita", GenderId = Gender.Male },
            new Person { PersonId = 11,  Name = "Jaya", GenderId = Gender.Female },
            new Person { PersonId = 12,  Name = "Vrita", GenderId = Gender.Male },
            new Person { PersonId = 13,  Name = "Vila", GenderId = Gender.Male },
            new Person { PersonId = 14,  Name = "Jnki", GenderId = Gender.Female },
            new Person { PersonId = 15,  Name = "Chika", GenderId = Gender.Female },
            new Person { PersonId = 16,  Name = "Kpila", GenderId = Gender.Male },
            new Person { PersonId = 17,  Name = "Satvy", GenderId = Gender.Female },
            new Person { PersonId = 18,  Name = "Asva", GenderId = Gender.Male },
            new Person { PersonId = 19,  Name = "Savya", GenderId = Gender.Male },
            new Person { PersonId = 20,  Name = "Krpi", GenderId = Gender.Female },
            new Person { PersonId = 21,  Name = "Saayan", GenderId = Gender.Male },
            new Person { PersonId = 22,  Name = "Mina", GenderId = Gender.Female },

            new Person { PersonId = 23,  Name = "Jata", GenderId = Gender.Male },
            new Person { PersonId = 24,  Name = "Driya", GenderId = Gender.Female },
            new Person { PersonId = 25,  Name = "Mnu", GenderId = Gender.Male },
            new Person { PersonId = 26,  Name = "Lavnya", GenderId = Gender.Female },
            new Person { PersonId = 27,  Name = "Gru", GenderId = Gender.Male },
            new Person { PersonId = 28,  Name = "Kriya", GenderId = Gender.Male },
            new Person { PersonId = 29,  Name = "Misa", GenderId = Gender.Male }
        };

        private static List<Couple> _coupleList = new List<Couple>
        {
            new Couple { CoupleId = 101, PersonIdOfPartner1 = 1, PersonIdOfPartner2 = 2 },

            new Couple { CoupleId = 102, PersonIdOfPartner1 = 4, PersonIdOfPartner2 = 5 },
            new Couple { CoupleId = 103, PersonIdOfPartner1 = 6, PersonIdOfPartner2 = 7 },
            new Couple { CoupleId = 104, PersonIdOfPartner1 = 8, PersonIdOfPartner2 = 9 },

            new Couple { CoupleId = 105, PersonIdOfPartner1 = 10, PersonIdOfPartner2 = 11 },
            new Couple { CoupleId = 106, PersonIdOfPartner1 = 13, PersonIdOfPartner2 = 14 },
            new Couple { CoupleId = 107, PersonIdOfPartner1 = 15, PersonIdOfPartner2 = 16 },
            new Couple { CoupleId = 108, PersonIdOfPartner1 = 17, PersonIdOfPartner2 = 18 },
            new Couple { CoupleId = 109, PersonIdOfPartner1 = 19, PersonIdOfPartner2 = 20 },
            new Couple { CoupleId = 110, PersonIdOfPartner1 = 21, PersonIdOfPartner2 = 22 },

            new Couple { CoupleId = 111, PersonIdOfPartner1 = 24, PersonIdOfPartner2 = 25 },
            new Couple { CoupleId = 112, PersonIdOfPartner1 = 26, PersonIdOfPartner2 = 27 }
        };

        private static List<Child> _childList = new List<Child>
        {
            new Child { ChildId = 201, CoupleId = 101, PersonId = 3 },
            new Child { ChildId = 202, CoupleId = 101, PersonId = 4 },
            new Child { ChildId = 203, CoupleId = 101, PersonId = 6 },
            new Child { ChildId = 204, CoupleId = 101, PersonId = 8 },

            new Child { ChildId = 205, CoupleId = 102, PersonId = 10 },
            new Child { ChildId = 206, CoupleId = 102, PersonId = 12 },
            new Child { ChildId = 207, CoupleId = 103, PersonId = 13 },
            new Child { ChildId = 208, CoupleId = 103, PersonId = 15 },
            new Child { ChildId = 209, CoupleId = 104, PersonId = 17 },
            new Child { ChildId = 210, CoupleId = 104, PersonId = 19 },
            new Child { ChildId = 211, CoupleId = 104, PersonId = 21 },

            new Child { ChildId = 212, CoupleId = 105, PersonId = 23 },
            new Child { ChildId = 213, CoupleId = 105, PersonId = 24 },
            new Child { ChildId = 214, CoupleId = 106, PersonId = 26 },
            new Child { ChildId = 215, CoupleId = 109, PersonId = 28 },
            new Child { ChildId = 216, CoupleId = 110, PersonId = 29 }
        };
    }
}
