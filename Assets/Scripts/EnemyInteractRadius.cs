using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractRadius : MonoBehaviour
{
    [SerializeField] private Objective objective; //We can turn this into an interactable and deal with this for the book item later so they can target it if its ont he ground somewhere,.

    private void Start() {
        objective ??= GetComponent<Objective>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            objective.AttemptToStealItem(other.GetComponent<Enemy>());
        }
    }
}
