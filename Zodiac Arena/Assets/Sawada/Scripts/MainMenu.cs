using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Aries()
    {
        SceneManager.LoadScene("Aries");
    }
    
    public void Taurus()
    {
        SceneManager.LoadScene("Taurus");
    }

    public void Gemini()
    {
        SceneManager.LoadScene("Gemini");
    }

    public void Cancer()
    {
        SceneManager.LoadScene("Cancer");
    }
    
    public void Leo()
    {
        SceneManager.LoadScene("Leo");
    }
    
    public void Virgo()
    {
        SceneManager.LoadScene("Virgo");
    }
    
    public void Libra()
    {
        SceneManager.LoadScene("Libra");
    }
    
    public void Scorpio()
    {
        SceneManager.LoadScene("Scorpio");
    }
    
    public void Sagittarius()
    {
        SceneManager.LoadScene("Sagittarius");
    }
    
    public void Capricorn()
    {
        SceneManager.LoadScene("Capricorn");
    }
    
    public void Aquarius()
    {
        SceneManager.LoadScene("Aquarius");
    }
    
    public void Pisces()
    {
        SceneManager.LoadScene("Pisces");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
