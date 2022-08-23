using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasketDection : MonoBehaviour
{
	  public static int BasketCountDetect;
    public RectTransform PlusTwo,ComboShot;
    public Color TextColor,PreTextColor;
    public Vector2 LastPos;
    public static string TutorialIntPlayerPrefsName = "com.spgvark.trashtrickshot.tutorialint";
    public Text ScoreText;
    public static int Score;
    public static int TutorialInt;
    public GameObject BasketScored;
    public AudioSource BasketAudio;
    bool IsAnimationCompactShot;
    public static bool IsComboShot;
    void Start()
    {
        IsComboShot = false;
        IsAnimationCompactShot = false;
        TutorialInt = PlayerPrefs.GetInt(TutorialIntPlayerPrefsName);
        BasketCountDetect = 0;
        Score = 0;
        if(TutorialInt<=0)
        {
          BasketScored.SetActive(false);
        }
        else
        ScoreText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update() 
    {
        if(IsAnimationCompactShot)
         StartCoroutine(ScoreTextAnimation());
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.CompareTag("Player"))
       {
        TutorialInt = PlayerPrefs.GetInt(TutorialIntPlayerPrefsName);
             
                                  if(TutorialInt<=0)
                                  {
                                    Score++;
                                    BasketAudio.Play();
                                    BasketScored.SetActive(true);
                                    Destroy(col.gameObject);
                                  }
                                  else
                                  {
                                    GamePlayController.TrichShotCount++;
                                    BallScript ball =  col.gameObject.GetComponent<BallScript>();
                                    int pitch = ball.PitchNumber;
                                    Destroy(col.gameObject);
                                    if(pitch ==0)
                                    {
                                    
                                    if(GamePlayController.TrichShotCount<3)
                                    {
                                    //IsComboShot = true;
                                    Score+=2;  
                                    PlusTwo.gameObject.SetActive(true);
                                    StartCoroutine(TrickShotAnimation(PlusTwo));    
                                    } 
                                    else
                                    {
                                       IsComboShot = true;
                                       IsAnimationCompactShot = true;
                                       Score+=5;
                                       StartCoroutine(TrickShotAnimation(ComboShot));    
                                    }
                                    }
                                    else
                                    {
                                    //IsComboShot = true;
                                    Score++;
                                    }
                                    Vibration.Vibrate(50);
                                    BasketAudio.Play();
                                    BasketCountDetect = 0;
                                    StartCoroutine(BasketCheckDelay());     
                                    ScoreText.text =  Score.ToString();
                                    
                                                         
                                  }
       }
    } 
      IEnumerator BasketCheckDelay()
      {
        yield return new WaitForSeconds(0.75f);
        GamePlayController.BasketScored = true;
        GamePlayController.CheckBasket = true;
      }
      IEnumerator ScoreTextAnimation()
      {
        
         ScoreText.fontSize = Mathf.RoundToInt(Mathf.Lerp(ScoreText.fontSize,160,0.2f));
         yield return new WaitForSeconds(0.2f);
         ScoreText.fontSize = Mathf.RoundToInt(Mathf.Lerp(ScoreText.fontSize,120,0.2f));
         yield return new WaitForSeconds(0.2f);
         IsAnimationCompactShot = false;
         }
      IEnumerator TrickShotAnimation(RectTransform Rec)
    {
      Rec.gameObject.SetActive(true);
      LastPos = Rec.anchoredPosition;
      Rec.anchoredPosition = new Vector2(LastPos.x,LastPos.y-250);   
      Vector2 Pos = new Vector2(LastPos.x,LastPos.y+60);   
      Rec.DOAnchorPos(Pos,0.35f);
      yield return new WaitForSeconds(0.32f);
      Rec.DOAnchorPos(LastPos,0.25f);
      yield return new WaitForSeconds(1.5f);
      Rec.anchoredPosition = new Vector2(LastPos.x,LastPos.y-250); 
      yield return new WaitForSeconds(0.5f);
      Rec.gameObject.SetActive(false);
      Rec.DOAnchorPos(LastPos,0.35f);  
    }
}
