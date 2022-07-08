using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementStatsHandeler : MonoBehaviour
{
    // NOTE: this script is respinsible for changing the food and stamina stats based on the player movement actions
    
    [Header("References")]
    [Tooltip("The Name of the food stat that you made in the player stats")]
    [SerializeField] string foodStatName = "Food"; // the name of the food stat
    [Tooltip("The Name of the stamina stat that you made in the player stats")]
    [SerializeField] string staminaStatName = "Stamina"; // the name of the stamina stat
    private Stat foodStat, staminaStat; // a reference for the food an stamina stats
    private PlayerMovement playerMovement; // a reference for the player movement

    [Header("Settings")]
    [SerializeField] float staminaRecoverSpeed = 2f; // the speed in which the player recovers stamina when in rest (not running/jumping)
    [SerializeField] float staminaConsumeSpeedOnPlayerRun = 3f; // the sped in which the player consumes the stamina when running
    [SerializeField] float staminaConsumeAmountOnPlayerJump = 10f; // the amount in which the player consumes the stamina when jumping
    [SerializeField] float playerFoodConsumeSpeed = 5f; // the speed in which the player consumes food when walking/running
    
    public float GetJumpStaminaNormalizedConsumeAmount() 
    {
        (float minStaminaAmount, float maxStaminaAmount) = staminaStat.GetMinMaxStatAmount();
        return (staminaConsumeAmountOnPlayerJump - minStaminaAmount) / (maxStaminaAmount - minStaminaAmount);
    }

    private void Start()
    {
        FindPrivateObjects();
        // start listening to the player jump event and consume stamina when the player jumps
        playerMovement.playerJumped += ConsumeStaminaOnPlayerJump;
    }

    // when the script is destroyed stop listening to the player jump event
    private void OnDestroy() => playerMovement.playerJumped -= ConsumeStaminaOnPlayerJump;

    void Update()
    {
        HandleStamina();
        HandleFood();
    }

    private void HandleFood()
    {
        if (!playerMovement.IsMoving()) { return; }
        ConsumeFood();
    }

    private void HandleStamina()
    {
        // if the player is running then consume stamina
        if (playerMovement.IsRunning()) ConsumeStaminaOnPlayerRun();
        // if player is not running recover stamina 
        else RecoverStamina();
    }

    // checks if the stamina normalized stat amount is bigger than the stamina amount the methoed recives(defult is 0)
    public bool HasStamina(float normalizedStaminaAmount = 0f) => staminaStat.GetNormalizedStatAmount() > normalizedStaminaAmount;

    // consume the stamina stat when the player jumps based on the player jump stamina consume value
    private void ConsumeStaminaOnPlayerJump() => ConsumeStat(staminaStat, staminaConsumeAmountOnPlayerJump);
    
    // consume the stamina stat when the player runs based on the stamina consume speed
    private void ConsumeStaminaOnPlayerRun() => ConsumeStat(staminaStat, staminaConsumeSpeedOnPlayerRun * Time.deltaTime);

    // consume the food stat  when the player walks/runs based on the player food consume speed
    private void ConsumeFood() => ConsumeStat(foodStat, playerFoodConsumeSpeed * Time.deltaTime);

    // consume a negetive value to recover the stamina stat base on the stamina recover speed
    private void RecoverStamina()
    {
        float recoverAmount = staminaRecoverSpeed * Time.deltaTime;
        ConsumeStat(staminaStat, -recoverAmount);
    }

    // base method consume a stat based on amount
    private void ConsumeStat(Stat stat, float consumeAmount)
    {
        stat.SetStatAmount(stat.GetStatAmount() - consumeAmount);
    }

    private void FindPrivateObjects()
    {
        // get the food and stamina stats base on their names
        PlayerStats playerStats = PlayerStats.instance;
        foodStat = playerStats.GetStat(foodStatName);
        staminaStat = playerStats.GetStat(staminaStatName);
        // get the player movement
        playerMovement = GetComponent<PlayerMovement>();
    }
}
