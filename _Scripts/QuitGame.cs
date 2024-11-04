using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuitButton : MonoBehaviour
{ 

    public void OnButtonClick()
    {
        Application.Quit(); 
    }
    public void OnButtonClickWeb()
    {
                SceneManager.LoadScene("_Quit_Scene");
    }


}
