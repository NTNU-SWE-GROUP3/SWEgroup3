using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    public void ClickSkip()
    {
        UseSkill.Clock = 0;
        gameObject.SetActive(false);
    }
}
