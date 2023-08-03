using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkillEnums;
using System;
using Google.GData.Extensions;


namespace Skill
{


    [System.Serializable]
    public class SkillData
    {
        [SerializeField]
        string skillObjectKey;
        [SerializeField]
        SkillSpwan spwan;
        [SerializeField]
        SkillTarget target;
        


        
        [SerializeField]
        SkillTiming applyDamageTiming;
        [SerializeField]
        float damage;
        [SerializeField]
        float damageTime;//데미지간격?


        [SerializeField]
        int isPiercing;

        [SerializeField]
        float duration;
        float speed;
        
        [SerializeField]
        float damageDelay;
        [SerializeField]
        int bulletCount;
        [SerializeField]
        float delay;

        public string SkillObjectKey
        {
            get { return skillObjectKey; }
        }
        public SkillTiming ApplyDamageTiming
        {
            get { return applyDamageTiming; }
        }
        public float Speed
        {
            get { return speed; }
        }
        public float Duration
        {
            get { return duration; }
        }
        public SkillSpwan Spwan
        {
            get { return spwan; }
        }
        public SkillTarget Target
        {
            get { return target; }
        }
        public int IsPiercing
        {
            get { return isPiercing; }
            set { isPiercing = value; }
        }
        public float Damage
        {
            get { return damage; }
        }
        public float Delay
        {
            get { return delay; }
        }


        public void InitData(string key, ObjectTable skillTable)
        {

            skillObjectKey = skillTable.FindString(key, "skillObjectKey");


            damage = skillTable.Findfloat(key, "damage");
            damageDelay = skillTable.Findfloat(key, "damageDelay");
            isPiercing = skillTable.FindInt(key, "isPiercing");
            speed = skillTable.Findfloat(key, "speed");
            duration = skillTable.Findfloat(key, "duration");
            delay = skillTable.Findfloat(key, "delay");
            bulletCount = skillTable.FindInt(key, "bulletCount");



            switch (skillTable.FindString(key, "spawnType"))
            {
                case "Player":
                    spwan = SkillSpwan.Player;
                    break;
                case "Target":
                    spwan = SkillSpwan.Target;
                    break;
                case "Position":
                    spwan = SkillSpwan.Position;
                    break;
            }
            
            



            switch (skillTable.FindString(key, "targetType"))
            {
                case "Near":
                    target = SkillTarget.Near;
                    break;
                case "Random":
                    target = SkillTarget.Random;
                    break;
                case "Far":
                    target = SkillTarget.Far;
                    break;
            }

            switch (skillTable.FindString(key, "applyDamageTiming"))
            {
                case "Enter":
                    applyDamageTiming = SkillTiming.Enter;
                    break;
                case "Stay":
                    applyDamageTiming = SkillTiming.Stay;
                    break;
                case "Exit":
                    applyDamageTiming = SkillTiming.Exit;
                    break;
            }


        }

        public void LevelUP(string key, ObjectTable skillTable)
        {
            skillObjectKey = skillTable.FindString(key, "skillObjectKey");


            damage = skillTable.Findfloat(key, "damage");
            damageDelay = skillTable.Findfloat(key, "damageDelay");
            isPiercing = skillTable.FindInt(key, "isPiercing");
            speed = skillTable.Findfloat(key, "speed");
            duration = skillTable.Findfloat(key, "duration");
            delay = skillTable.Findfloat(key, "delay");
            bulletCount = skillTable.FindInt(key, "bulletCount");
        }
        public SkillData Clone()
        {
            return (this.MemberwiseClone() as SkillData);
        }
    }


}