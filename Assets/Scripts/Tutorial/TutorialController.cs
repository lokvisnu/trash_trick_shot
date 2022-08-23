using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject PlayerObject,CurrentGameObject,FingerTouchGameObject,MoveObject,CurrentTouchFinger;
    public static bool CanInstantiate,CanInstantiateFingerTouch;
    public Rigidbody2D Knob,ConnectedPoint;
    bool isPressed;
    void Awake()
    {
        CanInstantiateFingerTouch = true;
        CurrentGameObject = Instantiate(PlayerObject,new Vector3(2.25f,-1.35f,0f),transform.rotation);
        CurrentGameObject.GetComponent<BallScript>().Knob = Knob;
        CurrentGameObject.GetComponent<BallScript>().SlingShotConnectedPoint = ConnectedPoint;
        FingerTouch.Player = CurrentGameObject;
    }
     void OnMouseDown()
    {
        isPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0||isPressed)
        {
            Destroy(MoveObject);
            Destroy(CurrentTouchFinger);
        }
        if(CanInstantiate)
        {
            CurrentGameObject = Instantiate(PlayerObject,new Vector3(2.25f,-1.35f,0f),transform.rotation);
            CurrentGameObject.GetComponent<BallScript>().Knob = Knob;
            CurrentGameObject.GetComponent<BallScript>().SlingShotConnectedPoint = ConnectedPoint;
            CanInstantiate = false;
        }
        if(CanInstantiateFingerTouch)
        {
            CurrentTouchFinger = Instantiate(FingerTouchGameObject);
            CurrentTouchFinger.GetComponent<FingerTouch>().enabled = true;
            CanInstantiateFingerTouch = false;
        }
    }
    public void ContinueToGame()
    {
        PlayerPrefs.SetInt(BasketDection.TutorialIntPlayerPrefsName,1);
        Application.LoadLevel(1);
    }
    public void CantUnderstand()
    {
    	Application.LoadLevel(2);
    }
}
