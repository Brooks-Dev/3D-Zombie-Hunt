using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    public int currentHealth;

    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = _maxHealth;
        _slider = GameObject.Find("Health Bar").GetComponent<Slider>();
        _slider.maxValue = _maxHealth;
        HealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        //use later?
    }

    public void Damage(int pain)
    {
        currentHealth -= pain;
        Debug.Log("Health is " + currentHealth);
        if (currentHealth < _minHealth)
        {
            Destroy(this.gameObject);
        }
        HealthBar();
    }

    public void HealthBar()
    {
        _slider.value = currentHealth;
    }
}
