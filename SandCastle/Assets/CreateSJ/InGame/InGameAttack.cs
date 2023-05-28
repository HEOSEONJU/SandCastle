
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




        private void Update()
        {
            if (inGameEnemySearch.Target.Count != 0)
            {
                float angle = Mathf.Atan2(inGameEnemySearch.Target[0].transform.position.y, inGameEnemySearch.Target[0].transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.Rotate(new Vector3(0, 0, -90));
            }
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
            Vector3 direction = inGameEnemySearch.Target[0].transform.position - transform.position;
            abstractAttack.Attack(inGameEnemySearch.Target, direction);


        }






    }
}