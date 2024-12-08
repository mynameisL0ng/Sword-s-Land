using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    public AudioClip[] monsterAudio;

    enum StateInfo { TAKEHIT, DEATH }
    string stateInfo;
    string previousState;

    private AudioSource audioSource;
    private Animator animator;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.enabled = false;
    }

    void Update()
    {
        MonsterVFX();
    }
    private void MonsterVFX()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit"))
        {
            stateInfo = StateInfo.TAKEHIT.ToString();
        }
        else
            stateInfo = "";
        if (stateInfo != previousState)
        {
            StateAudioClips();
            previousState = stateInfo;
        }
    }
    private void StateAudioClips()
    {
        if (stateInfo == "") return;

        for(int i = 0; i < monsterAudio.Length; i++)
        {
            audioSource.clip = monsterAudio[i];
            audioSource.enabled = true;
            audioSource.Play();
            break;
        }
    }
}
