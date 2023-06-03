
using inGame;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace InGame
{
    public class InGameAttack : MonoBehaviour
    {
        [SerializeField]
        InGameEnemySearch inGameEnemySearch;

        [SerializeField]
        Abstract_Attack abstractAttack;


        [Header("무기각도조정값")]
        [SerializeField]
        int value=-180;//

        [SerializeField]
        Animator animatorWeapon;
        [SerializeField]
        Animator particleWeapon;
        private void Update()
        {
            if (inGameEnemySearch.Target.Count != 0)
            {
                Vector2 T = new Vector2(transform.position.x - inGameEnemySearch.Target[0].transform.position.x, transform.position.y - inGameEnemySearch.Target[0].transform.position.y);
                float angle = Mathf.Atan2(T.y,T.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle+value, Vector3.forward);
                
            }
            
        }

        public void ResetAngle()
        {
            transform.rotation = Quaternion.identity;
            
        }

        public void ChangeAttack(Abstract_Attack abstractattack)
        {
            abstractAttack = abstractattack;
        }

        public void PlayAttack()
        {
            if (!abstractAttack.Require() || inGameEnemySearch.Target.Count == 0)
            {
                return;
            }
            animatorWeapon.SetTrigger("Fire");
            particleWeapon.SetTrigger("Fire");

        }

        public void EvnetAttack()
        {
            //Vector3 direction = inGameEnemySearch.Target[0].transform.position - transform.position;
            abstractAttack.Attack(inGameEnemySearch.Target, -transform.right);
        }




    }
}