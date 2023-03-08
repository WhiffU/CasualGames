using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool hasGameFinished, canClick;

    public static GameManager instance;
    [SerializeField] private Button btnHome;


    int roll;

    public Board myBoard;
    List<Player> players;
    int currentPlayer;

    public Vector3[] positions;

    Dictionary<int, int> joints;

    Dictionary<Player, GameObject> pieces;

    public delegate void UpdateMessage(Player player);
    public event UpdateMessage message;

    [SerializeField] GameObject gamePiece;

    [SerializeField] Vector3 startPos;

    private void Start()
    {
        btnHome.onClick.AddListener(GoToMainMenu);

    }
    private void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canClick = true;
        hasGameFinished = false;
        currentPlayer = 0;

        SetUpPositions();
        SetUpLadders();

        myBoard = new Board(joints);
        players = new List<Player>();
        pieces = new Dictionary<Player, GameObject>();

        //Create players
        for (int i = 0; i < 2; i++) //Players number
        {
            players.Add((Player)i);
            GameObject temp = Instantiate(gamePiece);
            pieces[(Player)i] = temp;
            temp.transform.position = startPos;
            temp.GetComponent<Piece>().SetColors((Player)i);
        }
    }
    private void SetUpPositions()
    {
        positions = new Vector3[100];
        float diff = 0.45f;
        positions[0] = startPos;
        int index = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                positions[index] = new Vector3(positions[index - 1].x + diff,
                    positions[index - 1].y,
                    positions[index - 1].z);
                index++;
            }

            positions[index] = new Vector3(positions[index - 1].x,
                   positions[index - 1].y + diff,
                   positions[index - 1].z);
            index++;

            for (int j = 0; j < 9; j++)
            {
                positions[index] = new Vector3(positions[index - 1].x - diff,
                   positions[index - 1].y,
                   positions[index - 1].z);
                index++;
            }

            if (index == 100) return;
            positions[index] = new Vector3(positions[index - 1].x,
                 positions[index - 1].y + diff,
                 positions[index - 1].z);
            index++;
        }

    }
    private void SetUpLadders()
    {
        joints = new Dictionary<int, int>
        {
            {1,22 },
            {7,33},
            {19,76 },
            {28,8 },
            {31,67},
            {37,14 },
            {40,78 },
            {46,4 },
            {52,32 },
            {61,36 },
            {73,97 },
            {81,99 },
            {84,94 },
            {85,53 },
            {91,69 },
            {96,24 }
        };
    }
    private void Update()
    {
        if (hasGameFinished || !canClick)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousPos2D = new Vector2(mousPos.x, mousPos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousPos2D, Vector2.zero);
            if (!hit.collider) return;
            if (hit.collider.CompareTag("Dice"))
            {
                roll = 1 + Random.Range(0, 6);
                hit.collider.gameObject.GetComponent<Dice>().Roll(roll);
                canClick = false;
            }
        }
    }

    public void MovePiece()
    {
        List<int> result = myBoard.UpdateBoard(players[currentPlayer], roll);

        if (result.Count == 0)
        {
            canClick = true;
            currentPlayer = (currentPlayer + 1) % players.Count;
            message(players[currentPlayer]);
            return;
        }

        pieces[players[currentPlayer]].GetComponent<Piece>().SetMovement(result);
        canClick = true;

        if (result[result.Count-1] == 99)
        {
            players.RemoveAt(currentPlayer);
            currentPlayer %= currentPlayer;
            if (players.Count == 1) hasGameFinished = true;
            message(players[currentPlayer]);

            return;
        }
        currentPlayer = roll ==6?currentPlayer:(currentPlayer+ 1) % players.Count;
        message(players[currentPlayer]);

    }
}
