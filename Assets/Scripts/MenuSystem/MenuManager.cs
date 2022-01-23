using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    // Array of Menu Pages
    public MenuPage[] menuPages = new MenuPage[0];
    // Current Menu Page
    public int currentMenuPage = 0;
    // Current Page Element (which button are we on?)
    public int currentPageElement = 0;

    // Show page on start
    public bool showPageOnStart = true;

    // Each page is a script containing all the UI Elements?
    // Page Script Contains an array of menu elements (in order of interaction)
    // Page Script contains refrence to its menu UI group 

    // Set current selected in event system to current page element?

    void Start()
    {
        if (showPageOnStart)
        {
            ShowMenuPage(0);
        }
    }


    public void HideAllMenuPages()
    {
        // Cycle through array of pages and hide each UI group
        foreach (var page in menuPages)
        {
            page.menuUI.SetActive(false);
        }
    }

    [ContextMenu("Show Menu Page")]
    public void ShowMenuPage(int newPage)
    {

        currentMenuPage = newPage;

        // Hide all pages for a clean start
        HideAllMenuPages();
        // Show current menu page
        menuPages[currentMenuPage].menuUI.SetActive(true);
        //
        SelectFirstElement();
    }


    public void SelectFirstElement()
    {
        currentPageElement = 0;
        SelectPageElement();
    }

    void SelectPageElement()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuPages[currentMenuPage].menuElements[currentPageElement]);

        HighlightSelectedElement();
    }

    [ContextMenu("Next Element")]
    public void NextElement()
    {
        if (currentPageElement < menuPages[currentMenuPage].menuElements.Length - 1)
        {
            currentPageElement++;
        }
        else
        {
            currentPageElement = 0;
        }

        SelectPageElement();
    }
    [ContextMenu("Previous Element")]
    public void PreviousElement()
    {
        if (currentPageElement > 0)
        {
            currentPageElement--;
        }
        else
        {
            currentPageElement = menuPages[currentMenuPage].menuElements.Length - 1;
        }

        SelectPageElement();
    }


    public void SelectElement(int elementToSelect)
    {
        currentPageElement = elementToSelect;
        SelectPageElement();
    }


    // Show the white line under the selected element
    void HighlightSelectedElement()
    {
        HideAllHighlights();

        menuPages[currentMenuPage].menuElements[currentPageElement].transform.GetChild(0).gameObject.SetActive(true);
    }
    void HideAllHighlights()
    {
        foreach (var element in menuPages[currentMenuPage].menuElements)
        {
            element.transform.GetChild(0).gameObject.SetActive(false);
        }
    }



}
