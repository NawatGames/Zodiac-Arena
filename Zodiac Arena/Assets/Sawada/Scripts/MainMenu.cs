using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject lastSelectedButton = null;
    private float angleToRotate = 0f;
    public List<Sprite> signIcons = new List<Sprite>();
    public GameObject signFrame;
    public GameObject levelsFrame;
    public List<GameObject> levels = new List<GameObject>();
    
    
    private void FixedUpdate()
    {
        // Debug.Log(levelsFrame.transform.eulerAngles.z);
        if (Math.Abs(levelsFrame.transform.eulerAngles.z - angleToRotate) > 2f)
        {
            levelsFrame.transform.Rotate(0, 0, 2);
        }

    }
    
    public void OnSelect(GameObject currentSelected)
    {
        lastSelectedButton = currentSelected;
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i] == currentSelected)
            {
                signFrame.GetComponent<Image>().sprite = signIcons[i];
                angleToRotate = 360 / levels.Count * i;
            }
        }
    }
    public void Aries(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Aries");
    }
    
    public void Taurus(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Taurus");
    }

    public void Gemini(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Gemini");
    }

    public void Cancer(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Cancer");
    }
    
    public void Leo(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Leo");
    }
    
    public void Virgo(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Virgo");
    }
    
    public void Libra(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Libra");
    }
    
    public void Scorpio(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Scorpio");
    }
    
    public void Sagittarius(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Sagittarius");
    }
    
    public void Capricorn(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Capricorn");
    }
    
    public void Aquarius(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Aquarius");
    }
    
    public void Pisces(GameObject button)
    {
        if(lastSelectedButton != button)
            OnSelect(button);
        else
            SceneManager.LoadScene("Pisces");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
