using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class No_choice : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI info_text;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        info_text.text = "";
    }
}
