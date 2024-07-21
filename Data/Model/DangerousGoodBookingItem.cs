namespace Data.Model
{
    public class DangerousGoodBookingItem
    {
        /// <summary>
        /// Number of Items
        /// </summary>
        public int? NumberOfItems { get; set; }

        /// <summary>
        /// Dangerous Goods Class
        /// </summary>
        public RequestDgClass? DgClass { get; set; }

        /// <summary>
        /// Number of units or volume
        /// </summary>
        public int? UnitWtOrVol { get; set; }

        /// <summary>
        /// Type of the unit
        /// </summary>
        public RequestUnitType? UnitType { get; set; }

        /// <summary>
        /// Group of the packaging
        /// </summary>
        public RequestPackagingGroup? PackagingGroup { get; set; }

        /// <summary>
        /// Subsidiary risk class of the items
        /// </summary>
        public RequestDgClass? SubsidiaryRiskClass { get; set; }
    }

    public enum RequestUnitType
    {
        Kg,
        L

    }
    public enum RequestPackagingGroup
    {
        None,
        One,
        Two,
        Three
    }

    public enum RequestDgClass
    {
        NotApplicable,
        FlammableGasCylinders_2_1,
        FlammableAerosols_2_1,
        NonFlammableNonToxicGas_2_2,
        OxidizingGas_2_2,
        OxidizingGas_5_1,
        FlammableLiquid_3,
        OxidizingAgent_5_1,
        Corrosive_8,
        MiscellaneousDangerousGoods_9,
        FlammableSolid_4_1,
        SpontaneouslyCombustible_4_2,
        ToxicInfectious_6_1,
        ToxicInfectious_6_2,
        RadioactiveMaterials_7,
        Explosives_1_1,
        Explosives_1_2,
        Explosives_1_3,
        Explosives_1_4,
        Explosives_1_5,
        Explosives_1_6,
        ToxicGas_2_3,
        DangerousWhenWet_4_3,
        OrganicPeroxide_5_2
    }
}