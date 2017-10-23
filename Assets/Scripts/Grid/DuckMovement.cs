using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [SerializeField]
    private int mySpot = 0;
    private Vector3 currentPosition;
    private int totalSpots = 3;

    private float time = 2f;

    public GameObject lineManager;

    void OnEnable()
    {
        TextManager.DuckActive += MoveDuck;
    }

    void OnDisable()
    {
        TextManager.DuckActive -= MoveDuck;
    }

    void Start()
    {
        MoveDuck();
    }

    void Update()
    {
    }

    void MoveDuck() // Move duck to the lowest avaiable spot;
    {
        if(mySpot > totalSpots)
        {
            return;
        }

        for(int i = totalSpots; i > 0; i--)
        {
            if(DuckGrid._gridFull[i] == false)
            {
                mySpot = i;
                DuckGrid._gridFull[i] = true;
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

