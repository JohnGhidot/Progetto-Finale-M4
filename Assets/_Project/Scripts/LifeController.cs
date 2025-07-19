using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthBarFill;

    private int _currentHealth;
    


    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateUI();
    }


    void Update()
    {
        
    }

    private void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        UpdateUI();
    }

    //public void Heal(int amount)
    //{
    //    _currentHealth += amount;
    //    _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    //    UpdateUI();
    //}

    private void UpdateUI()
    {
        if (_healthText != null)
        {
            _healthText.text = $"HP: {_currentHealth}/{_maxHealth}";

        }

        if (_healthBarFill != null)
        {
            float fillAmount = (float)_currentHealth / _maxHealth;
            _healthBarFill.fillAmount = fillAmount;
        }
    }

}
