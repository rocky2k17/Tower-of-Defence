using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    private void OnEnable()
    {
        roundText.text = PlayerStat.Rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go To Menu");
    }
}
