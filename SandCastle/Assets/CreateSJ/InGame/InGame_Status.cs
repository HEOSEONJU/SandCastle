using UnityEngine;

namespace InGame
{
    public class InGame_Status : MonoBehaviour
    {
        [Header("Status")]
        int currenthp;
        int maxHp;

        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float animationSpeed;
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






        public int CurrentHp
        {
            get { return currenthp; }
        }
        public float MoveSpeed
        {
            get { return moveSpeed; }

        }
        public float AnimationSpeed
        {
            get { return animationSpeed; }

        }
        public float GiveDamage
        {
            get { return giveDamage; }

        }
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
        


        public void Init(float movespeed, float animationspeed, float givedamage, float sandget, float waterget, float mudget, float range, int maxmana, int startmana,int maxhp)
        {
            moveSpeed = movespeed;
            animationSpeed = animationspeed;
            giveDamage = givedamage;
            sandGet = sandget;
            waterGet = waterget;
            mudGet= mudget;
            this.range = range;
            currentMana = startmana;
            maxMana = maxmana;
            currenthp = maxHp = maxhp;
            
            if (range==0)
            {
                search.Collider2d.enabled= false;
            }
            else

            search.Collider2d.radius = range;

        }
    }
}