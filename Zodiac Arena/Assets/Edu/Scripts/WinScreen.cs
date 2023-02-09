using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    public static int lastSceneIndex;
    [SerializeField] private Text deathCounterText;

    private void Start()
    {
        deathCounterText.text = "You died " + Death_Effect.EnemyDeaths[lastSceneIndex] + " times";
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMainMenu");
    }
}
