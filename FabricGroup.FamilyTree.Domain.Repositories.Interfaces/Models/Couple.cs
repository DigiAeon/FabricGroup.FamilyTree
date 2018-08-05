namespace FabricGroup.FamilyTree.Domain.Repositories.Interfaces.Models
{
    public class Couple
    {
        public int CoupleId { get; set; }
        public int PersonIdOfPartner1 { get; set; }
        public int PersonIdOfPartner2 { get; set; }
    }
}
