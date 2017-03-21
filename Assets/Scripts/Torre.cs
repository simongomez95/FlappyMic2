using UnityEngine;
using System.Collections;

public class Torre : MonoBehaviour {

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
        int rand = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = sprites[rand];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
