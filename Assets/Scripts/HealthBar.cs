using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHeatlhBar;
    [SerializeField] private Image currentHeatlhBar;

    // Start is called before the first frame update
    void Start()
    {
        totalHeatlhBar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHeatlhBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
