using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DiceManager : MonoBehaviour
{
    [Header("Dice Configuration")]
    public Dice[] diceScripts; // Array of Dice scripts
    public Button rollAllButton; // Reference to the button for rolling all dice
    public Betting betting; // Reference to the Betting class
    public Image confirmPanelBackground;
    //Private variables needed for this script
    private int previousGourdBet, previousFishBet, previousShrimpBet, previousDeerBet, previousCrabBet, previousChickenBet;
    private int totalWinnings = 0;
    private int gourdWin = 0;
    private int fishWin = 0;
    private int shrimpWin = 0;
    private int deerWin = 0;
    private int crabWin = 0;
    private int chickenWin = 0;
    private int previousCash = 0;
    private void Start()
    {   
        //When first start these button need to be not able to interact
        betting.RolDiceButton.interactable = false;
        betting.RetryButton = GetComponent<Button>();
        // Assign the button click listener
        rollAllButton.onClick.AddListener(RollAllDice);
    }
    private void initializeBetAmount()
    {

        betting.ResetButton.interactable = false;
        betting.RolDiceButton.interactable = false;
        previousCash = betting.current_cash;
        previousGourdBet = betting.GourdBetAmount;
        previousFishBet = betting.FishBetAmount;
        previousShrimpBet = betting.ShrimpBetAmount;
        previousDeerBet = betting.DeerBetAmount;
        previousCrabBet = betting.CrabBetAmount;
        previousChickenBet = betting.ChickenBetAmount;
    }
    public void RollAllDice()
    {
        initializeBetAmount();
        betting.ResetBets();        
        StartCoroutine(RollAllDiceCoroutine(previousGourdBet, previousFishBet, previousShrimpBet, previousDeerBet, previousCrabBet, previousChickenBet));
        betting.info_text.text = "Current Result:\n";
    }


    private IEnumerator RollAllDiceCoroutine(int previousGourdBet, int previousFishBet, int previousShrimpBet, int previousDeerBet, int previousCrabBet, int previousChickenBet)
    {

        // Start rolling all dice concurrently
        Coroutine[] rollCoroutines = new Coroutine[diceScripts.Length];
        for (int i = 0; i < diceScripts.Length; i++)
        {
            rollCoroutines[i] = StartCoroutine(diceScripts[i].RollTheDice());
        }

        // Wait for all rolling coroutines to finish
        foreach (var rollCoroutine in rollCoroutines)
        {
            yield return rollCoroutine;
        }

        // After rolling all dice, evaluate outcomes
        EvaluateDiceOutcomes(previousGourdBet, previousFishBet, previousShrimpBet, previousDeerBet, previousCrabBet, previousChickenBet);
    }

    private void EvaluateDiceOutcomes(int previousGourdBet, int previousFishBet, int previousShrimpBet, int previousDeerBet, int previousCrabBet, int previousChickenBet)
    {
               Dictionary<string, int> outcomeCounts = new Dictionary<string, int>
    {
        { "gourd", 0 },
        { "fish", 0 },
        { "shrimp", 0 },
        { "deer", 0 },
        { "crab", 0 },
        { "chicken", 0 }
    };
        int gourdAppearance = 0;
        int fishAppearance = 0;
        int shrimpAppearance = 0;
        int deerAppearance = 0;
        int crabAppearance = 0;
        int chickenAppearance = 0;

        // Count the appearances of each outcome
          foreach (var dice in diceScripts)
          {       
              if (outcomeCounts.ContainsKey(dice.FinalOutcome.ToLower()))
              {
                  if (dice.FinalOutcome.ToLower() == "gourd")
                      gourdAppearance++;
                  else if (dice.FinalOutcome.ToLower() == "fish")
                      fishAppearance++;
                  else if (dice.FinalOutcome.ToLower() == "shrimp")
                      shrimpAppearance++;
                  else if (dice.FinalOutcome.ToLower() == "deer")
                      deerAppearance++;
                  else if (dice.FinalOutcome.ToLower() == "crab")
                      crabAppearance++;
                  else if (dice.FinalOutcome.ToLower() =="chicken")
                      chickenAppearance++;                
              }
          }

        //fishAppearance = 3;
        //CalculateWiningAmount(betting.previous_cash,gourdAppearance, fishAppearance, shrimpAppearance, deerAppearance, crabAppearance, chickenAppearance);
        CalculateWiningAmount(gourdAppearance, fishAppearance, shrimpAppearance, deerAppearance, crabAppearance, chickenAppearance);

        //Check if current cash is enough for next round
        if (betting.current_cash > 0)
            betting.EnableBettingButtons();
        else
            GameOver();      


    }
    // This is to determine the winning amount
    private void CalculateWiningAmount(int gourdAppearance, int fishAppearance, int shrimpAppearance, int deerAppearance, int crabAppearance, int chickenAppearance)
    {
        // Calculate Gourd Winnings
        if (gourdAppearance > 0 && previousGourdBet > 0)
        {     
            gourdWin = previousGourdBet * (gourdAppearance + 1);        
            totalWinnings += gourdWin; // Add gourd winnings to total
            betting.current_cash += gourdWin; // Update current cash
        }

        // Calculate Fish Winnings
        if (fishAppearance > 0 && previousFishBet > 0)
        { 
            fishWin = previousFishBet * (fishAppearance + 1);
            totalWinnings += fishWin;
            betting.current_cash += fishWin;
        }

        // Calculate Shrimp Winnings
        if (shrimpAppearance > 0 && previousShrimpBet > 0)
        {  
            shrimpWin = previousShrimpBet * (shrimpAppearance + 1);
            totalWinnings += shrimpWin;
            betting.current_cash += shrimpWin;
        }

        // Calculate Deer Winnings
        if (deerAppearance > 0 && previousDeerBet > 0)
        { 
            deerWin = previousDeerBet * (deerAppearance + 1);      
            totalWinnings += deerWin;
            betting.current_cash += deerWin;
        }

        // Calculate Chicken Winnings
        if (chickenAppearance > 0 && previousChickenBet > 0)
        {
            chickenWin = previousChickenBet * (chickenAppearance + 1); 
            totalWinnings += chickenWin;
            betting.current_cash += chickenWin;
        }

        // Calculate Crab Winnings
        if (crabAppearance > 0 && previousCrabBet > 0)
        {
            crabWin = previousCrabBet * (crabAppearance + 1);
            totalWinnings += crabWin;
            betting.current_cash += crabWin;
        }
        UpdateResult(totalWinnings, gourdAppearance, fishAppearance, shrimpAppearance, deerAppearance, crabAppearance, chickenAppearance);
    }

    private void UpdateResult(int totalWinning, int gourdAppearance, int fishAppearance, int shrimpAppearance, int deerAppearance, int crabAppearance, int chickenAppearance)
    {
        int totalWin = 0;
        int previousBetTotal = previousChickenBet + previousCrabBet + previousDeerBet + previousGourdBet + previousShrimpBet + previousFishBet;
        bool isJackPot = false;
        // Gourd Results
        if (previousGourdBet > 0 && gourdAppearance > 0)
        {
            betting.info_text.text += gourdAppearance + " Gourd(s): <color=green>+$" + gourdWin + "</color>\n";
            totalWin += gourdWin;       
                
                if (gourdAppearance == 3)
                {
                    Mini_Game_PopUp();
                    betting.big_win_sound.GetComponent<AudioSource>().Play();   
                }    
            
        }
        else if (previousGourdBet > 0 && gourdAppearance == 0)
        {
            betting.info_text.text += "No Gourd: <color=red>-$" + previousGourdBet + "</color>\n";
            
        }

        // Fish Results
        if (previousFishBet > 0 && fishAppearance > 0)
        {        
            betting.info_text.text += fishAppearance + " Fish(s): <color=green>+$" + fishWin + "</color>\n";
            totalWin += fishWin;        
                
                if (fishAppearance == 3)
                {
                isJackPot = true;

                    
                }
        }
        else if (previousFishBet > 0 && fishAppearance == 0)
        {
            betting.info_text.text += "No Fish: <color=red>-$" + previousFishBet + "</color>\n";           
         }

        // Shrimp Results
        if (previousShrimpBet > 0 && shrimpAppearance > 0)
        {           
            betting.info_text.text += shrimpAppearance + " Shrimp(s): <color=green>+$" + shrimpWin + "</color>\n";
            totalWin += shrimpWin;
            
                
                if (shrimpAppearance == 3)
                {
                isJackPot = true;              
                }               

        }
        else if (previousShrimpBet > 0 && shrimpAppearance == 0)
        {
            betting.info_text.text += "No Shrimp: <color=red>-$" + previousShrimpBet + "</color>\n";                
        }

        // Chicken Results
        if (previousChickenBet > 0 && chickenAppearance > 0)
        {          
            betting.info_text.text += chickenAppearance + " Chicken(s): <color=green>+$" + chickenWin + "</color>\n";
            totalWin += chickenWin;
             
                if (chickenAppearance == 3)
                {
                isJackPot = true;
                }
          
        }
        else if (previousChickenBet > 0 && chickenAppearance == 0)
        {
            betting.info_text.text += "No Chicken: <color=red>-$" + previousChickenBet + "</color>\n";           
        }

        // Deer Results
        if (previousDeerBet > 0 && deerAppearance > 0)
        {
            betting.info_text.text += deerAppearance + " Deer(s): <color=green>+$" + deerWin + "</color>\n";
            totalWin += deerWin;  
                
                if (deerAppearance == 3)
            {
               isJackPot = true;
            }
                   
            
 
        }
        else if (previousDeerBet > 0 && deerAppearance == 0)
        {
            betting.info_text.text += "No Deer: <color=red>-$" + previousDeerBet + "</color>\n";         
        }

        // Crab Results
        if (previousCrabBet > 0 && crabAppearance > 0)
        { 
            betting.info_text.text += crabAppearance + " Crab(s): <color=green>+$" + crabWin + "</color>\n";
            totalWin += crabWin;           
                if (crabAppearance == 3)
                {
                isJackPot = true;
                }              
        }
        else if (previousCrabBet > 0 && crabAppearance == 0)
        {
            betting.info_text.text += "No Crab: <color=red>-$" + previousCrabBet + "</color>\n";  
           
        }
        if (isJackPot)
        {
            betting.current_cash_text.text = "Current Cash: $" + betting.current_cash;
            betting.big_win_sound.GetComponent<AudioSource>().Play();
            Mini_Game_PopUp();
            return;
        }

        if (totalWin < previousBetTotal)        
        {           

            betting.gameover_sound.GetComponent<AudioSource>().Play();
        }
        else
        {
            betting.winning_sound.GetComponent<AudioSource>().Play();
        }
        betting.current_cash_text.text = "Current Cash: $" + betting.current_cash;
    }
    private IEnumerator LoadSceneAfterSound(float delay)
    {
        // Wait for the audio to finish
        yield return new WaitForSeconds(delay);
        Mini_Game_PopUp();
    }
    private void GameOver()
    {
        betting.gameover_sound.GetComponent<AudioSource>().Play();
        betting.DisableBettingButtons();
        betting.RetryButton.gameObject.SetActive(true);
        betting.RolDiceButton.interactable = false;       
    }
    public void Mini_Game_PopUp()
    {
        betting.yesBtn.gameObject.SetActive(true);
        betting.noBtn.gameObject.SetActive(true);
        betting.info_text.text = "You hit jackpot\nPress Yes to go to your reward scene, press No if you wish to stay";
        Cash_Manager.starting_cash_amount = betting.current_cash;
    }
}