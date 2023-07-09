using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallSiegeBehaviour : MonoBehaviour
{
    private GameObject GameManager;
    private Attacker attacker;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        attacker = GameManager.GetComponent<Attacker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "JadeBall")
        {
            attacker.GainMana();
        }

    }
}
