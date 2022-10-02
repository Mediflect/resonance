using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource deathSound;
    public AudioSource respawnSound;

    public void PlayDeathSound() => deathSound.Play();
    public void PlayRespawnSound() => respawnSound.Play();
}
