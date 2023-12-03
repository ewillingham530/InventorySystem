using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image _itemSprite;
    public TextMeshProUGUI _itemCount;

    private void Awake()
    {
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

}
