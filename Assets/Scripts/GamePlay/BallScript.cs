using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour

{
	public GameObject CloudObjectPos,CloudObject;
	public List<GameObject> ProjectionDots;
	public GameObject ParentPredicitionDots;
	public int PrijectionDotIndex;
	//New 
    public  Rigidbody2D Knob,SlingShotConnectedPoint;
    public float StartTouchTime,EndTouchTime,TimeInterval;
    public float life = 2.0f, cooldown = 1.0f;
    private Vector2 TouchPos,MousePos;
    private float MaxDistance = 2f;
    public  int PitchNumber = 0;
    private bool CanInstantiateAndCeck,IsReleased;
    //
    public int BallForce; 
    [Range (100 , 500)]// Slider in Inspector Window
    //
    private float ForceMagnitude;
    public Vector2 Direction;
    public static int swipeCount;
    //
    public bool isPressed , IsPlayed;
    private Rigidbody2D rb;
    void Start()
    {
        SetProjectionDotFalse();
        IsReleased = false;
        CanInstantiateAndCeck = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        IsPlayed = true;
    }
    void OnMouseDown()
    {
      if(IsReleased == false)
      {
        isPressed = true;
        rb.isKinematic = true;
        Knob.isKinematic = true;
        Knob.gameObject.GetComponent<SpriteRenderer>().enabled = true;
      }
    }
    void OnMouseUp()
    {
       if(IsReleased == false)
      {
      	SetProjectionDotFalse();
        isPressed = false;
        Knob.isKinematic = true;
        ForceMagnitude = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
        Direction = (Knob.position - SlingShotConnectedPoint.position);
        StartCoroutine(Releasee());
      }
    }

    void Update()
    {
     
                  
    	 if(Input.touchCount > 0 && IsReleased == false && rb.isKinematic)
        {
        	Touch touch = Input.GetTouch(0);
          TouchPos = Camera.main.ScreenToWorldPoint(touch.position);

        	if(touch.phase == TouchPhase.Began)
        	{       	
        		Knob.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Knob.isKinematic = true;
        		rb.isKinematic = true;
        	}
          if(touch.phase == TouchPhase.Moved)
            {
            float distance = Vector2.Distance(TouchPos,SlingShotConnectedPoint.position);
          Vector2 Dir = TouchPos - SlingShotConnectedPoint.position;
          float angle = Mathf.Atan2(Dir.y,Dir.x)*Mathf.Rad2Deg;
          Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
          ParentPredicitionDots.transform.rotation = Quaternion.Slerp(ParentPredicitionDots.transform.rotation,rotation,1f);
          float distancePro = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
          if(distancePro > 1&& distancePro < 1.4f )
          {
          	    ProjectionDots[0].SetActive(true);
          	    ProjectionDots[1].SetActive(false);
          	    ProjectionDots[2].SetActive(false);
            	
            	PrijectionDotIndex = 0;
            	//Debug.Log("Distance Projection"+distancePro.ToString());
          }
          else if(distancePro > 1.4f && distancePro < 1.8f)
          {
          	    ProjectionDots[1].SetActive(true);
          	    ProjectionDots[0].SetActive(false);
          	    ProjectionDots[2].SetActive(false);
           		
           		PrijectionDotIndex = 1;
          }
          else if(distancePro > 1.8f)
          {
          	    ProjectionDots[2].SetActive(true);
          	    ProjectionDots[0].SetActive(false);
          	    ProjectionDots[1].SetActive(false);
          		
          		PrijectionDotIndex = 2;
          }
          else
          {
          	SetProjectionDotFalse();
          }
            if(distance>MaxDistance)
            {
               Vector2 direction = (TouchPos - SlingShotConnectedPoint.position).normalized;
               Knob.position = SlingShotConnectedPoint.position + direction * MaxDistance;
            }
            else
            {
                Knob.position = TouchPos;
            }

          }
        	if(touch .phase == TouchPhase.Ended)
        {
           SetProjectionDotFalse();
           float distance = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
           if(distance > 0.7)
           {
           rb.isKinematic = false;
           ForceMagnitude = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
           Direction = (Knob.position - SlingShotConnectedPoint.position);
           IsReleased = true;
           StartCoroutine(Releasee());
           }
           else
           {
               Knob.position = SlingShotConnectedPoint.position;

        	}
        	}
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////
   if(isPressed == true && rb.isKinematic == true&&IsReleased == false)
    {
         MousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
         //
          Vector2 Dir = MousePos - SlingShotConnectedPoint.position;
          float angle = Mathf.Atan2(Dir.y,Dir.x)*Mathf.Rad2Deg;
          Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
          ParentPredicitionDots.transform.rotation = Quaternion.Slerp(ParentPredicitionDots.transform.rotation,rotation,1f);
          //

          float distancePro = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
          if(distancePro > 1&& distancePro < 1.4f )
          {
          	    ProjectionDots[0].SetActive(true);
          	    ProjectionDots[1].SetActive(false);
          	    ProjectionDots[2].SetActive(false);
            	
            	PrijectionDotIndex = 0;
            	//Debug.Log("Distance Projection"+distancePro.ToString());
          }
          else if(distancePro > 1.4f && distancePro < 1.8f)
          {
          	    ProjectionDots[1].SetActive(true);
          	    ProjectionDots[0].SetActive(false);
          	    ProjectionDots[2].SetActive(false);
           		
           		PrijectionDotIndex = 1;
          }
          else if(distancePro > 1.8f)
          {
          	    ProjectionDots[2].SetActive(true);
          	    ProjectionDots[0].SetActive(false);
          	    ProjectionDots[1].SetActive(false);
          		
          		PrijectionDotIndex = 2;
          }
          else
          {
          	SetProjectionDotFalse();
          }
          //
          float distance = Vector2.Distance(MousePos,SlingShotConnectedPoint.position);

            if(distance > MaxDistance)
            {
               Vector2 direction = (MousePos - SlingShotConnectedPoint.position).normalized;
               Knob.position = SlingShotConnectedPoint.position + direction * MaxDistance;
            }
            else
            {
                Knob.position = MousePos;
            }
    }
}
	void SetProjectionDotFalse()
	{
		foreach (GameObject go in ProjectionDots)
         {
             go.SetActive(false);
         }
	}
    public int BounceCount;
    public AudioSource Bounce,Rim, BackBoard;

    void OnCollisionEnter2D(Collision2D col)
    {
    	PitchNumber++;
    	if(col.gameObject.name == "Floor Bottom")
    	{
    		GameObject c = Instantiate(CloudObject,CloudObjectPos.transform.position,CloudObjectPos.transform.rotation);
    		Destroy(c,1f);
    	}
        if(col.gameObject.name == "Floor Bottom" && IsPlayed)
        {
            IsPlayed = false;
            Bounce.Play();
            StartCoroutine(PlayedAudio());
        }
        if(col.gameObject.name == "Floor Bottom" && BounceCount>=3 )
        { 
                life -= Time.deltaTime;
                Color startColor = GetComponent<Renderer>().material.GetColor("_Color");                
                GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, life));
                StartCoroutine(DestroyNum());
                GamePlayController.TrichShotCount = 0;
        }
            else
                BounceCount++;
        if(col.gameObject.name == "Rim")
                 Rim.Play();
        if(col.gameObject.name == "BackBoard")
                BackBoard.Play();
      if(col.gameObject.name == "Floor Bottom"&& CanInstantiateAndCeck && BounceCount>=3 && BasketDection.TutorialInt>=1)
      {
          StartCoroutine(CheckAndInstantiate());
      }
    }
     IEnumerator CheckAndInstantiate()
    {
        BounceCount = 0;
        yield return new WaitForSeconds(2f);
        GamePlayController.CheckBasket = true;
        CanInstantiateAndCeck = false;
        if(GamePlayController.TrichShotCount == 0)
         Destroy(gameObject);
         else
         {
            GamePlayController.TrichShotCount = 0;
            Destroy(gameObject);
         }
      }
    
    IEnumerator DestroyNum()
    {
        yield return new WaitForSeconds(0.001f);
        
        if(BasketDection.TutorialInt<=0)
        {
          if(BasketDection.Score<=0)
          {
              TutorialController.CanInstantiate = true;
          }
              
          Destroy(gameObject);
        }
        
    }
    IEnumerator Releasee()
    {
       yield return new WaitForSeconds(0.17f);
       float distance = Vector2.Distance(Knob.position,SlingShotConnectedPoint.position);
       if(distance>1f)
       {
       Debug.Log("Added Force Magnitude " + ForceMagnitude.ToString());
       rb.isKinematic = false;
       Knob.gameObject.GetComponent<SpriteRenderer>().enabled = false;
       rb.AddForce(-1*Direction*ForceMagnitude *BallForce);
       IsReleased = true;
        IsReleased = true;
        Knob = null;
      }
         else
           {
               Knob.position = SlingShotConnectedPoint.position;

          }
    }
  
    IEnumerator PlayedAudio()
    {
        yield return new WaitForSeconds(0.75f);
        IsPlayed = true;
    }
}
