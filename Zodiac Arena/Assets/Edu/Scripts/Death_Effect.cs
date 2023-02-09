using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Effect : MonoBehaviour
{
    [SerializeField] private Animation anim;
    private int sceneIndex;
    [SerializeField] float DeathTimeScale; // 0.1

    public static int[] EnemyDeaths = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // {aries, pisces, aquarius, capricorn, sagitarius, scorpio, libra, ..., taurus}
    //                                                                      0       1       2         3           4          5       6           12

    private void Start()
    {
        Time.timeScale = 1f;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Current deaths: " + EnemyDeaths[sceneIndex]);
        //Debug.Log("sceneIndex: " + sceneIndex);
    }

    public void DeathEffects()
    {
        Time.timeScale = DeathTimeScale;
        EnemyDeaths[sceneIndex] ++;
        anim.Play();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
