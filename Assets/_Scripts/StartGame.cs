using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{


    public void OnButtonClick()
    {
        LoadScene("_Pick_Your_Angpow");
        Cash_Manager.starting_cash_amount = 0;
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
