using System.ComponentModel;

namespace FabricGroup.FamilyTree.Domain.Services.Models
{
    public enum Relationships
    {
        [Description("Father")]
        Father = 1,

        [Description("Mother")]
        Mother,

        [Description("Brother")]
        Brother,

        [Description("Sister")]
        Sister,

        [Description("Children")]
        Children,

        [Description("Son")]
        Son,

        [Description("Daughter")]
        Daughter,

        [Description("Grand Daughter")]
        GrandDaughter,

        [Description("Grand Son")]
        GrandSon,

        [Description("Grand Children")]
        GrandChildren,

        [Description("Cousin")]
        Cousin,

        [Description("Paternal Uncle")]
        PaternalUncle,

        [Description("Maternal Uncle")]
        MaternalUncle,

        [Description("Paternal Aunt")]
        PaternalAunt,

        [Description("Maternal Aunt")]
        MaternalAunt,

        [Description("Sister-In-Law")]
        SisterInLaw,

        [Description("Brother-In-Law")]
        BrotherInLaw
    }
}
