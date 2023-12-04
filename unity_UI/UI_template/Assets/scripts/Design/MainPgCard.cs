using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainPgCard : MonoBehaviour
{
   
   public float rotationSpeed = 2f;
   public Sprite FrontSprite;
   public Sprite BackSprite;
    void Awake(){
        GetComponent<Image>().sprite = FrontSprite;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            RotateCard();
        }
        float angle = transform.transform.rotation.eulerAngles.y;
        if((90 < angle)&&(angle<270)){
            GetComponent<Image>().sprite = BackSprite;
        }
        else{
            GetComponent<Image>().sprite = FrontSprite;
        }
        
    }
    void RotateCard(){
        transform.Rotate(0.0f,rotationSpeed, 0.0f, Space.World);
        Debug.Log(transform.transform.rotation.eulerAngles.y);
    }
}
