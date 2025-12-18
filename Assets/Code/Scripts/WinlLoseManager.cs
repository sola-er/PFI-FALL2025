using UnityEngine;
using UnityEngine.UI;

public class WinlLoseManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lossScreen; // | || || |_
    public void WinChecker(bool win)
    {
        if (win)
        {
            Debug.Log("yayyyyyyyy u won !!! ggs :3");
            winScreen.SetActive(true);
        }
        else
        {
            Debug.Log("u lost. ur such a fat fucking chud");
            lossScreen.SetActive(true);
        }
    }
    private void OnValidate()
    {
        Debug.Assert(winScreen != null);
        Debug.Assert(lossScreen != null);
    }
}
