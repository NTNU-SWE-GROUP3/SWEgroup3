using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ResultAnimation
{

    public class AnimationController : MonoBehaviour
    {
        [SerializeField] GameObject GachaResult1;
        [SerializeField] GameObject GachaResult10;
        [SerializeField] Image resultImage;
        [SerializeField] Sprite coinSprite;
        [SerializeField] Sprite skillSprite;
        [SerializeField] Sprite cardStyleSprite;


        public void DisplayCardResult(string resultType)
        {
            Debug.Log("check: " + resultType);
            switch (resultType)
            {
                case "coins":
                    resultImage.sprite = coinSprite;
                    break;
                case "skill":
                    resultImage.sprite = skillSprite;
                    break;
                case "card_style":
                    resultImage.sprite = cardStyleSprite;
                    break;
                default:
                    break;
            }
        }


        



    }

}