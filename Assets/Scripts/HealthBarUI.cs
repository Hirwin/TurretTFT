using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    [SerializeField] private GameObject hasHealthSystem;
    [SerializeField] private Image barImage;
    private Health hasHealth;

    private void Start() {
        hasHealth = hasHealthSystem.GetComponent<Health>();
        if (hasHealth == null) {
            Debug.LogError(hasHealthSystem + " Does not have an IHasProgress Thingy");
        }
        hasHealth.OnHealthChanged += HasHealth_OnHealthChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasHealth_OnHealthChanged(object sender, Health.OnHealthChangedEventArgs e) {
        barImage.fillAmount = e.healthNormalized;
        if (e.healthNormalized == 0f || e.healthNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
