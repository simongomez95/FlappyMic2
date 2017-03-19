using UnityEngine;
using UnityEngine.UI;

public class LoudnessLabel : MonoBehaviour
{
    #region Private members

    private Text loudnessLabel;

    #endregion

    #region Unity Events

    void Awake()
    {
        loudnessLabel = GetComponent<Text>();
    }

    void Update()
    {
        loudnessLabel.text = "Loudness: " + MicrophoneInput.Loudness;
    }

    #endregion
}
