using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollision : MonoBehaviour
{
    public SnakeMovement sm;
    private void Awake()
    {
        sm = FindObjectOfType<SnakeMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food")
        {
            Destroy(other.gameObject, 0.1f);
            Debug.Log("growing snake");
            sm.GrowSnake();
        }
        if (other.tag == "border" || other.tag=="spike")
        {
            sm.GameOverFunc();
        }
    }
}
