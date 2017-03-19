using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    public static float Loudness;

    public enum VolumeType
    {
        VolumeRMS,
        VolumePeak
    }

    public VolumeType volumeType;
    [Range(0, 100)]
    public float sensibility = 50;

    #region Private members

    private string device;
    private AudioClip clipRecord = new AudioClip();
    private const int sampleWindow = 128;
    private bool isInitialized;

    #endregion

    #region Microphone Methods

    /// <summary>
    /// Microphone Initialization
    /// </summary>
    private void InitMic()
    {
        if (device == null)
        {
            device = Microphone.devices[0];
        }
        clipRecord = Microphone.Start(device, true, 999, 44100);
    }

    /// <summary>
    /// Stop the microphone
    /// </summary>
    private void StopMicrophone()
    {
        Microphone.End(device);
    }

    /// <summary>
    /// Get data from microphone into audioclip
    /// </summary>
    /// <returns>Volume RMS</returns>
    private float VolumeRMS()
    {
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(device) - (sampleWindow + 1); // null means the first microphone
        if (micPosition < 0)
        {
            return 0;
        }
        clipRecord.GetData(waveData, micPosition);

        var sum = 0f;

        // Getting the average on the last 128 samples
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += waveData[i] * waveData[i];
        }

        return Mathf.Sqrt(sum / sampleWindow);
    }

    /// <summary>
    /// Get data from microphone into audioclip
    /// </summary>
    /// <returns>Max Level</returns>
    private float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(device) - (sampleWindow + 1); // null means the first microphone
        if (micPosition < 0)
        {
            return 0;
        }
        clipRecord.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    #endregion

    #region Unity Events

    void Update()
    {
        if (volumeType == VolumeType.VolumePeak)
        {
            // levelMax equals to the highest normalized value power 2, a small number because < 1
            // pass the value to a static var so we can access it from anywhere
            Loudness = LevelMax() * sensibility;
        }
        else
        {
            Loudness = VolumeRMS() * sensibility;
        }
    }

    /// <summary>
    /// Start mic when scene starts
    /// </summary>
    void OnEnable()
    {
        InitMic();
        isInitialized = true;
    }

    /// <summary>
    /// Stop mic when loading a new level or quit application
    /// </summary>
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }

    /// <summary>
    /// Make sure the mic gets started & stopped when application gets focused
    /// </summary>
    /// <param name="focus"></param>
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");
            if (!isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            isInitialized = false;
        }
    }

    #endregion
}
