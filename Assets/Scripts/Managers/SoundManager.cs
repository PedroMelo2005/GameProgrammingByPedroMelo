using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public enum Sound {
        OnClick1_SFX,
        OnClick2_SFX,
        OnClose_SFX,
        OnMouseOver1_SFX,
        OnMouseOver2_SFX,
        PlayerAttack_SFX,
        EnemyAttack_SFX,
        Consumable_SFX,
        OpenChest_SFX,
        KillEnemy_SFX,
        SolvePuzzle_SFX,
        WinGame_SFX,
        LoseGame_SFX,
        Soundtrack_Music
    }

    public void PlaySound(Sound sound) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.I.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;
    }

}