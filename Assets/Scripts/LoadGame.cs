using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadGame : MonoBehaviour
{

    public void OnStart() // ゲームをはじめる
    {
        SceneManager.LoadScene("Select Scene");
    }    
}
