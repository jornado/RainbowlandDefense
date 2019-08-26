using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    /* Update player's remaining money */
    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
