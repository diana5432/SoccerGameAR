using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _infoDisplay;
    [SerializeField] private GameObject _creditsDisplay;

    public void ShowMainMenu()
    {
        _mainMenu.SetActive(true);
        _infoDisplay.SetActive(false);
        _creditsDisplay.SetActive(false);
    }

    public void ShowInfoDisplay()
    {
        _infoDisplay.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void ShowCreditsDisplay()
    {
        _creditsDisplay.SetActive(true);
        _mainMenu.SetActive(false);
    } 
}
