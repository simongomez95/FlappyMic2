using UnityEngine;
using System.Collections;

public class Obstaculo : MonoBehaviour
{
    public float velocity = -4f;
    private Transform trans;
    private Random rand;

    void Start()
    {
        rand = new Random();
        trans = GetComponent<Transform>();
        trans.position = new Vector3(7f, Random.Range(0.42f, 3.4f), trans.position.z);

    }

    void Update()
    {
        trans.position = new Vector3(trans.position.x + velocity, trans.position.y, trans.position.z);
    }
}