using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class GamePlayController : MonoBehaviour
{
	//*** Public /****
  public int ScoreChangeDetect;
  public Vector2 AnchorPos;
  public int LeastScoreInt;
  public List<Vector2> PositionList;
  public List<Rigidbody2D> KnobRbList,ConnectedRbList; 
  public AudioSource TrickShotAudio;
  public List<GameObject> LifeList;
  public GameObject PlayerObject , CurrentGameObject;
  public GameObject GamePlayUI,GameOverUI;
  public Text CurretScoreText, BestScoreText;
  public RectTransform Lifes,Buttons,Scores,TrickShotGameObject;
  public int AdRandom;
  public bool CalledOnce;
  public Text ScoreText;
  public GameObject LoadingScreen;
  public Text LoadingText;

  ///***/// Public //**//*/**

////***//// Public Static ///*/*//

  public static GamePlayController instance;
  public static string TrashTrickShotHighscoreId = "com.pgvark.trashtrickshot.bestscore";
  public static bool CheckBasket;
  public static int LifeCountStatus;
  public static bool BasketScored;
  public static int TrichShotCount;

  //**/*/// Public Static /**/*///

  //***/**///Private *///*/*/

  private bool ShowedInterstitial , ShowInterstitialAd;
  private string FilePath;
  private bool isFocus = false;
  private bool isProcessing = false;

  //**/*/// Private /**/*///
 

          
  void ScreenShot()
  {
  	ScreenCapture.CaptureScreenshot("screenShot.png",1);
  }
//*/*/ In-Built Methods*////
void Pause()
{
	if(Time.timeScale!=0)
  		Time.timeScale = 0;
  	else
  		Time.timeScale=1;
}
  void Awake()
  {
    TrichShotCount = 0;
    if(instance==null)
    {
      instance = this;
    }
  }
 

    void Start()
    {

    	 ShowedInterstitial = false;
    	 ShowInterstitialAd = false;

    	 if(Random.Range(2,4) == 2 && Application.internetReachability != NetworkReachability.NotReachable)
    	 {
            ShowInterstitialAd = true;
            Debug.Log("Going To Show Interstitial Ad");
         }
         else
         	Debug.Log("Not Going To Show Interstitial Ad");

        LeastScoreInt = 0;
        AdRandom = Random.Range(0,4);
        CalledOnce = false;
    	Lifes.DOAnchorPos(new Vector2(4,-55),0.35f);
        ScoreChangeDetect = 0;
        LifeCountStatus = 2;  
        CheckBasket = false;
        InstantiatePlayer();
    }

     void Update()
    {
    	if(Input.GetKeyDown(KeyCode.A))
    	Pause();
    	if(Input.GetKeyDown(KeyCode.B))
    	ScreenShot();
    	if(CheckBasket == true)
  {
    
        if(BasketScored==true)
        {
          LeastScoreInt++;
          InstantiatePlayer();
      if(TrichShotCount>=3)
      {
      
      	if(!BasketDection.IsComboShot)
        {
        TrichShotCount = 0;
        BasketDection.Score+=3;
        ScoreText.text = BasketDection.Score.ToString();
        StartCoroutine(TrickShotAnimation());
        }
        else
        {
        	Vibration.Vibrate(80);
          BasketDection.IsComboShot = false;
        }
        	TrichShotCount = 0;

        if(LifeCountStatus != 2||LifeCountStatus < 2)
        {
             LifeCountStatus++;
             LifeList[LifeCountStatus].SetActive(true);
        }
        
      }
        }
        else if(LeastScoreInt > 0)
        {

           LifeList[LifeCountStatus].SetActive(false);
           LifeCountStatus-=1;
           BallScript.swipeCount=-1;

           if(LifeCountStatus<=-1)
           {
           
           }
           else
             InstantiatePlayer();
        }
        else
            InstantiatePlayer();

        CheckBasket =false;
        BasketScored = false;
    }
       if(LifeCountStatus<=-1)
       {
           Destroy(CurrentGameObject);
           GamePlayUI.SetActive(false);
           GameOverUI.SetActive(true);

           if(ShowInterstitialAd==false)
        {
        	Debug.Log("Not Show Interstitial");
           CurretScoreText.text = BasketDection.Score.ToString();
           int BestScore = PlayerPrefs.GetInt("com.pgvark.trashtrickshot.bestscore");
          
           if(CalledOnce!=true)
           {
           Scores.DOAnchorPos(new Vector2(15,49),0.35f);
           Buttons.DOAnchorPos(new Vector2(20,-126),0.35f);
           CalledOnce = true;
           }
           if(BasketDection.Score>BestScore)
           {
            PlayerPrefs.SetInt("com.pgvark.trashtrickshot.bestscore",BasketDection.Score);
            BestScoreText.text = BasketDection.Score.ToString();
            MainMenuManager.AddScoreToLeaderBoard();
           }
           else
            BestScoreText.text = BestScore.ToString();
         }
         else
         	ShowInterstitialAd = false;
      }
    
    
  }

  void InstantiatePlayer()
    {
      
      int random = GetRandomPos();
      Vector2 vector2 = PositionList[random];
      CurrentGameObject =  Instantiate(PlayerObject,vector2, Quaternion.identity);
      CurrentGameObject.GetComponent<Rigidbody2D>().isKinematic = true;
      CurrentGameObject.GetComponent<BallScript>().Knob = KnobRbList[random];
      CurrentGameObject.GetComponent<BallScript>().SlingShotConnectedPoint = ConnectedRbList[random];
      foreach(Rigidbody2D rrb in KnobRbList )
      {
        rrb.gameObject.GetComponent<SpriteRenderer>().enabled = false;
      }

    }

    int GetRandomPos()
    {
      int random;
      random = Random.Range(0,5);
      if(Level_Manager.IsLevel && random == 2)
      {
        while(random == 2)
        {
          random = Random.Range(0,5);
        }
      }
      return random; 
    }
  ////*/* In-Built Methods/*****\


  /////// /* Buttons /*
      IEnumerator GamePlayScreen()
  {
        yield return new WaitForSeconds(0.25f);
        Scores.DOAnchorPos(new Vector2(15,397),0.4f);
        yield return new WaitForSeconds(0.35f);
        Application.LoadLevel(1);
    }

    public void HomeButton()
    {
            Buttons.DOAnchorPos(new Vector2(20,-345),0.45f);
            Scores.DOAnchorPos(new Vector2(15,-15),0.2f);
            StartCoroutine(HomeScreen());
    }
     IEnumerator HomeScreen(){
        yield return new WaitForSeconds(0.25f);
        Scores.DOAnchorPos(new Vector2(15,397),0.4f);
        yield return new WaitForSeconds(0.35f);
        Application.LoadLevel(0);
    }
     public void Restart()
  {

            Buttons.DOAnchorPos(new Vector2(20,-345),0.45f);
            Scores.DOAnchorPos(new Vector2(15,-15),0.2f);
    		StartCoroutine(GamePlayScreen());
  }

  public void ShareScreeShotText()
{
  
      StartCoroutine (ShareTextInAnroid ());
   

}
IEnumerator takeScreenShot()
{
    string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
    string fileName = "ScreenShot" + timeStamp + ".png";
    FilePath = Application.persistentDataPath +"/"+ fileName;
    ScreenCapture.CaptureScreenshot(fileName);
    yield return new WaitForEndOfFrame(); 
}

  IEnumerator ShareTextInAnroid () 
  {
    yield return new WaitForEndOfFrame();
    var shareSubject = "I challenge you to beat my high score";
    var shareMessage = "I challenge you to beat my high score. " +
                       "Get the game from the link below. Cheers\n\n" +
                       "https://play.google.com/store/apps/details?id=com.spgvark.trashTrickShot";

    Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
	ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
	ss.Apply();

	string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
	File.WriteAllBytes( filePath, ss.EncodeToPNG() );
	
	Destroy( ss );
    new NativeShare().AddFile(filePath).SetSubject(shareSubject).SetText(shareMessage).Share();
  }
    ///* Buttons*/

  ///*/* Animations */
    IEnumerator TrickShotAnimation()
    {
      TrichShotCount = 0;
      TrickShotGameObject.gameObject.SetActive(true);
      Vector2 LastPos = TrickShotGameObject.anchoredPosition;
      Vector2 Poss = LastPos;
      TrickShotGameObject.anchoredPosition = new Vector2(LastPos.x,LastPos.y-250);
      Vector2 Pos = new Vector2(LastPos.x,LastPos.y+60);
      TrickShotGameObject.DOAnchorPos(Pos,0.35f);
      Vibration.Vibrate(65);
      TrickShotAudio.Play();
      yield return new WaitForSeconds(0.32f);
      TrickShotGameObject.DOAnchorPos(LastPos,0.25f);
      yield return new WaitForSeconds(1.5f);
      TrickShotGameObject.DOAnchorPos(new Vector2(LastPos.x,LastPos.y-250),0.5f);
      yield return new WaitForSeconds(0.5f);
      TrickShotGameObject.gameObject.SetActive(false);
      TrickShotGameObject.DOAnchorPos(Poss,0.35f);
    }
       
       
         
    
    
  



}
