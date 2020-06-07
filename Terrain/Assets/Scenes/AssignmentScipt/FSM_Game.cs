using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Game : MonoBehaviour
{
    private FPSAthlete ath;
    public enum FSM_STATE { RISE, MOVING, SUPERATHLETE, DEAD, GAMEOVER };
    public bool idle;

    [SerializeField]
    private FSM_STATE currentState;

    public FSM_STATE CurrentState
    {
        get
        { return currentState; }
        set
        {
            currentState = value;
            StopAllCoroutines();
            switch (currentState)
            {
                case FSM_STATE.RISE:
                    StartCoroutine(Rise(true));
                    break;
                case FSM_STATE.MOVING:
                    StartCoroutine(Moving(true));
                    break;
                case FSM_STATE.SUPERATHLETE:
                    StartCoroutine(SuperAthlete());
                    break;
                case FSM_STATE.DEAD:
                    StartCoroutine(Dead());
                    break;
                case FSM_STATE.GAMEOVER:
                    StartCoroutine(GameOver());
                    break;
            }
        }
    }

    public IEnumerator Rise(bool checkIn)
    {
        while (checkIn)
        {
            if (ath.stuck)
            {
                checkIn = false;
                break;
                currentState = FSM_STATE.MOVING;
            }
        }
        yield return null;
    }
   
    public IEnumerator Moving(bool checkIn)
    {
        while (checkIn)
        {
           if (ath.collide)
            {
               if (ath.Energy > 0)
               {
                    checkIn = false;
                    ath.position = true;
                    break;
                    currentState = FSM_STATE.DEAD;
               }
               else
               {
                    checkIn = false;
                    currentState = FSM_STATE.GAMEOVER;
               }
           }
        }
        
        yield return null;
    }
    public IEnumerator SuperAthlete()
    {
        yield return null;
    }
    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
    }
    public IEnumerator GameOver()
    {
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = FSM_STATE.RISE;
        ath = GetComponent<FPSAthlete>();
    }

    // Update is called once per frame
    void Update()
    {
        idle = ath.stuck;
    }
}
