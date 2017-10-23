using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDuck : MonoBehaviour
{
    [SerializeField]
    private Transform duckPrefab;
    private int chanceFactor = 0;
    private float time = 1f;

    void OnEnable()
    {
        TextManager.DuckActive += InstantiateDuck;
    }

    void OnDisable()
    {
        TextManager.DuckActive -= InstantiateDuck;
    }

    void InstantiateDuck()
    {
        int randomIndex = Random.Range(0, 2);
        print("rand: " + randomIndex + " chance: " + chanceFactor);
        if (randomIndex <= chanceFactor)
        {
            Instantiate(duckPrefab, DuckGrid._duckPositionArray[0],
                Quaternion.identity);
            chanceFactor = 0;
        }
        else if (randomIndex > chanceFactor)
        {
            chanceFactor++;
        }
    }
}
