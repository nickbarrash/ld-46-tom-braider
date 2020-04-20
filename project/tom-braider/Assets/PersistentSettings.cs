using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSettings : MonoBehaviour
{
    private static bool created = false;
    public bool HardMode = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        } else {
            Destroy(this.gameObject);
        }
    }
}
