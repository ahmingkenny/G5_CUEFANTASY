using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour, IDestroyable
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyIt", GetComponent<ParticleSystem>().main.duration);
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }

}
