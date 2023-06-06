
using UnityEngine;
using TMPro;


public class LivesUI : MonoBehaviour
{


    public TextMeshProUGUI livesText;
    void Update()
    {
        livesText.text = PlayerStat.Lives.ToString() + "LIVES";
    }
}
