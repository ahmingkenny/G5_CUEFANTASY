using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueSpawner : MonoBehaviour
{
    [SerializeField] private GameObject TigerSpear;
    [SerializeField] private GameObject WaterSpear;
    [SerializeField] private GameObject HorseBlade;
    [SerializeField] private GameObject DogRod;

    public void SpawnCue(int cueNum)
    {
        if (cueNum == 1)
        {
            GameObject cue = Instantiate(TigerSpear, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 2)
        {
            GameObject cue = Instantiate(WaterSpear, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 3)
        {
            GameObject cue = Instantiate(HorseBlade, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 4)
        {
            GameObject cue = Instantiate(DogRod, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

    }

    public void RespawnCue(int cueNum)
    {
        GameObject Cue = GameObject.FindGameObjectWithTag("Cue");

        if (Cue != null)
        {
            Cue.GetComponent<IDestroyable>().DestroyIt();
        }

        if (cueNum == 1)
        {
            GameObject cue = Instantiate(TigerSpear, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 2)
        {
            GameObject cue = Instantiate(WaterSpear, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 3)
        {
            GameObject cue = Instantiate(HorseBlade, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

        if (cueNum == 4)
        {
            GameObject cue = Instantiate(DogRod, this.transform.position, Quaternion.identity);
            cue.transform.parent = gameObject.transform;
        }

    }

}

