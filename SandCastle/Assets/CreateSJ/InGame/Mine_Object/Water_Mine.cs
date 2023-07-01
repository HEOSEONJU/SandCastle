using InGameResourceEnums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace InGame
{
    public class Water_Mine : Base_Mine
    {








        IEnumerator Ani;

        [SerializeField]
        float delay=0.3f;

        public override void Init_Object(string type, int amount, float maxhp, int amountmax)
        {
            base.Init_Object(type,amount,maxhp,amountmax);
            Ani = SwapImage();
            StartCoroutine(Ani);
        }
        
        public void OnDisable()
        {
            StopCoroutine(Ani);
        }


        IEnumerator SwapImage()
        {
            while(true) 
            {
                if(sprites==null || sprites.Count==0)
                {
                    Debug.Log("x종료");
                    yield break;
                }


                Debug.Log(State+gameObject.name);

                switch (State)
                {
                    case ResourceState.Full:
                        mainImage.sprite = sprites[0];
                        
                        break;
                    case ResourceState.Half:
                        mainImage.sprite = sprites[2];
                        
                        break;
                    case ResourceState.Dead:
                        mainImage.sprite = sprites[4];
                        break;


                }
                yield return new WaitForSeconds(delay);




                switch (State)
                {
                    case ResourceState.Full:
                        mainImage.sprite = sprites[1];
                        break;
                    case ResourceState.Half:
                        mainImage.sprite = sprites[3];
                        break;
                    case ResourceState.Dead:
                        mainImage.sprite = sprites[5];
                        break;


                }
                yield return new WaitForSeconds(delay);
                Debug.Log("종료");
            }
            

        }



        public override void Change_Image()
        {
            return;
        }

    }
}