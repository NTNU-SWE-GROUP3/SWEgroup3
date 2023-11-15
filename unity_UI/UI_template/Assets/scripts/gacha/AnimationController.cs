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
        [SerializeField] Sprite coinSprite;
        [SerializeField] Sprite skillSprite;
        [SerializeField] Sprite cardStyleSprite;

        public void DisplayCardResults(List<object> jsonArray)
        {
            if (jsonArray.Count > 1)
            {
                Transform result10 = GachaResult10.transform.Find("Result10");
                if (result10 == null)
                {
                    Debug.LogError("Could not find result10 in GachaResult10");
                    return;
                }

                Image[] resultImages = result10.GetComponentsInChildren<Image>();

                if (resultImages.Length != jsonArray.Count)
                {
                    Debug.LogError("Length mismatch");
                    return;
                }

                for (int i = 0; i < jsonArray.Count; i++)
                {
                    Dictionary<string, object> dict = jsonArray[i] as Dictionary<string, object>;

                    string resultType = dict["type"].ToString();
                    Debug.Log("check: " + resultType);
                    // resultImage = resultImages[i];
                    switch (resultType)
                    {
                        case "coins":
                            resultImages[i].sprite = coinSprite;
                            break;
                        case "skill":
                            resultImages[i].sprite = skillSprite;
                            break;
                        case "card_style":
                            resultImages[i].sprite = cardStyleSprite;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (jsonArray.Count == 1)
            {
                Dictionary<string, object> dict = jsonArray[0] as Dictionary<string, object>;

                Image resultImage = GachaResult1.GetComponentInChildren<Image>();

                string resultType = dict["type"].ToString();
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
            else
            {
                Debug.Log("Error to parse json array result");
            }
        }

    }

}