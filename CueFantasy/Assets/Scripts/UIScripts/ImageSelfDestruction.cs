using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSelfDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyIt", GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }

}
