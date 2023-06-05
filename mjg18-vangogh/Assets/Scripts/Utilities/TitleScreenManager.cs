using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void LoadArenaScene()
    {
        SceneManager.LoadScene("Arena");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
