using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(2);

            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(0);

            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(1);
            }
        }

    }
}
