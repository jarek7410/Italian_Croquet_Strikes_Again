using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> childrenWithTag = new List<GameObject>();
        foreach (var v in childrenWithTag)
        {
            Debug.Log(v.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
