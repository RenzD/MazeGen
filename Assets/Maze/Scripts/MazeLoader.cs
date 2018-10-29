using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MazeLoader : MonoBehaviour
{
    public int mazeRows, mazeColumns;
    public GameObject maze;
    public GameObject wall;
    public GameObject wall2;
    public GameObject floor;
    public GameObject dragon;
	public GameObject goal;
	public GameObject target;
    public float size = 2f;

	private float timeLeft = 5f;

    //private NavMeshSurface surface;
    private MazeCell[,] mazeCells;

    // Use this for initialization
    void Start()
    {

        InitializeMaze();

        MazeAlgorithm ma = new HuntAndKillMazeAlgorithm(mazeCells);
        ma.CreateMaze();

		int row = Random.Range (1, mazeRows);
		int col = Random.Range (1, mazeColumns);
		Vector3 temp = new Vector3(row * 6 ,0f, col * 6);
		goal.transform.position = temp;
		dragon.transform.position = new Vector3 (mazeRows * size, dragon.transform.position.y , mazeColumns * size);
        BakeNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			int row = Random.Range (1, mazeRows);
			int col = Random.Range (1, mazeColumns);

			target.transform.position = new Vector3 (row * size, 0, col * size);
			timeLeft = 5f;
		}
		if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.JoystickButton6)) {
			dragon.transform.position = new Vector3 (mazeRows * size, dragon.transform.position.y , mazeColumns * size);
		}
    }

    private void InitializeMaze()
    {

        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                // For now, use the same wall object for the floor!
                mazeCells[r, c].floor = Instantiate(floor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                mazeCells[r, c].floor.transform.parent = maze.transform;
                mazeCells[r, c].floor.AddComponent(typeof(NavMeshSourceTag));
                

                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall2, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                    mazeCells[r, c].westWall.transform.parent = maze.transform;
                    for (int i = 0; i < mazeCells[r, c].westWall.transform.childCount; ++i)
                    {
                        GameObject go = mazeCells[r, c].westWall.transform.GetChild(i).gameObject;
                        go.AddComponent(typeof(NavMeshSourceTag));
                    }
                }

                mazeCells[r, c].eastWall = Instantiate(wall2, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;
                mazeCells[r, c].eastWall.transform.parent = maze.transform;
                for (int i = 0; i < mazeCells[r, c].eastWall.transform.childCount; ++i)
                {
                    GameObject go = mazeCells[r, c].eastWall.transform.GetChild(i).gameObject;
                    go.AddComponent(typeof(NavMeshSourceTag));
                }

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                    mazeCells[r, c].northWall.transform.parent = maze.transform;
                    for (int i = 0; i < mazeCells[r, c].northWall.transform.childCount; ++i)
                    {
                        GameObject go = mazeCells[r, c].northWall.transform.GetChild(i).gameObject;
                        go.AddComponent(typeof(NavMeshSourceTag));
                    }
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                mazeCells[r, c].southWall.transform.parent = maze.transform;
                for (int i = 0; i < mazeCells[r, c].southWall.transform.childCount; ++i)
                {
                    GameObject go = mazeCells[r, c].southWall.transform.GetChild(i).gameObject;
                    go.AddComponent(typeof(NavMeshSourceTag));
                }
            }
        }
    }

    private void BakeNavMesh()
    {

    }
}
