using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
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
        if(collision.gameObject.name == "floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Item")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
        }
    }
}
