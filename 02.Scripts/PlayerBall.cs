using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;
    AudioSource audio;
    bool isJump;
    Rigidbody rigid;

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();           
        audio = GetComponent<AudioSource>();     
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }     
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Point")
        {
            if(itemCount == manager.TotalItemCount)
            {
                SceneManager.LoadScene("Example1_"+(manager.stage+1).ToString());
            }
            else
            {
                SceneManager.LoadScene("Example1_" + manager.stage.ToString());
            }
        }

    }
    
}
