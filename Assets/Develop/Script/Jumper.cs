using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpPower;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.contacts[0].normal ==  Vector2.down)
            other.rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
