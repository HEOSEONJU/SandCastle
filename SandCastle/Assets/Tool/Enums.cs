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
namespace Player
{
    public enum StageState
    {
        Clerar,
        Unlock,
        Lock
    }


}


namespace Enemy
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Death
        
        

    }
}

namespace InGame
{
    public enum PlayerState 
    {
        Idle,
        Move,
        Skill,
        Harvest,
        Death

    }

    public enum AttackType
    {
        Single = 1,
        Multi = 2,
        Range = 4,
        Continuous = 8,
        Penetrating = 16
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
    public enum ResourceState
    {
        Full=1,
        Half=2,
        Dead=4
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
        Spin,
        Stop

    }
    public enum SkillSpwan
    {
        Player,
        Target,
        Position,
        Trace
    }
    public enum SkillTarget
    {
        Near,
        Random,
        Far,
        None

    }
    public enum SkillTiming
    {
        Enter,
        Stay,
        Exit,

    }
    public enum BuffType
    {
        exp


    }

}