using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabGroup : MonoBehaviour
{
    public List<CategoryButton> categoryButtons;
    public List<GameObject> objectsToSwap;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public CategoryButton selectedTab;

    public void Subscribe(CategoryButton button)
    {
        if(categoryButtons == null)
        {
            categoryButtons = new List<CategoryButton>();
        }

        categoryButtons.Add(button);
    }

    public void OnTabEnter(CategoryButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }

    }

    public void OnTabExit(CategoryButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(CategoryButton button)
    {
        //Find if selected tab selects and deselect it
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        TextMeshProUGUI tabText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tabText != null)
        {
            string name = tabText.text;
            selectedTab.Select(name);
           
        }
        
        ResetTabs();
       button.background.color = tabActive;

        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i< objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (CategoryButton button in categoryButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }
}
