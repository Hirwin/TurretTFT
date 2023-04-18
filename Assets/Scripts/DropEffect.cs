using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class DropEffect : MonoBehaviour
{
    private Vector3 velocity = Vector3.up;
    [SerializeField]private Rigidbody rb;
    private Vector3 startPosition;
    private Boolean dropped = false;
    // Update is called once per frame
    void Update()
    {
        if (dropped) {
            rb.position += velocity * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(Random.Range(-150f, 150f), Random.Range(150f, 250f), Random.Range(-150f, 150f)) * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);

            if (velocity.y < -4f) {
                velocity.y = -4f;
            } else {
                velocity -= Vector3.up * 5 * Time.deltaTime;
            }

            if (Mathf.Abs(rb.position.y) < 0.25f && velocity.y < 0f) {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.velocity = velocity;
                this.enabled = false;
            }
        }
    }

    public void TryDropItem() {
        dropped = true;

        startPosition = this.transform.position;
        velocity *= Random.Range(4f, 6f);
        velocity += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
