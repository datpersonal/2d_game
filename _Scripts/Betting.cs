using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Betting : MonoBehaviour
{
    public static Betting Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI info_text;
    public TextMeshProUGUI current_cash_text;
    private int gourd_bet_amount = 0;
    private int fish_bet_amount = 0;
    private int shrimp_bet_amount = 0;
    private int deer_bet_amount = 0;
    private int crab_bet_amount = 0;
    private int chicken_bet_amount = 0;
    public int current_cash = 0;
    public int starting_cash_amount;
    private int previous_cash = 0;
    public int GourdBetAmount => gourd_bet_amount;
    public int FishBetAmount => fish_bet_amount;
    public int ShrimpBetAmount => shrimp_bet_amount;
    public int DeerBetAmount => deer_bet_amount;
    public int CrabBetAmount => crab_bet_amount;
    public int ChickenBetAmount => chicken_bet_amount;
    public Button[] bettingButtons; // Array of betting buttons to disable
    public Button ResetButton;
    public Button QuitButton;
    public Button RetryButton;
    public Button RolDiceButton;
    public AudioSource winning_sound;
    public AudioSource gameover_sound;
    public AudioSource big_win_sound;
    public Button yesBtn;
    public Button noBtn;
    private void Awake()
    {
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);

    }
    void Start()
    {
        current_cash += Cash_Manager.starting_cash_amount;
        UpdateAllBetTexts();        
        RolDiceButton.interactable = false;
        ResetButton.interactable = false;        
    }

    public void ButtonClicked(Button clickedButton)
    {
        ResetButton.interactable = true;
        RolDiceButton.interactable = true;      
        info_text.text = "Current Bet:\n";     
        string buttonName = clickedButton.name;
        
        // Parse the amount from the button name (format: "Bet_X_Gourd")
        string[] parts = buttonName.Split('_');

        if (parts.Length > 2) // We need at least two parts: "Bet" and "X"
        {
            if (int.TryParse(parts[1], out int amount))
            {
                if (current_cash - amount < 0)
                {
                    UpdateAllBetTexts();
                    DisableBettingButtons();
                    info_text.text += "----------------------------\n";
                    info_text.text += "<color=red>Insufficient Money!\nPlease roll dice now or reset the bet! </color>\n";
                    return;
                }
                else
                {                   
                    current_cash -= amount;                    
                }
                string betOn = parts[2].ToLower();

                switch (betOn)
                {
                    case "gourd":
                        gourd_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    case "fish":
                        fish_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    case "shrimp":
                        shrimp_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    case "deer":
                        deer_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    case "crab":
                        crab_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    case "chicken":
                        chicken_bet_amount += amount;
                        Debug.Log("Current cash before bet: " + current_cash + " Previus cas before bet: " + previous_cash);
                        break;
                    default:
                        Debug.LogWarning("Unknown bet type: " + betOn);
                        return; // Exit if the bet type is not recognized
                }
                
                UpdateAllBetTexts();           
            }

        }       
    }

    private void UpdateAllBetTexts()
    {
        if (deer_bet_amount > 0)
        {
            info_text.text += "Deer Bet: $" + deer_bet_amount + "\n";
        }
            if (gourd_bet_amount > 0)
            {
                info_text.text += "Gourd Bet: $" + gourd_bet_amount + "\n";

            }
            if (shrimp_bet_amount > 0)
            {
                info_text.text += "Shrimp Bet: $" + shrimp_bet_amount + "\n";
            }
            if (crab_bet_amount > 0)
            {
                info_text.text += "Crab Bet: $" + crab_bet_amount + "\n";
            }
            if (chicken_bet_amount > 0)
            {
                info_text.text += "Chicken Bet: $" + chicken_bet_amount + "\n";
            }
            if (fish_bet_amount >0)
        {
            info_text.text += "Fish Bet: $" + fish_bet_amount + "\n";
        }
     
        current_cash_text.text = "Current Cash: $" + current_cash;
    }   
  
    public void DisableBettingButtons()
    {
        foreach (Button button in bettingButtons)
        {
            string buttonName = button.name;
            string[] parts = buttonName.Split('_');
            if (int.TryParse(parts[1], out int amount))
            {
                if (amount > current_cash)
                    button.interactable = false; // Disable each betting button
            }
        }
    }

    public void EnableBettingButtons()
    {

        foreach (Button button in bettingButtons)
        {
            button.interactable = true; // Re-enable each betting button
        }        
    }

    public void ResetBets()
    {
        gourd_bet_amount = 0;
        fish_bet_amount = 0;
        shrimp_bet_amount = 0;
        deer_bet_amount = 0;
        crab_bet_amount = 0;
        chicken_bet_amount = 0;
        info_text.text = "";
    }
    public void ResetCurrentbet()
    {
        current_cash += chicken_bet_amount + crab_bet_amount + fish_bet_amount + gourd_bet_amount + shrimp_bet_amount + deer_bet_amount;
        current_cash_text.text = "Current Cash: $" + current_cash;
        ResetBets();
        ResetButton.interactable = false;
        EnableBettingButtons();
        
    }

}
