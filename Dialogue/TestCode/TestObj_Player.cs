using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj_Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    Vector2 movement;
    public Obj_Journal testObj;
    public Obj_Journal testObj2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movement = Vector2.left;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            movement = Vector2.right;
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("GetKeyDown Keyboard T ");
            // 인터렉트함.
            // 인터페이스를 가지고 있다면 그 인터페이스를 실행함.
            if(testObj.TryGetComponent<IPrompt>(out IPrompt _prompt))
            {
                _prompt.SetPromptData();
                DialogueManager.Instance.prompt.PrintPrompt();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("GetKeyDown Keyboard Y ");
            // 인터렉트함.
            // 인터페이스를 가지고 있다면 그 인터페이스를 실행함.
            if (testObj2.TryGetComponent<IPrompt>(out IPrompt _prompt))
            {
                _prompt.SetPromptData();
                DialogueManager.Instance.prompt.PrintPrompt();
            }
        }
        rb.velocity = movement * speed * Time.deltaTime;
    }
}
