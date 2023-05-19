
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


        Transform lastTargetDirection;


        public void ChangeAttack(Abstract_Attack abstractattack)
        {
            abstractAttack = abstractattack;
        }

        public void PlayAttack(Animator animator)
        {
            if (!abstractAttack.Require() || inGameEnemySearch.Target.Count == 0)
            {
                return;
            }
            if(animator.GetBool("Attack"))
            {
                return;
            }
            if (inGameEnemySearch.Target.Count >= 2)
            {
                inGameEnemySearch.Target.Sort((a, b) => (a.transform.position - transform.position).magnitude.CompareTo((b.transform.position - transform.position).magnitude));
            }
            lastTargetDirection = inGameEnemySearch.Target[0].transform;
            
            
            animator.SetBool("Attack",true);
        }


        public void Attack(Animator animator)
        {

            if(lastTargetDirection is null && inGameEnemySearch.Target.Count==0)
            {
                animator.SetBool("Attack", false);
                return;
            }
           else if (lastTargetDirection is null)
            {
                lastTargetDirection = inGameEnemySearch.Target[0].transform;
            }

            Vector3 direction  = lastTargetDirection.position - transform.position;
            abstractAttack.Attack(inGameEnemySearch.Target, direction);
            //애니메이션을실행
        }



    }
}