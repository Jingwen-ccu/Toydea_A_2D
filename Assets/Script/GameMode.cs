using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{

    public ExecutorController PlayerLeft; //����|�אּScript_Controller
    public PrisonerController PlayerRight; //����|�אּScript_Controller

    //Function: Timer
    private float WorldTime = 120f;
    public Text Time;

    //Function: Health
    public Text LHealth;
    public Text RHealth;


    //Function: Cloth
    public GameObject LeftCloth;
    public GameObject RightCloth;
    float TargetDistance = 20f;
    float Duration = 1f;  // �`�@���ʪ��ɶ�
    float Interval = 0.02f;  // �C�����ʪ����j�ɶ�
    float Steps;  // �ݭn���ʪ�����
    float StepDistance;  // �C�����ʪ��Z��

    // Start is called before the first frame update
    void Start()
    {
        Steps = Duration / Interval;
        StepDistance = TargetDistance / Steps;
        OpenCloth();

        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenCloth();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            CloseCloth();
        }
    }

    void GameStart()
    {

    }

    void GameInitiate()
    {

    }


    //When Hit Somebody
    void HitPlayer()
    {

    }

    void GameTurnRound()
    {

    }

    void GameOver(int Winner)
    {

    }

    void OpenCloth()
    {
        
        StartCoroutine(OpeningCloth());
    }

    void CloseCloth()
    {
        StartCoroutine(ClosingCloth());
    }

    //�������ؼЮɩI�s
    void UpdateHealth()
    {
        LHealth.text = PlayerLeft.Health.ToString();
        RHealth.text = PlayerRight.Health.ToString();
    }


    IEnumerator OpeningCloth()
    {
        float CurrentDistance = 0;

        while (CurrentDistance < TargetDistance)
        {
            LeftCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position += new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Step");
            yield return new WaitForSeconds(Interval);
        }
    }

    IEnumerator ClosingCloth()
    {
        float CurrentDistance = 0;

        while (CurrentDistance < TargetDistance)
        {
            LeftCloth.transform.position += new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Close");
            yield return new WaitForSeconds(Interval);
        }
    }

    IEnumerator StartCountdown()
    {
        while (WorldTime > 0)
        {
            yield return new WaitForSeconds(1);
            WorldTime--;
            Time.text = WorldTime.ToString();
        }

        Debug.Log("Time out");
    }
}