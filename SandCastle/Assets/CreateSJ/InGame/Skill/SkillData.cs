using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkillEnums;
public class SkillData : MonoBehaviour
{
    string skillObjectKey;
    SkillSpwan spwan;
    SkillTarget target;
    int repeat;
    int repeatInterval;

    int targetType;
    bool targetAmount;//리피트할때마다 true면 재계산
    SkillTiming applyDamageTiming;
    float damage;
    float damageTime;
    string AbnormalKey;
    string chainSkillKey;
    SkillTiming chainTiming;

    float duration;
    float speed;
    int isPiercing;
}
