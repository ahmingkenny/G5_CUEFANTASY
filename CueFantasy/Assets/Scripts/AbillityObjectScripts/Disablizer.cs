using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablizer : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject TinyExplosionFx;
    [SerializeField] private int power = 100;

    void Start()
    {
        TinyExplosionFx = Instantiate(TinyExplosionFx, this.transform.position, Quaternion.identity);
        Invoke("DestroyIt", 0.5f);

        GameObject[] AbilityObject = GameObject.FindGameObjectsWithTag("AbilityObject");
        if (AbilityObject != null)
        {
            foreach (GameObject abilityObject in AbilityObject)
            {
                abilityObject.GetComponent<IDamageable>().Hit(power);
            }
        }

    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}
