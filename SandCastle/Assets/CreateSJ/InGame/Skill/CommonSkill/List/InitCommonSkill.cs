
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class InitCommonSkill : BasicCommonSkill
    {

        protected override void Effect()
        {
            for (int i = 0; i < skillData.BulletCount; i++)
            {
                if (skill.SettingTarget(skillData))
                {
                    Debug.Log(skill.Target);
                    ObjectPooling.Instance.GetObject(skillEffectPrefab.gameObject, transform).TryGetComponent<SkillObject>(out SkillObject bulletobject);
                    bulletobject.Init(skillData);
                    skill.ActiveSkill(bulletobject);
                }
            }
        }

    }
}
