using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _animator;

    private bool playerRanger;

    private Player _player;

    private NavMeshAgent _navMeshAgent;
    private bool playerDeath;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
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
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _animator.SetTrigger("death");
        }
    }
}
