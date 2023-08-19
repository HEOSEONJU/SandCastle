
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class InitCommonSkill : BasicCommonSkill
    {
        WaitForSeconds delayTime;
        protected override void Effect(float delay)
        {
            delayTime=new WaitForSeconds(delay);
            StartCoroutine(EffectCorountine(delay));
        }

        IEnumerator EffectCorountine(float delay)
        {
            for (int i = 0; i < skillData.BulletCount; i++)
            {
                
                if (skill.SettingTarget(skillData))
                {
                    ObjectPooling.Instance.GetObject(skillEffectPrefab.gameObject, transform).TryGetComponent<SkillObject>(out SkillObject bulletobject);
                    bulletobject.Init(skillData);

                    

                    for(int j= bulletobject.transform.childCount; j< skillData.Multiple;j++)
                    {
                        var e = Instantiate(bulletobject.transform.GetChild(0).gameObject, bulletobject.transform);
                        int t = j - 1;
                        float range = Mathf.Pow(-1, t) * 1 * ((t / 2)+1);
                        e.transform.position = new Vector3(range, e.transform.position.y, 0);
                    }
                    

                    skill.ActiveSkill(bulletobject);
                }
                yield return delayTime;


            }
        }

    }
}
