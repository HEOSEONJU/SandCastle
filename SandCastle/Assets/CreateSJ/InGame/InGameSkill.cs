
using UnityEngine;
using SkillEnums;

using System.Linq;
using Skill;
namespace InGame
{
    public class InGameSkill : MonoBehaviour
    {
        
        
        [SerializeField]
        InGame_Char inGameChar;
        [SerializeField]
        InGameSkillSensor inGameSkillSensor;

        SkillData skillData;
        [SerializeField]
        SkillObject skillPrefab;



        Transform target;

        [SerializeField]
        Transform spwanposi;
        [SerializeField]
        Transform poolingParent;

        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }
        public void Init(InGameSkillSensor sensor,Transform parent,ObjectTable skilltable,string skillname)
        {
            inGameSkillSensor= sensor;
            poolingParent= parent;
            skillData = new SkillData();
            
            skillData.InitData(skillname, skilltable);
            
            
            
            Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey).TryGetComponent<SkillObject>(out skillPrefab);
            


            //ActiveSkill();
        }

        public bool SettingTarget(SkillData skillData=null)
        {
            if(inGameSkillSensor.GameObjects.Count==0)
            {
                return false;
            }
            if(skillData==null)
            {
                skillData = this.skillData;
            }

            switch (skillData.Target)
            {
                case SkillTarget.Near:
                    inGameSkillSensor.GameObjects =inGameSkillSensor.GameObjects.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();

                    Target = inGameSkillSensor.GameObjects.First().transform;

                    break;
                case SkillTarget.Random:
                    Target = inGameSkillSensor.GameObjects[UnityEngine.Random.Range(0, inGameSkillSensor.GameObjects.Count)].transform;

                    break;
                case SkillTarget.Far:
                    inGameSkillSensor.GameObjects = inGameSkillSensor.GameObjects.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
                    Target = inGameSkillSensor.GameObjects.Last().transform;

                    break;
                case SkillTarget.None:
                    Target = null;

                    break;
            }
            return true;
        }


        public void ActiveSkill(SkillObject skill=null)
        {

            if(skill == null)
            {
                skill=ObjectPooling.Instance.GetObject(skillPrefab.gameObject, poolingParent).GetComponent<SkillObject>();

                skill.Init(skillData);
            }
            
            
            
            
            switch (skill.SkillData.Spwan)
            {
                case SkillSpwan.Player:

                    if (Target == null)
                    {
                        return;
                    }
                    Debug.Log("스킬생성위치");
                    skill.Active(spwanposi, target);
                    break;
                case SkillSpwan.Target:

                    if (Target == null)
                    {
                        return;
                    }
                    skill.Active(target, target);
                    break;
                case SkillSpwan.Position:

                    break;
                case SkillSpwan.Trace:
                    Debug.Log("추적형");
                    skill.Active(spwanposi, target);
                    break;

            }
            //e.GetComponent<SkillObject>().Active()
            /*
            if (bulletobject is null)
            {
                return;
            }
            
            bulletobject.transform.position = attackPoint.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletobject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            bulletobject.DamagePoint = status.GiveDamage;
            bulletobject.Move();
            CanAttack = false;
            StartCoroutine(Delay());
            */
        }





    }

}