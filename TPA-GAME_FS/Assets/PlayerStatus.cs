using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

    public CharacterStats playerStats;
    [SerializeField]
    private Image hpbar, xpbar;

    public Text levelInd;
    // Start is called before the first frame update
    void Start()
    {
        levelInd = GetComponent<Text>();
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        levelInd.text = "Level " + (playerStats.level).ToString();
        hpbar.fillAmount = (float)playerStats.currentHealth / playerStats.maxHealth;
        xpbar.fillAmount = (float)playerStats.exp / playerStats.maxExp;
    }


}
