using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushRoomObject_HMJ : MonoBehaviour
{
    Vector3 minScale = new Vector3(0.7f, 0.7f, 0.7f);
    Vector3 maxScale = new Vector3(0.7f, 0.7f, 0.7f);

    float scaleTime = 0.0f;

    int playN = 4;
    bool bScaleMove = false;

    PlayerScaleState m_ePlayerScaleState;

    public enum PlayerScaleState
    {
        SmallState,
        BigState,
        PlayerScaleStateEnd
    }
    // Start is called before the first frame update
    void Start()
    {
        bScaleMove = false;

        minScale = new Vector3(80.0f * 0.7f, 80.0f * 0.7f, 80.0f * 0.7f);
        maxScale = new Vector3(80.0f * 1.5f, 80.0f * 1.5f, 80.0f * 1.5f);

        m_ePlayerScaleState = PlayerScaleState.PlayerScaleStateEnd;

        playN = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(bScaleMove)
        {
            scaleTime += Time.deltaTime;
            if (scaleTime > 1.0f)
            {
                --playN;
                if (m_ePlayerScaleState == PlayerScaleState.SmallState)
                    m_ePlayerScaleState = PlayerScaleState.BigState;
                else
                    m_ePlayerScaleState = PlayerScaleState.SmallState;
                scaleTime = 0.0f;
            }
            switch (m_ePlayerScaleState)
            {
                case PlayerScaleState.SmallState:
                    gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, minScale, scaleTime);
                    break;
                case PlayerScaleState.BigState:
                    gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, maxScale, scaleTime);
                    break;
                case PlayerScaleState.PlayerScaleStateEnd:
                    break;
            }
        }

        if (playN <= 0)
        {
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Player") && !bScaleMove)
        {
            bScaleMove = true;

            m_ePlayerScaleState = PlayerScaleState.BigState;
            scaleTime = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player") && !bScaleMove)
        {
            bScaleMove = true;

            m_ePlayerScaleState = PlayerScaleState.BigState;
            scaleTime = 0.0f;
        }
    }
}
