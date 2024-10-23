using UnityEngine;
using UnityEngine.UI;
using norbertcUtilities.Extensions;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Manager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] Slider tempoSlider;
    [SerializeField] TextMeshProUGUI tempoDisplay;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Transform grid;
    [SerializeField] GameObject ghost;
    [SerializeField] Transform board;
    [SerializeField] Transform keyboard;

    [SerializeField] int xBoardOffset = 500;
    float keyboarStartY;

    const int CELL_X_SIZE= 100;
    public const int CELL_Y_SIZE = 30;
    const int MIN_TEMPO_CHANGE = 5;
    const float BOARD_HORIZONTAL_ALIGN = 18.75F;

    public static bool isPlaying;
    public static int tempo;

    Transform lastGridCell;
    AudioSource source;

    void Start()
    {
        Tempo_OnValueChange();

        lastGridCell = grid.GetChild(grid.childCount - 1);
        keyboarStartY = keyboard.localPosition.y;

        HorizontalSlider_OnValueChange();
        VerticalSlider_OnValueChange();
        sampleRate = AudioSettings.outputSampleRate;
        source = GetComponent<AudioSource>();
        source.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (!isPlaying)
                Play();
            else
                Stop();
        }

        if(isPlaying)
        {
            // if whole board is scrolled, stops playing
            if(lastGridCell.position.x < 70)
            {
                Stop();
            }
        }
    }

    public void Tempo_OnValueChange()
    {
        tempoSlider.SetValueWithoutNotify(ncUtilitiesExtensions.RoundTo(tempoSlider.value, MIN_TEMPO_CHANGE));
        tempoDisplay.text = $"Tempo:  {tempoSlider.value}";
        tempo = (int)tempoSlider.value;
    }

    public void HorizontalSlider_OnValueChange()
    {
        // round slider value and set board pos
        int roundedValue = ncUtilitiesExtensions.RoundTo(horizontalSlider.value, CELL_X_SIZE);
        horizontalSlider.SetValueWithoutNotify(roundedValue);

        Vector2 boardPos = Vector2.zero;
        boardPos.x = -roundedValue - BOARD_HORIZONTAL_ALIGN + xBoardOffset;
        boardPos.y = board.localPosition.y;
        board.localPosition = boardPos;
    }

    public void VerticalSlider_OnValueChange()
    {
        // update board and keyboard scrolling
        int roundedValue = ncUtilitiesExtensions.RoundTo(verticalSlider.value, CELL_Y_SIZE);
        verticalSlider.SetValueWithoutNotify(roundedValue);

        Vector2 boardPos = Vector2.zero;
        boardPos.x = board.localPosition.x;
        boardPos.y = -roundedValue;
        board.localPosition = boardPos;
        keyboard.localPosition = new Vector3(keyboard.localPosition.x, keyboarStartY - roundedValue);
    }

    public void Play()
    {
        isPlaying = true;
        ghost.SetActive(false);
        ToggleScrollSliders();
        source.enabled = true;
    }

    public void Stop()
    {
        isPlaying = false;
        ghost.SetActive(true);
        board.localPosition = BoardScrolling.startPos;
        ToggleScrollSliders();
        VerticalSlider_OnValueChange();
        HorizontalSlider_OnValueChange();
        source.enabled = false;
    }

    void ToggleScrollSliders()
    {
        verticalSlider.gameObject.SetActive(!verticalSlider.gameObject.activeSelf);
        horizontalSlider.gameObject.SetActive(!horizontalSlider.gameObject.activeSelf);
    }

    public static double freq = 0;
    float amplitude = 0.5f;
    double phase;
    int sampleRate;
    private void OnAudioFilterRead(float[] data, int channels)
    {
        double phaseIncrement = freq / sampleRate;

        for (int sample = 0; sample < data.Length; sample += channels)
        {
            float value = Mathf.Sin((float)phase * 2 * Mathf.PI) * amplitude;
            phase = (phase + phaseIncrement) % 1;

            for (int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = value;
            }
        }
    }
}
