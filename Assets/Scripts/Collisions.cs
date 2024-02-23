using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("You Loose!");
        this.gameObject.SetActive(false); //can't destroy, camera follow etc.
    }
}