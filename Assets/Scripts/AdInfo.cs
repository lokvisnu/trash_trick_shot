using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class AdsInfo
{

   public string AdImageurl;
   public string Connecturl;
   public string SecondImage;

    public AdsInfo(string AdImageurl,string Connecturl,string SecondImage)
   {
   	this.AdImageurl = AdImageurl;
   	this.Connecturl = Connecturl;
      this.SecondImage = SecondImage;
   }
   public string GetAdImageURl()
   {
   	return AdImageurl;
   }
   public string GetConnectURl()
   {
   	return Connecturl;
   }
   public void SetAdImageURL(string AdImageurl)
   {
   	this.AdImageurl = AdImageurl;
   }
   public void SetConnectURL(string Connecturl)
   {
   	this.Connecturl = Connecturl;
   }
   public string GetSecondImage()
   {
      return SecondImage;
   }
}
