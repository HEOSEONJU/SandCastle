using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandEnums
    {
    public class Enums : MonoBehaviour
    {
        
    }

    public enum PriceType
    {
        None,
        AD,
        jewel
    }

}
namespace InGameResourceEnums
{
    public enum ResourceEnum
    {
        sand,
        water,
        mud
    }

}
namespace Roulette
{
    public enum GetCount
    {
        Single,
        Multi
    }
}


namespace SkillEnums
{
    public enum SkillPattern
    {
        Straight,
        Bounce,
        Wave,
        Spin
    }
    public enum SkillSpwan
    {
        Player,
        Target,
        Position,
        
    }
    public enum SkillTarget
    {
        Near,
        Random,
        Far,

    }
    public enum SkillTiming
    {
        Enter,
        Stay,
        Exit,

    }


}