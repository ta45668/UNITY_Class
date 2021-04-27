using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GetMicrophone : MonoBehaviour
{

    public AudioSource m_audioSource; // 用來錄音的

    public Microphone microphone; // 麥克風參數
    public string[] device; // 麥克風設備名稱
    public int devicePos = 0; // 設備位置
    public int minFreq = int.MaxValue, maxFreq = int.MinValue; // 取得麥克風的最小值以及最大值頻率

    public float volume;
    public float[] microphoneSamles;

    public float Volume
    {
        get
        {
            if (Microphone.IsRecording(device[devicePos]))
            {
                // 取得的樣本數量
                int sampleSize = 128;
                float[] samples = new float[sampleSize];
                int startPosition = Microphone.GetPosition(device[devicePos]) - (sampleSize + 1);
                // 得到資料
                this.m_audioSource.clip.GetData(samples, startPosition);
                microphoneSamles = samples;

                // Getting a peak on the last 128 samples
                float levelMax = 0;
                for (int i = 0; i < sampleSize; i++)
                {
                    float wavePeek = samples[i];
                    if (levelMax < wavePeek)
                    {
                        levelMax = wavePeek;
                    }
                }
                return levelMax * 99;
            }
            return 0;
        }

    }

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        device = Microphone.devices; // get device name

        // To warn if have no microphone detected
        if (device.Length == 0) { Debug.LogWarning("No microphone input."); }
    }

    private void Start()
    {
        StartCaptureVoice();
    }

    private void Update()
    {
        volume = Volume;
    }

    private void OnDestroy()
    {
        StopCaptureVoice();
    }

    private void OnDisable()
    {
        StopCaptureVoice();
    }

    private void StartCaptureVoice()
    {
        // get microphone frequency
        Microphone.GetDeviceCaps(device[devicePos], out minFreq, out maxFreq);
        // set audio source
        m_audioSource.clip = Microphone.Start(device[devicePos], true, 3599, maxFreq);
        m_audioSource.loop = true;
        m_audioSource.timeSamples = Microphone.GetPosition(device[devicePos]);
    }

    private void StopCaptureVoice()
    {
        if (!Microphone.IsRecording(device[devicePos]))
        {
            return;
        }
        Microphone.End(device[devicePos]);
        m_audioSource.Stop();
    }

}