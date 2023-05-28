using UnityEngine;

namespace InGame
{
    public class InGame_Status : MonoBehaviour
    {
        [Header("Status")]
         float moveSpeed;
         float animationSpeed;
         float giveDamage;
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
        int maxMana;
        [SerializeField]
        int currentMana;


        private void Start()
        {
            MoveSpeed = 1f;
            AnimationSpeed = 1f;
            GiveDamage = 1f;
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value;}
        }
        public float AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }
        public float GiveDamage
        {
            get { return giveDamage; }
            set { giveDamage = value; }
        }
        public int CurrentMana
        {
            get { return currentMana; }
            set { currentMana = value; }
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
    }
}