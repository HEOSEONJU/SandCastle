using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGameEnemySearch : MonoBehaviour
    {
        [SerializeField]
        List<Enemy_Manager> target;
        [SerializeField]
        float searchRange;

        [SerializeField]
        CircleCollider2D collider2d;
        public List<Enemy_Manager> Target { get { return target; } }

        private void OnEnable()
        {
            target = new List<Enemy_Manager>();
            collider2d.radius= searchRange;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Enemy_Manager>(out Enemy_Manager temp);
                if (!(temp is null))
                {
                    target.Add(temp);
                    

                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Enemy_Manager>(out Enemy_Manager temp);
                if (!(target is null))
                {
                    target.Remove(temp);
                    

                }
            }
        }

    }
}