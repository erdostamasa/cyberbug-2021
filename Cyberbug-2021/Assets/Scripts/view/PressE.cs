using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressE : MonoBehaviour
{
    [SerializeField] GameObject e;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onMapChanged += EChange;
    }

    // Update is called once per frame
    void EChange(bool change)
    {
        if (change)
        {
            e.SetActive(true);
        }

        if (!change)
        {
            e.SetActive(false);
        }
    }
}
