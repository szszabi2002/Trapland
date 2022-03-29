using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
