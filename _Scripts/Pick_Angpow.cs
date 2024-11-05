using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pick_Angpow : MonoBehaviour
{
    public Button NextButton;
    public TextMeshProUGUI info_text;
    private static int totalClicks = 0; // Static to track total clicks across all instances
    private const int maxClicks = 5; // Maximum number of valid clicks
    public AudioSource audioSource; // Reference to the AudioSource
    private void Start()
    {
        totalClicks = 0; // Reset clicks to 0 when the scene is loaded
        info_text.text = "You are free to pick 5 of these angpow!\r\nEach one has random amount of cash!\nBest of Luck!!";
    }
    private void OnMouseDown()
    {
        if (totalClicks < maxClicks)
        {
            totalClicks++;
            int randomCashAmount = Random.Range(0, 100);
            Cash_Manager.starting_cash_amount += randomCashAmount; // Update static cash amount
            info_text.text = "Current cash: $" + Cash_Manager.starting_cash_amount;
            audioSource.Play();
            Destroy(gameObject);
            if (totalClicks >= maxClicks)
            {
                Debug.Log("Maximum valid clicks reached! No more items can be picked.");
                NextButton.gameObject.SetActive(true);
                DisableRemainingItems();
            }
        }
        else
        {
            Debug.Log("No more items can be picked. Maximum limit reached.");
        }
    }

    private void DisableRemainingItems()
    {
        Pick_Angpow[] allPickables = FindObjectsOfType<Pick_Angpow>();
        foreach (Pick_Angpow pickable in allPickables)
        {
            if (pickable.gameObject != gameObject)
            {
                pickable.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
