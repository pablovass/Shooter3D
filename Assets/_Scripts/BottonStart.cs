using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
public class BottonStart : MonoBehaviour
{
    public void StartGame()
    {
        print("Start game");
        SceneManager.LoadScene("Main");

    }
}
