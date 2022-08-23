using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTouch : MonoBehaviour
{
    public static GameObject Player;
    bool isPressed;
      void OnMouseDown()
    {
        isPressed = true;
    }

    public GameObject MoveObject;
    void Start()
    {
      GetComponent<Rigidbody2D>().position = new Vector2(Player.transform.position.x,Player.transform.position.y-0.3f);   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0||isPressed)
        {
            Destroy(MoveObject);
            Destroy(gameObject);
        }
           StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
      {

        yield return new WaitForSeconds(1.5f);
        TutorialController.CanInstantiateFingerTouch = true;
        Destroy(gameObject);
        }
}
