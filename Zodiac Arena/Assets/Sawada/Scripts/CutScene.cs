using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    void Update()
    {
        if (!gameObject.GetComponent<VideoPlayer>().isPlaying)
        {
            SceneManager.LoadScene("MainMenu");
        }else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
}
