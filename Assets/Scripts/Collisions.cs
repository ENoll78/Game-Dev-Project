using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        SceneManager.LoadScene("GameOver");
        this.gameObject.SetActive(false); //can't destroy, camera follow etc.
    }
}