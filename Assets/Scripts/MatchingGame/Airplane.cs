using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField]
    private float speed;

    float height;
    public bool isRightPlane ;
    /*{
        get;
        set;
    };*/
    string inpt;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        //Debug.Log(height + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        if(isRightPlane){
            float move = Input.GetKey(KeyCode.UpArrow)? 1 : 0 ;
            move += Input.GetKey(KeyCode.DownArrow)? -1 : 0 ;
            transform.Translate(move * Vector2.up * Time.deltaTime * speed);
        }
        else{
            float move = Input.GetKey(KeyCode.W)? 1 : 0 ;
            move += Input.GetKey(KeyCode.S)? -1 : 0 ;
            transform.Translate(move * Vector2.up * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
    }


    /*public void Init(bool isRight)
    {
        isRightPlane = isRight;
        Vector2 pos = Vector2.zero;

        if (isRight)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;
            inpt = "PlaneRight";
        } else {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;
            inpt = "PlaneLeft";
        }

        transform.position = pos;
        transform.name = inpt;
    }*/
}
