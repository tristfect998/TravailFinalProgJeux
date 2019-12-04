using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsSettings : MonoBehaviour {

    public Color PrimaryColor { get; set; }
    public Color SecondaryColor { get; set; }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
