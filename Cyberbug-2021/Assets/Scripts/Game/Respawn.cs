using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] AudioSource death;
    // Start is called before the first frame update
    IEnumerator OnTriggerEnter(Collider other)
    {
        death.Play();
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
