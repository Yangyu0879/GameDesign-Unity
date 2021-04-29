using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    private Image healthBar;
    public static int healthCurrent;
    public static int healthMax;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)healthCurrent / (float)healthMax;
        healthText.text = healthCurrent.ToString() + "/" + healthMax.ToString();
    }
}
