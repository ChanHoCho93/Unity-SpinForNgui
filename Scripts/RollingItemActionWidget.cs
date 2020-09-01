using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingItemActionWidget : MonoBehaviour
{
    public UILabel label;
    public UISprite sprite;

    public void Start()
    {
        Set(true);
    }

    public void Set(bool isLabel)
    {
        if (label != null && sprite != null)
        {
            label.gameObject.SetActive(isLabel);
            sprite.gameObject.SetActive(!isLabel);
        }
    }

    public void Toggle()
    {
        if (label != null && sprite != null)
        {
            label.gameObject.SetActive(!label.gameObject.activeSelf);
            sprite.gameObject.SetActive(!sprite.gameObject.activeSelf);
        }
    }
}
