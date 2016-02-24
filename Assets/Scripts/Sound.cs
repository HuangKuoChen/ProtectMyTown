using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    public static Sound Instance;
    public AudioSource birdDie, shot, explosion, reflect, countdown, background, fight, win;

    // Use this for initialization
    void Start () {
        Instance = this;
	}

    public void playBirdDie()
    {
        birdDie.Play();
    }

    public void playExplosion()
    {
        explosion.Play();
    }

    public void playShot()
    {
        shot.Play();
    }

    public void playReflect()
    {
        reflect.Play();
    }

    public void playCountdown()
    {
        countdown.Play();
    }

    public void playBg()
    {
        background.Play();
    }

    public void stopBg()
    {
        background.Stop();
    }

    public void playFight()
    {
        fight.Play();
    }

    public void stopFight()
    {
        fight.Stop();
    }

    public void playWin()
    {
        win.Play();
    }

    public void stopWin()
    {
        win.Stop();
    }
}
