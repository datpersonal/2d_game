using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Next_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        LoadScene("_Play_Scene");
    }

    private void LoadScene(string sceneName)
    {
        // Check if the scene exists before loading
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or invalid!");
        }
    }
}
