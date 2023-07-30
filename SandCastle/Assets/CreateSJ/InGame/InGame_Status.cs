using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGame_Status : MonoBehaviour
    {
        int level;
        [Header("Status")]
        [SerializeField]
        int currentHp;
        [SerializeField]
        int baseHp;
        [SerializeField]
        int maxHp;

        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float animationSpeed;
        [SerializeField]
        float baseDamage;
        [SerializeField]
        int gradeDamage = 0;
        [SerializeField]
        float giveDamage;
        [SerializeField]
        float sandGet;
        [SerializeField]

        float waterGet;
        [SerializeField]
        float mudGet;
        [SerializeField]
        float range;
        [SerializeField]
        int maxMana;
        [SerializeField]
        int currentMana;
        [Header("Animation_Name")]
        [SerializeField]
        string CharcaterImage;
        [SerializeField]
        string tree;
        [SerializeField]
        string water;
        [Header("Local")]
        [SerializeField]
        string localKeyName;


        [SerializeField]
        InGameEnemySearch search;

        [SerializeField]
        int gradeHp = 0;
        
        

        [SerializeField]
        float baseCRP = 0.05f;
        [SerializeField]
        float gradeCRP = 0;
        [SerializeField]
        float currentCRP = 0f;


        [SerializeField]
        float baseCRD = 1.5f;
        
        [SerializeField]
        float currentCRD = 0f;

        int tmephp = 0;

        [SerializeField]
        float exp;
        [SerializeField]
        List<float> needExp;

        public int CurrentHp
        {
            get { return currentHp; }
        }
        public float MoveSpeed
        {
            get { return moveSpeed; }

        }
        public float AnimationSpeed
        {
            get { return animationSpeed; }

        }
        public float WaterGet
        {
            get { return waterGet; }
        }

        public float MudGet
        {
            get { return mudGet; }
        }

        public float SandGet
        {
            get { return sandGet; }
        }


        public float GiveDamage
        {
            get { return giveDamage; }

        }
        public float CurrentCRP
        { get { return currentCRP; } }

        public float CurrentCRD
        { get { return currentCRD; } }

        public int CurrentMana
        {
            get { return currentMana; }
            set { currentMana= value; }

        }
        public bool CanSkill
        {
            get
            {
                if (currentMana > maxMana)
                    return true;
                else
                    return false;
            }
        }


        

        public void Init(float movespeed, float animationspeed, float givedamage, float sandget, float waterget, float mudget, float range, int maxmana, int startmana,int maxhp,float crp,float crd)
        {
            exp = 0;
            level = 1;
            moveSpeed = movespeed;
            animationSpeed = animationspeed;
            baseDamage = giveDamage = givedamage;
            sandGet = sandget;
            waterGet = waterget;
            mudGet= mudget;
            this.range = range;
            currentMana = startmana;
            maxMana = maxmana;
            baseHp = currentHp = maxHp = maxhp;

            gradeHp = gradeDamage = 0; gradeCRP = 0f;
            baseCRP = crp;
            baseCRD = crd;

            needExp = new List<float>();

            if (range==0)
            {
                search.Collider2d.enabled= false;
            }
            else

            search.Collider2d.radius = range;
            Calculation();
        }
        public void InputLevel(List<float> needexp)
        {
            needExp = needexp;
        }

        public void Calculation()//시작할때 업그레이드 레벨 늘리는거나오면 체력 생성시 다시 리셋팅
        {
            
            maxHp = baseHp + gradeHp;
            currentHp += tmephp;
            giveDamage = baseDamage + gradeDamage;
            currentCRP=baseCRP+ gradeCRP;
            currentCRD = baseCRD;
        }

        public void GetEXP(float value)
        {
            exp += value;
            if(level<needExp.Count)
            {
                if(needExp[level-1]<=exp)
                {
                    exp -= needExp[level - 1];
                    level++;
                    Debug.Log("레벨업이벤트");
                }
            }

        }

        public void LevelUpHp(int grade)
        {
            tmephp = grade - gradeHp;
            gradeHp = grade;
            
            Calculation();

        }
        public void LevelUpDMG(int grade)
        {

            gradeDamage = grade;
            Calculation();
            Debug.Log("실적용");
        }
        public void LevelUpCRT(float grade)
        {
            gradeCRP = grade;
            Calculation();
        }
    }
}