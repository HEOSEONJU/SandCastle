using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace InGame
{
    public class InGameMineSearch : MonoBehaviour
    {
        [SerializeField]
        Animator animator;

        [SerializeField]
        InGame_Harvest harvest;
        [SerializeField]
        Abstract_Mine target;

        [SerializeField]
        float searchRange;

        [SerializeField]
        CircleCollider2D collider2d;
        public Abstract_Mine Target { get { return target; } }



        private void OnEnable()
        {
            target = null;
            collider2d.radius = searchRange;
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (animator.GetBool("RecallEnd"))
            {
                return;
            }

            if (collision.CompareTag("Mine"))
            {
                collision.TryGetComponent<Abstract_Mine>(out  target);
                if (!(target is null))
                {
                    target.ConnectList.Add(this.gameObject);
                    
                    harvest.Harvest();
                        
                    

                    
                    
                    
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Mine"))
            {
                collision.TryGetComponent<Abstract_Mine>(out target);
                if (!(target is null))
                {
                    target.ConnectList.Remove(this.gameObject);
                    if (target.ConnectList.Count == 0)
                    {
                        //harvest.Do = false;
                    }




                }
                
            }
        }

    }
}