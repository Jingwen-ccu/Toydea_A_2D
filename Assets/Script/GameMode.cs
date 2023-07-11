using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {

    public ExecutorController PlayerLeft; //之後會改為Script_Controller
    public PrisonerController PlayerRight; //之後會改為Script_Controller

    //Function: Timer
    private float WorldTime = 120f;
    public Text Times;

    //Function: Health
    public Text LHealth;
    public Text RHealth;


    //Function: Cloth
    public GameObject LeftCloth;
    public GameObject RightCloth;
    float TargetDistance = 20f;
    float Duration = 1f;  // 總共移動的時間
    float Interval = 0.02f;  // 每次移動的間隔時間
    float Steps;  // 需要移動的次數
    float StepDistance;  // 每次移動的距離
    bool isClose = true;
    public GameObject LPlayerBoard;
    public GameObject RPlayerBoard;

    


    //Function: Video
    public GameObject Camera_Video;
    public GameObject RightVideo;
    public GameObject LeftVideo;

    //Woose Added: Call round start method
    public ExecutorController executorController;
    public PrisonerController prisonerController;


    // Start is called before the first frame update
    void Start() {

        //前置作業
        Steps = Duration / Interval;
        StepDistance = TargetDistance / Steps;

        //打開Cloth
        OpenCloth();

        //開始計時
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            OpenCloth();
        } else if(Input.GetKeyDown(KeyCode.W)) {
            CloseCloth();
        } else if(Input.GetKeyDown(KeyCode.E)) {
            PlayVideo(true);
        } else if(Input.GetKeyDown(KeyCode.R)) {
            PlayVideo(false);
        } else if(Input.GetKeyDown(KeyCode.T)) {
            StopVideo();
        }

    }

    void GameStart() {

    }

    //遊戲重新開始
    void GameInitiate() {

        //遊戲開始
        //Time.timeScale = 1f;

        executorController.OnRoundStart();
        prisonerController.OnRoundStart();
        BulletsWipe();


    }


    //When Hit Somebody
    public void HitPlayer(bool isHitRight) {

        //血量UI更新
        UpdateHealth();

        //初始化位置子彈
        GameInitiate();

        //遊戲暫停
        Time.timeScale = 0f;

        //Check遊戲結束了嗎
        //是：Call GameOver(int Winner)
        if (PlayerLeft.Health==0)
        {

        }
        else if(PlayerRight.Health==0)
        {

        }
        //不是：Call GameTurnRound()
        if(isHitRight) 
        {
            GameTurnRound(true);
        } 
        else 
        {
            GameTurnRound(false);

        }
    }


    //轉場
    void GameTurnRound(bool isHitRight) 
    {
        StartCoroutine(PlayShortVideo(isHitRight));
        //Woose Added: Call round start method
        
    }

    void GameOver(int Winner) 
    {

    }
    //Woose Added Delete Bullet
    public void BulletsWipe() 
    {

        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets) Destroy(bullet); //desstroy bullet
    }
    void OpenCloth() {

        StartCoroutine(OpeningCloth());
    }

    void CloseCloth() {
        StartCoroutine(ClosingCloth());
    }

    void PlayVideo(bool isRightWin) {
        Camera_Video.SetActive(true);

        if(isRightWin) {
            LeftVideo.SetActive(false);
            RightVideo.SetActive(true);
        } else {
            LeftVideo.SetActive(true);
            RightVideo.SetActive(false);
        }

    }

    void StopVideo() {
        Camera_Video.SetActive(false);
        RightVideo.SetActive(false);
        LeftVideo.SetActive(false);
    }

    //當擊中目標時呼叫
    void UpdateHealth() {
        LHealth.text = PlayerLeft.Health.ToString();
        RHealth.text = PlayerRight.Health.ToString();
    }


    IEnumerator OpeningCloth() {
        //isSuccessOpen = false;
        if(isClose == false)
        {
            yield break;
        }

        isClose = false;
        float CurrentDistance = 0;

        while(CurrentDistance < TargetDistance) {
            LeftCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position += new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Step");
            yield return new WaitForSecondsRealtime(Interval);
        }

        
        
    }

    IEnumerator ClosingCloth() 
    {
        if (isClose == true)
        {
            yield break;
        }
        isClose = true;
        //isSuccessClose = false;
        float CurrentDistance = 0;

        while(CurrentDistance < TargetDistance) {
            LeftCloth.transform.position += new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Close");
            yield return new WaitForSecondsRealtime(Interval);
        }
        
        //isSuccessClose = true;
    }

    IEnumerator StartCountdown() {
        while(WorldTime > 0) {
            yield return new WaitForSeconds(1);
            WorldTime--;
            Times.text = WorldTime.ToString();
        }

        Debug.Log("Time out");
    }

    IEnumerator PlayShortVideo(bool isHitRight) {
        //Debug.Log("PLAY video");




        if(isHitRight) 
        {
            PlayVideo(false);
        } else {
            PlayVideo(true);

        }

        yield return new WaitForSecondsRealtime(1.3f);
        //Debug.Log("stop video");
        StopVideo();

        //關布幕
        CloseCloth();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        OpenCloth();


      


        //遊戲開始
        Time.timeScale = 1f;
    }
}
