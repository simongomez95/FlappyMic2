using UnityEngine;
using System.Collections;

public class Obstaculizador : MonoBehaviour
{

    public GameObject obs;

    void Start()
    {
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
    }

    void CreateObstacle()
    {
        Instantiate(obs);
    }
}
