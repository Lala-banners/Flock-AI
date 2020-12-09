using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : FlockLife
{
    public static Prey instance = null;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        // is instance already set? and not me?
        else if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    [Range(10, 500)] //[Range()] inside unity editor sets min and max birds as a slider
    public int startingCount = 150;

    [Range(1f, 100f)]
    public float maxPreySpeed = 5f;
}
