using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Handles the ducks movement in the grid. When a duck is spawned it sets its own spot to the lowest non occupied spot
 *  When clicked send event to TextManager and destroy self.
 */

public class DuckMovement : MonoBehaviour
{
    [SerializeField]
    private int mySpot = 0;
    private Vector3 currentPosition;
    private int totalSpots = 3;

    private float time = 1f;

    public GameObject lineManager;

    public delegate void DuckAction();
    public static event DuckAction DuckActive;

    void OnEnable()
    {
        DuckMovement.DuckActive += MoveDuck;
    }

    void OnDisable()
    {
        DuckMovement.DuckActive -= MoveDuck;
    }

    void Start()
    {
        MoveDuck();
    }

    private void OnMouseDown()
    {
        if (!TextManager._waitCheck)
        {
            DuckActive();
            DuckGrid._spotOccupied[mySpot] = false;
            DuckActive();
            DestroyObject(gameObject);
        }
    }

    void MoveDuck() // Move duck to the lowest avaiable spot;
    {
        if (mySpot > totalSpots)
        {
            return;
        }

        for (int i = totalSpots; i > 0; i--)
        {
            if (DuckGrid._spotOccupied[i] == false && mySpot < i)
            {
                DuckGrid._spotOccupied[mySpot] = false;
                mySpot = i;
                DuckGrid._spotOccupied[i] = true;
                break;
            }
        }

        currentPosition = transform.position;
        StartCoroutine(MoveDuckAnim());
    }

    private IEnumerator MoveDuckAnim()
    {

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            currentPosition = transform.position;
            transform.position = Vector3.Lerp(currentPosition,
                   DuckGrid._duckPositionArray[mySpot], 0.2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = DuckGrid._duckPositionArray[mySpot];
        yield return null;
    }
}

