using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource deathSound;

    public void PlayDeathSound() => deathSound.Play();
}
