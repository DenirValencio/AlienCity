using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudaCor : MonoBehaviour {
    private SpriteRenderer sr;
    private Color[] cores = new Color[] { Color.red, Color.green, Color.blue };
    public int valPedra = 0;

    // Use this for initialization
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    //Quando player toca apedra, ela desparece.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (valPedra == 2)
        {
            valPedra = 0;
        }
        else
        {
            valPedra++;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            sr.color = cores[valPedra];
        }
    }
}
