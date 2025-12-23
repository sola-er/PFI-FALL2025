using UnityEngine;
using UnityEngine.UI;

public class WinlLoseManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lossScreen; /* | || || |_ */
    public void CheckWinCondition(bool win) // true = win, false = loss
    {
        if (win)
        {
            Debug.Log("yayyyyyyyy u won !!! ggs :3");
            winScreen.SetActive(true);
        }
        else
        {
            Debug.Log("u lost. do better :(");
            lossScreen.SetActive(true);
        }
    }
    private void OnValidate()
    {
        Debug.Assert(winScreen != null);
        Debug.Assert(lossScreen != null);
    }
}
