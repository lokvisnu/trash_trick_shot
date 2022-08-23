using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class MainMenuManager : MonoBehaviour
{
    public Text BestScoreText;
    public RectTransform Buttons,Scores;

  public void ShowLeaderboards()
   {
        if (PlayGamesPlatform.Instance.localUser.authenticated) 
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else 
        {
           Social.localUser.Authenticate ((bool success) => 
           {
                 if(success)
                 {
                     AddScoreToLeaderBoard();
                     PlayGamesPlatform.Instance.ShowLeaderboardUI();
                 }
            });
        }
    }

   public static void AddScoreToLeaderBoard()
   {
      if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportScore(PlayerPrefs.GetInt(GamePlayController.TrashTrickShotHighscoreId),GPGSIds.leaderboard_best_score,(bool success) =>{
                        Debug.Log("(Lollygagger) Leaderboard update success: " + success);
                });
            }
   }
   public GameObject hastagWindow;

   public void HasTag()
   {
        hastagWindow.SetActive(true);
   }
   public void back(){
     hastagWindow.SetActive(false);
   }
   public void SpgvarkGamesUrl(){
    Application.OpenURL("https://www.instagram.com/spgvark_games/");
   }
    
    void Start()
    {
        BestScoreText.text = PlayerPrefs.GetInt("com.pgvark.trashtrickshot.bestscore").ToString();
        Buttons.DOAnchorPos(new Vector2(0,70),0.3f);
        Scores.DOAnchorPos(new Vector2 (240,-35),0.3f);
        ///////////////////////////////////////////////
        PlayGamesClientConfiguration config = new 
        PlayGamesClientConfiguration.Builder()
        .Build();

        PlayGamesPlatform.DebugLogEnabled = true; 
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        if (!PlayGamesPlatform.Instance.localUser.authenticated) 
        {
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else if(PlayGamesPlatform.Instance.localUser.authenticated)
        {
            SignInCallback(true);
            AddScoreToLeaderBoard();
        }
      
    }
   
    public void SignInCallback(bool success) 
    {
        if (success) 
        {
             PlayGamesPlatform.Instance.LoadScores (
             GPGSIds.leaderboard_best_score,
             LeaderboardStart.PlayerCentered,
             1,
             LeaderboardCollection.Public,
             LeaderboardTimeSpan.AllTime,
         (LeaderboardScoreData data) => 
         {
             Debug.Log (data.Valid);
             Debug.Log (data.Id);
             Debug.Log (data.PlayerScore);
             PlayerPrefs.SetInt("com.pgvark.trashtrickshot.bestscore", int.Parse (data.PlayerScore.formattedValue));
             BestScoreText.text = PlayerPrefs.GetInt("com.pgvark.trashtrickshot.bestscore").ToString();
         });
            AddScoreToLeaderBoard();
        }
        else
        {
        Debug.Log("Cant Login Sorry");
        BestScoreText.text = PlayerPrefs.GetInt("com.pgvark.trashtrickshot.bestscore").ToString();
        }

    }

   
    public void GamePlayOnClick()
    {
        Buttons.DOAnchorPos(new Vector2(0,25),0.2f);
        Scores.DOAnchorPos(new Vector2(240,-65),0.2f);
        StartCoroutine(GamePlayScreen());
    	
    }
    public void rateButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.spgvark.trashTrickShot");
        }
    IEnumerator GamePlayScreen()
    {
        yield return new WaitForSeconds(0.35f);
        Buttons.DOAnchorPos(new Vector2(0,400),0.5f);
        Scores.DOAnchorPos(new Vector2(240,400),0.5f);
        yield return new WaitForSeconds(0.5f);
        
        if(PlayerPrefs.GetInt(BasketDection.TutorialIntPlayerPrefsName)<=0)
        Application.LoadLevel(2);
        else
        Application.LoadLevel(1);
        
    }
}

