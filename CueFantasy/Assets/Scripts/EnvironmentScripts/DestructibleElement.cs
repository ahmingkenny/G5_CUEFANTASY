using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleElement : MonoBehaviour, IDamageable, IDestroyable
{
    [SerializeField] private int hp = 50;
    [SerializeField] private GameObject BigDustExplosionFX;
    [SerializeField] private AudioClip DestructionSound;
    [SerializeField] private AudioClip ImpactSound;

    public void Hit(int damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(ImpactSound, this.transform.position);
        if (hp <= 0)
        {
            DestroyIt();
        }
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
        Instantiate(BigDustExplosionFX, this.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(DestructionSound, this.transform.position);
    }

}
