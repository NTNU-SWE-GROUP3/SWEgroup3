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
        public ImageManager imageManager;

        public void DisplayCardResults(List<object> jsonArray)
        {
            if (imageManager == null)
            {
                Debug.LogError("ImageManager is not assigned. Please assign it in the Inspector.");
                return;
            }

            if (jsonArray.Count > 1)
            {
                Transform result10 = GachaResult10.transform.Find("Result10");
                if (result10 == null)
                {
                    Debug.LogError("Could not find result10 in GachaResult10");
                    return;
                }

                Image[] resultImages = result10.GetComponentsInChildren<Image>();
                Text[] resultText = result10.GetComponentsInChildren<Text>();

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

                    switch (resultType)
                    {
                        case "coins":
                            resultImages[i].sprite = coinSprite;
                            resultText[i].text = "coin";
                            AdjustImageSize(resultImages[i], coinSprite);
                            break;
                        case "skill":
                            int skill_id = int.Parse(dict["id"].ToString());
                            Debug.Log("skill_id: " + skill_id);
                            Sprite skillSprite = imageManager.GetSkillImage(skill_id);
                            resultImages[i].sprite = skillSprite;
                            resultText[i].text = imageManager.GetSkillName(skill_id);
                            AdjustImageSize(resultImages[i], skillSprite);
                            break;
                        case "card_style":
                            int style_id = int.Parse(dict["id"].ToString());
                            Debug.Log("style_id: " + style_id);
                            Sprite styleSprite = imageManager.GetCardStyleImage(style_id);
                            resultImages[i].sprite = styleSprite;
                            resultText[i].text = imageManager.GetCardStyleName(style_id);
                            AdjustImageSize(resultImages[i], styleSprite);
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
                Text resultText = GachaResult1.GetComponentInChildren<Text>();

                string resultType = dict["type"].ToString();
                Debug.Log("check: " + resultType);
                switch (resultType)
                {
                    case "coins":
                        resultImage.sprite = coinSprite;
                        resultText.text = "coin";
                        // AdjustImageSize(resultImage, coinSprite);
                        break;
                    case "skill":
                        int skill_id = int.Parse(dict["id"].ToString());
                        Debug.Log("skill_id: " + skill_id);
                        Sprite skillSprite = imageManager.GetSkillImage(skill_id);
                        resultImage.sprite = skillSprite;
                        resultText.text = imageManager.GetSkillName(skill_id);
                        // AdjustImageSize(resultImage, skillSprite);
                        break;
                    case "card_style":
                        int style_id = int.Parse(dict["id"].ToString());
                        Debug.Log("style_id: " + style_id);
                        Sprite styleSprite = imageManager.GetCardStyleImage(style_id);
                        resultImage.sprite = styleSprite;
                        resultText.text = imageManager.GetCardStyleName(style_id);
                        // AdjustImageSize(resultImage, styleSprite);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("Error parsing json array result");
            }
        }

        void AdjustImageSize(Image image, Sprite sprite)
        {
            if (image != null && sprite != null)
            {
                Debug.Log($"{sprite.texture.width}, {sprite.texture.height}");
                image.rectTransform.sizeDelta = new Vector2(sprite.texture.width / 10, sprite.texture.height / 10);
            }
        }
    }
}
