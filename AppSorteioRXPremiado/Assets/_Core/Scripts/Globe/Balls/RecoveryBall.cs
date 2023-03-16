using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryBall : MonoBehaviour
{

    [Header("Restore Balls")]
    public int ballCount = 5;
    public List<int> lastIndexBallsRaffle;

    void Start()
    {
        
    }

    public IEnumerator RestoreBalls(List<int> lastFiveBalls, List<int> _numbers)
    {
        for (int i = 0; i < lastFiveBalls.Count; i++)
        {
            //StartCoroutine(ShowBigBall(_numbers[lastFiveBalls[i]]);
            yield return new WaitForSeconds(2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
