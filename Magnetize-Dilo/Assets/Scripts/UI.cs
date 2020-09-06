using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public UnityEvent onPause;
    public UnityEvent onResume;
    public UnityEvent onFinish;
    public UnityEvent onGameOver;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        onPause.Invoke();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        onResume.Invoke();
    }

    public void Finish()
    {
        Time.timeScale = 0;
        onFinish.Invoke();
    }

    public void GameOver()
    {
        onGameOver.Invoke();
    }
}
