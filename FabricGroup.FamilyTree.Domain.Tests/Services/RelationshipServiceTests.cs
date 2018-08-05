using FabricGroup.FamilyTree.Domain.Services;
using FabricGroup.FamilyTree.Domain.Services.Interfaces;
using FabricGroup.FamilyTree.Domain.Services.Models;
using FabricGroup.FamilyTree.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FabricGroup.FamilyTree.Domain.Tests.Services
{
    [TestClass]
    public class RelationshipServiceTests
    {
        private static FamilyTreeContext _familyTreeContext;
        private static IRelationshipService _relationshipService;
        

        private static FamilyTreeContext GetFamilyTreeContext()
        {
            return new FamilyTreeContext(AssemblyInitializer.GetFamilyTreeContextInMemoryOptions());
        }

        private static IRelationshipService GetRelationshipService(FamilyTreeContext familyTreeContext)
        {
            return new RelationshipService(new RelationshipReadRepository(familyTreeContext), new RelationshipWriteRepository(familyTreeContext));
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _familyTreeContext = GetFamilyTreeContext();
            _relationshipService = GetRelationshipService(_familyTreeContext);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            try
            {
                if (_familyTreeContext != null)
                {
                    _familyTreeContext.Dispose();
                }
            }
            catch { }
        }

        [TestMethod]
        public void GIVEN_invalid_name_WHEN_relative_is_searched_THEN_system_should_return_valid_output()
        {
            var result = _relationshipService.FindRelatives(Relationships.Father, "XYZ");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [DataTestMethod]
        [DynamicData(nameof(FindRelativesTestCases), DynamicDataSourceType.Method)]
        public void GIVEN_valid_name_WHEN_relative_is_searched_THEN_system_should_return_expected_output(Relationships relationship, string personName, List<string> expectedResult)
        {
            var result = _relationshipService.FindRelatives(relationship, personName);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First().Relatives.Count, expectedResult.Count);

            if (result.Count > 0)
            {
                var relatives = string.Join("\n",
                        result.First().Relatives.OrderBy(x => x.Name).Select(x => x.Name).ToList());

                var expectedRelatives = string.Join("\n",
                        expectedResult.OrderBy(x => x).ToList());

                Assert.IsTrue(string.Compare(relatives, expectedRelatives, true) == 0);
            }
        }

        private static IEnumerable<object[]> FindRelativesTestCases()
        {
            var testCases = new List<object[]>();

            testCases.AddRange(FindRelativesTestCases_Father());
            testCases.AddRange(FindRelativesTestCases_Mother());

            testCases.AddRange(FindRelativesTestCases_Brothers());
            testCases.AddRange(FindRelativesTestCases_Sisters());

            testCases.AddRange(FindRelativesTestCases_Sons());
            testCases.AddRange(FindRelativesTestCases_Daughters());
            testCases.AddRange(FindRelativesTestCases_Children());

            testCases.AddRange(FindRelativesTestCases_GrandDaughters());
            testCases.AddRange(FindRelativesTestCases_GrandSons());
            testCases.AddRange(FindRelativesTestCases_GrandChildren());

            testCases.AddRange(FindRelativesTestCases_MaternalUncles());
            testCases.AddRange(FindRelativesTestCases_MaternalAunts());

            testCases.AddRange(FindRelativesTestCases_PaternalUncles());
            testCases.AddRange(FindRelativesTestCases_PaternalAunts());

            testCases.AddRange(FindRelativesTestCases_BrotherInLaws());
            testCases.AddRange(FindRelativesTestCases_SisterInLaws());

            testCases.AddRange(FindRelativesTestCases_Cousins());

            return testCases;
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Father()
        {
            yield return new object[]
            {
                Relationships.Father, "Ish", new List<string> { "King Shan" }
            };

            yield return new object[]
            {
                Relationships.Father, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Father, "Lavnya", new List<string> { "Vila" }
            };

            yield return new object[]
            {
                Relationships.Father, "Satvy", new List<string> { "Vyan" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Mother()
        {
            yield return new object[]
            {
                Relationships.Mother, "Chit", new List<string> { "Queen Anga" }
            };

            yield return new object[]
            {
                Relationships.Mother, "Queen Anga", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Mother, "Lavnya", new List<string> { "Jnki" }
            };

            yield return new object[]
            {
                Relationships.Mother, "Vrita", new List<string> { "Ambi" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Brothers()
        {
            yield return new object[]
            {
                Relationships.Brother, "Chit", new List<string> { "Ish", "Vich" }
            };

            yield return new object[]
            {
                Relationships.Brother, "Vyan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Brother, "Satvy", new List<string> { "Savya", "Saayan" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Sisters()
        {
            yield return new object[]
            {
                Relationships.Sister, "Satya", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Sister, "Saayan", new List<string> { "Satvy" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Sons()
        {
            yield return new object[]
            {
                Relationships.Son, "King Shan", new List<string> { "Ish", "Chit", "Vich" }
            };

            yield return new object[]
            {
                Relationships.Son, "Kriya", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Son, "Vila", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Son, "Vyan", new List<string> { "Savya", "Saayan" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Daughters()
        {
            yield return new object[]
            {
                Relationships.Daughter, "King Shan", new List<string> { "Satya" }
            };

            yield return new object[]
            {
                Relationships.Daughter, "Queen Anga", new List<string> { "Satya" }
            };

            yield return new object[]
            {
                Relationships.Daughter, "Ish", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Daughter, "Lika", new List<string> { "Chika" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Children()
        {
            yield return new object[]
            {
                Relationships.Children, "King Shan", new List<string> { "Ish", "Chit", "Vich", "Satya" }
            };

            yield return new object[]
            {
                Relationships.Children, "Queen Anga", new List<string> { "Ish", "Chit", "Vich", "Satya" }
            };

            yield return new object[]
            {
                Relationships.Children, "Ish", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Children, "Jaya", new List<string> { "Jata", "Driya" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_GrandDaughters()
        {
            yield return new object[]
            {
                Relationships.GrandDaughter, "King Shan", new List<string> { "Chika", "Satvy" }
            };

            yield return new object[]
            {
                Relationships.GrandDaughter, "Queen Anga", new List<string> { "Chika", "Satvy" }
            };

            yield return new object[]
            {
                Relationships.GrandDaughter, "Ish", new List<string>()
            };

            yield return new object[]
            {
                Relationships.GrandDaughter, "Ambi", new List<string> { "Driya" }
            };

            yield return new object[]
            {
                Relationships.GrandDaughter, "Lika", new List<string> { "Lavnya" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_GrandSons()
        {
            yield return new object[]
            {
                Relationships.GrandSon, "King Shan", new List<string> { "Drita", "Vrita", "Vila", "Savya", "Saayan" }
            };

            yield return new object[]
            {
                Relationships.GrandSon, "Vich", new List<string>()
            };

            yield return new object[]
            {
                Relationships.GrandSon, "Vyan", new List<string> { "Kriya", "Misa" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_GrandChildren()
        {
            yield return new object[]
            {
                Relationships.GrandChildren, "King Shan", new List<string> { "Drita", "Vrita", "Vila", "Savya", "Saayan", "Chika", "Satvy" }
            };

            yield return new object[]
            {
                Relationships.GrandChildren, "Vich", new List<string> { "Lavnya" }
            };

            yield return new object[]
            {
                Relationships.GrandChildren, "Satya", new List<string> { "Kriya", "Misa" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_MaternalUncles()
        {
            yield return new object[]
            {
                Relationships.MaternalUncle, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.MaternalUncle, "Satvy", new List<string> { "Vich", "Chit", "Ish" }
            };

            yield return new object[]
            {
                Relationships.MaternalUncle, "Chika", new List<string>()
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_MaternalAunts()
        {
            yield return new object[]
            {
                Relationships.MaternalAunt, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.MaternalAunt, "Satvy", new List<string> { "Lika", "Ambi" }
            };

            yield return new object[]
            {
                Relationships.MaternalAunt, "Chika", new List<string>()
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_PaternalUncles()
        {
            yield return new object[]
            {
                Relationships.PaternalUncle, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.PaternalUncle, "Satvy", new List<string>()
            };

            yield return new object[]
            {
                Relationships.PaternalUncle, "Vila", new List<string> { "Ish", "Chit", "Vyan" }
            };

            yield return new object[]
            {
                Relationships.PaternalUncle, "Kriya", new List<string> { "Asva", "Saayan" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_PaternalAunts()
        {
            yield return new object[]
            {
                Relationships.PaternalAunt, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.PaternalAunt, "Chika", new List<string> { "Ambi", "Satya" }
            };

            yield return new object[]
            {
                Relationships.PaternalAunt, "Kriya", new List<string> { "Satvy", "Mina" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_SisterInLaws()
        {
            yield return new object[]
            {
                Relationships.SisterInLaw, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.SisterInLaw, "Lika", new List<string> { "Satya" }
            };

            yield return new object[]
            {
                Relationships.SisterInLaw, "Jnki", new List<string> { "Chika" }
            };

            yield return new object[]
            {
                Relationships.SisterInLaw, "Satvy", new List<string> { "Krpi", "Mina" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_BrotherInLaws()
        {
            yield return new object[]
            {
                Relationships.BrotherInLaw, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.BrotherInLaw, "Vyan", new List<string> { "Ish", "Chit", "Vich" }
            };

            yield return new object[]
            {
                Relationships.BrotherInLaw, "Chika", new List<string>()
            };

            yield return new object[]
            {
                Relationships.BrotherInLaw, "Krpi", new List<string> { "Saayan" }
            };
        }

        private static IEnumerable<object[]> FindRelativesTestCases_Cousins()
        {
            yield return new object[]
            {
                Relationships.Cousin, "King Shan", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Cousin, "Chika", new List<string> { "Drita", "Vrita", "Satvy", "Savya", "Saayan" }
            };

            yield return new object[]
            {
                Relationships.Cousin, "Vich", new List<string>()
            };

            yield return new object[]
            {
                Relationships.Cousin, "Kriya", new List<string> { "Misa" }
            };
        }
    }
}
