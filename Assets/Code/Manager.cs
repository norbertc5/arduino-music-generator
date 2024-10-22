using UnityEngine;
using UnityEngine.UI;
using norbertcUtilities.Extensions;
using TMPro;

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
    const int CELL_y_SIZE = 30;
    const int MIN_TEMPO_CHANGE = 5;
    const float BOARD_HORIZONTAL_ALIGN = 18.75F;

    public static bool isPlaying;
    public static int tempo;

    Transform lastGridCell;

    void Start()
    {
        Tempo_OnValueChange();

        lastGridCell = grid.GetChild(grid.childCount - 1);
        keyboarStartY = keyboard.localPosition.y;

        HorizontalSlider_OnValueChange();
        VerticalSlider_OnValueChange();
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
        int roundedValue = ncUtilitiesExtensions.RoundTo(verticalSlider.value, CELL_y_SIZE);
        verticalSlider.SetValueWithoutNotify(roundedValue);

        Vector2 boardPos = Vector2.zero;
        boardPos.x = board.localPosition.x;
        boardPos.y = -roundedValue;
        board.localPosition = boardPos;
        keyboard.localPosition = new Vector3(keyboard.localPosition.x, keyboarStartY - roundedValue);
    }

    public void Play()
    {
        print("play");
        isPlaying = true;
        ghost.SetActive(false);
    }

    public void Stop()
    {
        print("stop");
        isPlaying = false;
        ghost.SetActive(true);
        board.localPosition = BoardScrolling.startPos;
    }
}
