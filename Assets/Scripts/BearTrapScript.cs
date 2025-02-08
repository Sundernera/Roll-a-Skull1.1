using UnityEngine;
using UnityEngine.UI;

public class BearTrapScript : MonoBehaviour
{
    public int requiredPresses = 8;
    public int currentPresses = 0;
    bool isPlayerTrapped = false;
    GameObject trappedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trappedPlayer = other.gameObject;
            trappedPlayer.GetComponent<PlayerManagerV2>().FreezeMovement();
            isPlayerTrapped = true;
            currentPresses = 0;

            if (UIManagerV2.instance.progressBar)
            {
                UIManagerV2.instance.bearTrapUI.gameObject.SetActive(true);
                UIManagerV2.instance.progressBar.value = 0;
                UIManagerV2.instance.progressBar.maxValue = requiredPresses;
            }
        }
    }

    private void Update()
    {
        if (isPlayerTrapped && Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            if (UIManagerV2.instance.progressBar)
            {
                UIManagerV2.instance.progressBar.value = currentPresses;
            }

            if (currentPresses >= requiredPresses)
            {
                ReleasePlayer();
            }
        }
    }

    void ReleasePlayer()
    {
        if (trappedPlayer != null)
        {
            {
                trappedPlayer.GetComponent<PlayerManagerV2>().UnfreezeMovement();
                isPlayerTrapped = false;
                trappedPlayer = null;

                if (UIManagerV2.instance.progressBar)
                {
                    UIManagerV2.instance.bearTrapUI.gameObject.SetActive(false);
                }
            }

            Destroy(gameObject);
        }
    }
}

