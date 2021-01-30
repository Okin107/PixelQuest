using IdleHeroesDAL.Enums;
using System.Collections.Generic;

namespace IdleHeroes.Models
{
    public static class HeroSkills
    {
        public static List<ProfileSkillTypeEnum> List
        {
            get 
            {
                List.Add(ProfileSkillTypeEnum.DPS);
                List.Add(ProfileSkillTypeEnum.HP);
                List.Add(ProfileSkillTypeEnum.Armor);
                List.Add(ProfileSkillTypeEnum.Accuracy);
                List.Add(ProfileSkillTypeEnum.Agility);
                return List; 
            }
        }
    }
}
