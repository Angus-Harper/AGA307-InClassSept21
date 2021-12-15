using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] enemyHits;
    public AudioClip[] footsteps;

    public AudioClip GetEnemyHit()
    {
        return enemyHits[Random.Range(0, enemyHits.Length)];
    }

    public AudioClip GetFootsteps()
    {
        return footsteps[Random.Range(0, footsteps.Length)];
    }
}
