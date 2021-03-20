using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private Transform tra;
    private Health hp;
    private Transform ob1;

    private void Start()
    {
        hp = FindObjectOfType<Health>();
        ob1 = GameObject.Find("主角").transform;
        tra = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp.health--;
        ob1.position = tra.position;
    }

}
