using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mini_game : MonoBehaviour
{
    Betting betting;
    public void OnButtonClick()
    {
        SceneManager.LoadScene("_Pick_Your_Angpow");

    }
}
