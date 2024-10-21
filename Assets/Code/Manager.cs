using UnityEngine;
using UnityEngine.UI;
using norbertcUtilities.Extensions;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] Slider tempoSlider;
    [SerializeField] TextMeshProUGUI tempoDisplay;
    [SerializeField] Slider horizontalSlider;

    [SerializeField] Transform grid;
    [SerializeField] GameObject ghost;
    [SerializeField] Transform board;

    public static bool isPlaying;
    public static int tempo;

    Transform lastGridCell;

    void Start()
    {
        Tempo_OnValueChange();
        lastGridCell = grid.GetChild(grid.childCount - 1);
    }

    void Update()
    {
        //board.GetComponent<ScrollRect>().SetLayoutHorizontal();
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
        tempoSlider.SetValueWithoutNotify(ncUtilitiesExtensions.RoundTo(tempoSlider.value, 5));
        tempoDisplay.text = $"Tempo:  {tempoSlider.value}";
        tempo = (int)tempoSlider.value;
    }

    public void HorizontalSlider_OnValueChange()
    {
        // round slider value and set board pos
        int roundedValue = ncUtilitiesExtensions.RoundTo(horizontalSlider.value, 100);
        horizontalSlider.SetValueWithoutNotify(roundedValue);

        Vector2 boardPos = Vector2.zero;
        boardPos.x = -roundedValue;
        boardPos.y = board.localPosition.y;
        board.localPosition = boardPos;
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
