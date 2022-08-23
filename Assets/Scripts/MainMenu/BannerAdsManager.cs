using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Proyecto26;

public class BannerAdsManager : MonoBehaviour
{
	/*private AdsInfo adsinfo;
	private bool CanCallSetImage;
	public Image BannerAdHolder;
	private int adIndex;
	public string FirebaseUrlString;
	void Update()
	{
		if(CanCallSetImage==true)
			 StartCoroutine( LoadAndSetImage());

    if(adIndex==0)
      GetAdIndex();
	}
	void Start()
	{
    adIndex = 0;
		CanCallSetImage = true;
    GetAdIndex();
   }

   public void GetAdIndex()
   {
    if(Application.internetReachability != NetworkReachability.NotReachable)
        {
          Debug.Log("Internet Available");
          BannerAdHolder.enabled = false;
          RestClient.Get<int>(FirebaseUrlString + "AdCountHolder.json").Then(response => 
          {
            Debug.Log("Get Ad MaxCount");
            int maxadCount = response+1;
            Debug.Log("AdMaxCount"+response.ToString());
            if(maxadCount!=null)
            {
             adIndex = Random.Range(1,maxadCount);
               GetAdDetails();
            }

            
          });
         
       }
       else
       {
       BannerAdHolder.enabled = false;
        Debug.Log("Internet UnAvailable");
       }
   }
   public void GetAdDetails()
   {
      Debug.Log("Getting Details......");
   		RestClient.Get<AdsInfo>(FirebaseUrlString + adIndex +".json").Then(response => 
          {
          	adsinfo = response;
          });
      Debug.Log("Got Details........");
   		ShowBannerAd();
   }

   public void ShowBannerAd()
   {
   	 BannerAdHolder.gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);
      Debug.Log("Showing BannerAd........");
   	 StartCoroutine( LoadAndSetImage());

   }
   void OnClicked()
   {
   	Application.OpenURL(adsinfo.GetConnectURl());
   }

   IEnumerator LoadAndSetImage() 
   {

    if(adsinfo!=null)
    {
      Debug.Log("Loading And Setting Images........");
   	CanCallSetImage = false;
   	string iamgeurl = adsinfo.GetAdImageURl();
   	string iamgeurlsecond = adsinfo.GetSecondImage();
    Texture2D texture = BannerAdHolder.canvasRenderer.GetMaterial().mainTexture as Texture2D;

    WWW www = new WWW(iamgeurl);
    WWW wwww = new WWW(iamgeurlsecond);
    yield return www;
    yield return wwww;

    BannerAdHolder.enabled = true;

    www.LoadImageIntoTexture(texture);
    www.Dispose();
    www = null;

    yield return new WaitForSeconds(6f);
    BannerAdHolder.sprite = null;
    yield return new WaitForSeconds(1f);

    wwww.LoadImageIntoTexture(texture);
    wwww.Dispose();
    wwww = null;
    yield return new WaitForSeconds(6f);
    CanCallSetImage = true;
  }
}/*/
}
