using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using System;

public class BallController : MonoBehaviour
{
    public static event Action OnBallDrawn;
    public static event Action OnBallRevoked;

    public Transform parentBack;
    public Transform parentFront;
    public Transform spawnerPointBallsFinal;
    [Space]
    public List<Transform> positionBallsGrid;
    public List<Transform> spawnerPointBallsStart;
    [Space]
    public ballAnimGlobeController animeGlobe;
    public BigBall bigBall;
    public Ball ball;
    public List<Ball> BallsSelected;
    [Space]
    public int numberBall;
    public int ballCountSpawn;
    public bool isLoop = false;
    public float timeBetweenBalls = 2f;

    private void Start()
    {
        animeGlobe = FindObjectOfType<ballAnimGlobeController>();
    }

    #region ShowBalls
    public IEnumerator ShowBallDrawn(int _number)
    {
        numberBall = _number;
        animeGlobe.SetCurrentBall(numberBall);
        while (animeGlobe.isFinishMovement == false)
        {
            yield return null;
        }
        bigBall.SetInfoInBigBall(numberBall);
        OnBallDrawn?.Invoke();
        if (BallsSelected.Count < 5 && !isLoop)
        {
            StartCoroutine(SpawnerBall());
        }
        else
        {
            isLoop = true;
        }
        if (isLoop)
        {
            StartCoroutine(SpawnerBallLoop());
        }
    }

    public void UpdateScreenAfterRevoked(int _number)
    {
        if (_number != 0)
            bigBall.SetInfoInBigBall(_number, false);
        else
            bigBall.SetBgBallWithLogo();
        
        OnBallRevoked?.Invoke();
        
        if (GameManager.instance.globeScriptable.ballsDrawn.Count >= 5)
        {
            for (int i = 0; i < BallsSelected.Count; i++)
            {
                BallsSelected[i].SetInfoBall(GameManager.instance.globeScriptable.ballsDrawn[GameManager.instance.globeScriptable.ballsDrawn.Count - (BallsSelected.Count - i)]);
            }
        }
        else if (GameManager.instance.globeScriptable.ballsDrawn.Count < 5)
        {
            int index = BallsSelected.Count - 1;
            Destroy(BallsSelected[index].gameObject, 0.3f);
            BallsSelected.Remove(BallsSelected[index]);
            ballCountSpawn--;

            isLoop = false;
            
            if(BallsSelected.Count==0)
            {
                GameManager.instance.globeScriptable.indexBalls = 0;
            }
        }
    }
    #endregion

    #region MovementBall
    public IEnumerator SpawnerBall()
    {
        Ball inst = Instantiate(ball, spawnerPointBallsStart[0].position, Quaternion.identity);
        BallsSelected.Add(inst);
        inst.transform.SetParent(parentBack);
        inst.transform.localScale = Vector3.one;
        inst.SetInfoBall(numberBall);
        SetBallWinner();
        yield return new WaitForSeconds(0.5f);
        MovementOnSpawner(inst);
    }
    //Após mostrar as 5 primeira bolas sorteadas, essa função cria um loop para exibir seguinte bolas sorteadas.
    public IEnumerator SpawnerBallLoop()
    {
        Ball inst = Instantiate(ball, spawnerPointBallsStart[0].position, Quaternion.identity);
        BallsSelected.Add(inst);
        inst.transform.SetParent(parentBack);
        inst.transform.localScale = Vector3.one;
        inst.SetInfoBall(numberBall);
        BallsSelected[0].ExitBall(spawnerPointBallsFinal, spawnerPointBallsStart[0], parentBack);
        BallsSelected[0].RotateLoop(360);
        yield return new WaitForSeconds(0.2f);
        BallsSelected.Remove(BallsSelected[0]);
        SetBallWinner();
        yield return new WaitForSeconds(0.3f);
        MovementOnSpawnerLoop(inst);
    }
    private void MovementOnSpawner(Ball instance)
    {
        timeBetweenBalls = 2f;
        instance.RotateLoop(-360);
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, instance.transform.DOMove(spawnerPointBallsStart[1].position, 0.3f).OnComplete(() =>
        {
            instance.SetSize(_delay: 0f);
            instance.transform.SetParent(parentFront);
            instance.transform.DOMove(instance.transform.position, 0.2f);
        }));

        seq.Insert(1, instance.transform.DOMove(spawnerPointBallsStart[2].position, 0.2f).OnComplete(() =>
        {
            instance.MoveBallAtFinalPos(positionBallsGrid[ballCountSpawn].position);
            ballCountSpawn++;
            if (ballCountSpawn < 5)
            {
                instance.RotateLoop(360);
            }
        }));
    }
    private void MovementOnSpawnerLoop(Ball instance)
    {
        timeBetweenBalls = 1.5f;
        instance.RotateLoop(-360);
        Sequence seq = DOTween.Sequence();
        seq.Insert(0, instance.transform.DOMove(spawnerPointBallsStart[1].position, 0.3f).OnComplete(() =>
        {
            instance.SetSize();
            instance.transform.SetParent(parentFront);

        }));
        seq.Insert(1, instance.transform.DOMove(spawnerPointBallsStart[2].position, 0.2f).SetDelay(0.2f).OnComplete(() =>
        {
            instance.MoveBallAtFinalPos(positionBallsGrid[ballCountSpawn - 1].position);
            Invoke("CallWinnerScreen", 2f);

        }));
        for (int i = 0; i < positionBallsGrid.Count - 1; i++)
        {
            BallsSelected[i].MoveBallAtFinalPos(positionBallsGrid[i].position);
            BallsSelected[i].RotateLoop(rotZ: 360, rotSpeed: 1f, loop: 1);
        }
    }
    #endregion

    public void SetBallWinner()
    {
        if (GameManager.instance.globeScriptable.Winners > 0)
        {
            bigBall.SetBallWinner();
            BallsSelected[BallsSelected.Count - 1].SetBallWinner();
        }
    }
}
