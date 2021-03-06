using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _animator;

    private bool playerRange;

    private Player _player;

    private NavMeshAgent _navMeshAgent;
    private bool playerDeath;
    
    //timer para nuestro ataques 
    private float timer;
    private float timerAttack=2f;

    private AudioSource audio;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        GameObject playerTMP= GameObject.Find("Player");

        if (playerTMP!=null)
        {
            _player = playerTMP.GetComponent<Player>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {/*
    if (Input.GetKeyDown(KeyCode.P))
        {
            _animator.SetTrigger("death");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _animator.SetTrigger("eat");
        }
    */
        if (playerDeath == false)
        {
            _navMeshAgent.SetDestination(_player.transform.position);

            timer += Time.deltaTime;
            if (timer >= timerAttack && playerRange&& playerDeath==false)
            {
                Attack();    
            }    
        }

        
    }

    void Attack()
    {
        
        timer = 0f;
        if (_player.lives > 0)
        {
            _player.Attack();
        }
        else
        { 
            playerDeath = true;
            _animator.SetTrigger("eat");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRange = true;
          _animator.SetBool("attack",true);  
        }
        //print("choca con "+ collision.gameObject.tag);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRange = false;
            _animator.SetBool("attack",false );  
        }
       
    }

    public void takeDamage()
    {
        if (playerDeath == false)
        {
            audio.Play();
            _player.updateZombiesCount();
            _animator.SetTrigger("death");
            _navMeshAgent.enabled = false;
            playerDeath = true;    
        }
        
    }
    
}
