using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class spawns ducks, There is a random chance that a duck is spawned when currentCard changes.
 * The chance increases each time the player gets a new card but does not spawn a duck. First 1/10, Second 2/10.....
*/

public class SpawnDuck : MonoBehaviour
{
    [SerializeField]
    private Transform duckPrefab;
    [SerializeField]
    private int probabilityFactor = 10;
    private int chanceFactor = 0;
    private float time = 1f;

    void OnEnable()
    {
        TextManager.DuckSpawnActive += InstantiateDuck;
    }

    void OnDisable()
    {
        TextManager.DuckSpawnActive -= InstantiateDuck;
    }

    void InstantiateDuck()
    {
        int randomIndex = Random.Range(1, probabilityFactor);
        //print("rand: " + randomIndex + " chance: " + chanceFactor);
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
