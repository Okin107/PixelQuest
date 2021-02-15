using IdleHeroesDAL.Enums;
using System.Collections.Generic;

namespace IdleHeroes.Models
{
    public static class CompanionSettings
    {
        private static List<CompanionGrowth> _companionGrowths { get; set; } = new List<CompanionGrowth>();

        public static List<CompanionGrowth> CompanionGrowths
        {
            get
            {
                CompanionGrowth warriorGrowth = new CompanionGrowth()
                {
                    //Warrior
                    Class = CompanionClassesEnum.Warrior,
                    //HP
                    AscendHP1 = 3,
                    AscendHP2 = 4,
                    AscendHP3 = 5,
                    AscendHP4 = 6,
                    //DPS
                    AscendDPS1 = 3,
                    AscendDPS2 = 4,
                    AscendDPS3 = 4,
                    AscendDPS4 = 5,
                    //Armor
                    AscendArmor1 = 2,
                    AscendArmor2 = 2,
                    AscendArmor3 = 2,
                    AscendArmor4 = 2,
                    //Agility
                    AscendAgility1 = 2,
                    AscendAgility2 = 2,
                    AscendAgility3 = 2,
                    AscendAgility4 = 2,
                    //Accuracy
                    AscendAccuracy1 = 3,
                    AscendAccuracy2 = 3,
                    AscendAccuracy3 = 3,
                    AscendAccuracy4 = 3,
                };
                _companionGrowths.Add(warriorGrowth);

                CompanionGrowth tankGrowth = new CompanionGrowth()
                {
                    //Warrior
                    Class = CompanionClassesEnum.Tank,
                    //HP
                    AscendHP1 = 4,
                    AscendHP2 = 4,
                    AscendHP3 = 5,
                    AscendHP4 = 5,
                    //DPS
                    AscendDPS1 = 2,
                    AscendDPS2 = 2,
                    AscendDPS3 = 2,
                    AscendDPS4 = 2,
                    //Armor
                    AscendArmor1 = 4,
                    AscendArmor2 = 5,
                    AscendArmor3 = 6,
                    AscendArmor4 = 6,
                    //Agility
                    AscendAgility1 = 2,
                    AscendAgility2 = 2,
                    AscendAgility3 = 2,
                    AscendAgility4 = 2,
                    //Accuracy
                    AscendAccuracy1 = 2,
                    AscendAccuracy2 = 2,
                    AscendAccuracy3 = 2,
                    AscendAccuracy4 = 2,
                };
                _companionGrowths.Add(tankGrowth);

                CompanionGrowth rangerGrowth = new CompanionGrowth()
                {
                    //Warrior
                    Class = CompanionClassesEnum.Ranger,
                    //HP
                    AscendHP1 = 2,
                    AscendHP2 = 2,
                    AscendHP3 = 3,
                    AscendHP4 = 3,
                    //DPS
                    AscendDPS1 = 4,
                    AscendDPS2 = 5,
                    AscendDPS3 = 6,
                    AscendDPS4 = 7,
                    //Armor
                    AscendArmor1 = 2,
                    AscendArmor2 = 2,
                    AscendArmor3 = 2,
                    AscendArmor4 = 2,
                    //Agility
                    AscendAgility1 = 3,
                    AscendAgility2 = 4,
                    AscendAgility3 = 4,
                    AscendAgility4 = 5,
                    //Accuracy
                    AscendAccuracy1 = 3,
                    AscendAccuracy2 = 4,
                    AscendAccuracy3 = 4,
                    AscendAccuracy4 = 5
                };
                _companionGrowths.Add(rangerGrowth);

                CompanionGrowth assassinGrowth = new CompanionGrowth()
                {
                    //Warrior
                    Class = CompanionClassesEnum.Assassin,
                    //HP
                    AscendHP1 = 2,
                    AscendHP2 = 3,
                    AscendHP3 = 3,
                    AscendHP4 = 4,
                    //DPS
                    AscendDPS1 = 4,
                    AscendDPS2 = 5,
                    AscendDPS3 = 6,
                    AscendDPS4 = 7,
                    //Armor
                    AscendArmor1 = 2,
                    AscendArmor2 = 2,
                    AscendArmor3 = 2,
                    AscendArmor4 = 2,
                    //Agility
                    AscendAgility1 = 3,
                    AscendAgility2 = 4,
                    AscendAgility3 = 4,
                    AscendAgility4 = 5,
                    //Accuracy
                    AscendAccuracy1 = 3,
                    AscendAccuracy2 = 4,
                    AscendAccuracy3 = 4,
                    AscendAccuracy4 = 5
                };
                _companionGrowths.Add(assassinGrowth);

                return _companionGrowths;
            }
        }
    }

    public class CompanionGrowth
    {
        public CompanionClassesEnum Class { get; set; }
        //HP
        public double AscendHP1 { get; set; }
        public double AscendHP2 { get; set; }
        public double AscendHP3 { get; set; }
        public double AscendHP4 { get; set; }
        //DPS
        public double AscendDPS1 { get; set; }
        public double AscendDPS2 { get; set; }
        public double AscendDPS3 { get; set; }
        public double AscendDPS4 { get; set; }
        //Armor
        public double AscendArmor1 { get; set; }
        public double AscendArmor2 { get; set; }
        public double AscendArmor3 { get; set; }
        public double AscendArmor4 { get; set; }
        //Agility
        public double AscendAgility1 { get; set; }
        public double AscendAgility2 { get; set; }
        public double AscendAgility3 { get; set; }
        public double AscendAgility4 { get; set; }
        //Accuracy
        public double AscendAccuracy1 { get; set; }
        public double AscendAccuracy2 { get; set; }
        public double AscendAccuracy3 { get; set; }
        public double AscendAccuracy4 { get; set; }
    }
}
