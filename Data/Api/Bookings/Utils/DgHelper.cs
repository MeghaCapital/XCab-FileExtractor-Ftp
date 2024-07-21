namespace Data.Api.Bookings.Utils
{
    public static class DgHelper
    {
        public static string GetDgClassName(DgClass? dgClass, bool retrieveSubClass = false)
        {
            var classTag = "Class";
            if (retrieveSubClass)
            {
                classTag = "SubCls";
            }
            var dgClassName = string.Empty;
            switch (dgClass)
            {
                case DgClass.Corrosive_8:
                    dgClassName = classTag + " 8";
                    break;

                case DgClass.DangerousWhenWet_4_3:
                    dgClassName = classTag + " 4.3";
                    break;

                case DgClass.Explosives_1_1:
                    dgClassName = classTag + " 1.1";
                    break;
                case DgClass.Explosives_1_2:
                    dgClassName = classTag + " 1.2";
                    break;
                case DgClass.Explosives_1_3:
                    dgClassName = classTag + " 1.3";
                    break;
                case DgClass.Explosives_1_4:
                    dgClassName = classTag + " 1.4";
                    break;
                case DgClass.Explosives_1_5:
                    dgClassName = classTag + " 1.5";
                    break;
                case DgClass.Explosives_1_6:
                    dgClassName = classTag + " 1.6";
                    break;
                case DgClass.FlammableAerosols_2_1:
                    dgClassName = classTag + " 2.1";
                    break;
                case DgClass.FlammableGasCylinders_2_1:
                    dgClassName = classTag + " 2.1";
                    break;
                case DgClass.FlammableLiquid_3:
                    dgClassName = classTag + " 3";
                    break;
                case DgClass.FlammableSolid_4_1:
                    dgClassName = classTag + " 4.1";
                    break;
                case DgClass.MiscellaneousDangerousGoods_9:
                    dgClassName = classTag + " 9";
                    break;
                case DgClass.NonFlammableNonToxicGas_2_2:
                    dgClassName = classTag + " 2.2";
                    break;
                case DgClass.NotApplicable:
                    dgClassName = "";
                    break;
                case DgClass.OrganicPeroxide_5_2:
                    dgClassName = classTag + " 5.2";
                    break;
                case DgClass.OxidizingAgent_5_1:
                    dgClassName = classTag + " 5.1";
                    break;
                case DgClass.OxidizingGas_2_2:
                    dgClassName = classTag + " 2.2";
                    break;
                case DgClass.OxidizingGas_5_1:
                    dgClassName = classTag + " 5.1";
                    break;
                case DgClass.RadioactiveMaterials_7:
                    dgClassName = classTag + " 7";
                    break;
                case DgClass.SpontaneouslyCombustible_4_2:
                    dgClassName = classTag + " 4.2";
                    break;
                case DgClass.ToxicGas_2_3:
                    dgClassName = classTag + " 2.3";
                    break;
                case DgClass.ToxicInfectious_6_1:
                    dgClassName = classTag + " 6.1";
                    break;
                case DgClass.ToxicInfectious_6_2:
                    dgClassName = classTag + " 6.2";
                    break;


            }
            return dgClassName;
        }

        public static string GetPackagingGroupName(PackagingGroup? packagingGroup)
        {
            var packagingGroupName = "";
            switch (packagingGroup)
            {
                case PackagingGroup.None:
                    packagingGroupName = "";
                    break;
                case PackagingGroup.One:
                    packagingGroupName = "Pack Grp 1";
                    break;
                case PackagingGroup.Two:
                    packagingGroupName = "Pack Grp 2";
                    break;
                case PackagingGroup.Three:
                    packagingGroupName = "Pack Grp 3";
                    break;
            }
            return packagingGroupName;
        }
    }
}