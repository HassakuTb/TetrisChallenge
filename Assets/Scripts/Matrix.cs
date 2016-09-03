using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="Matrix", menuName ="ScriptableObject/Matrix")]
public class Matrix : ScriptableObject{

    public Cell[,] cells;

    public void Init() {
        cells = new Cell[18, 28];

        for (int x = 0; x < 18; ++x) {
            for (int y = 0; y < 28; ++y) {
                cells[x, y] = ScriptableObject.CreateInstance<Cell>();
                cells[x, y].hasBlock = false;
                cells[x, y].isWall = false;
                cells[x, y].isDeadLine = false;
            }
        }

        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 28; ++y) {
                cells[x, y].hasBlock = true;
                cells[x, y].isWall = true;
                cells[x, y].block = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Block_B"));

                cells[x, y].block.transform.position = new Vector3(x, y, 0);
            }
        }

        for (int x = 14; x < 18; ++x) {
            for (int y = 0; y < 28; ++y) {
                cells[x, y].hasBlock = true;
                cells[x, y].isWall = true;
                cells[x, y].block = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Block_B"));
                cells[x, y].block.transform.position = new Vector3(x, y, 0);
            }
        }

        for (int x = 4; x < 14; ++x) {
            for (int y = 0; y < 4; ++y) {
                cells[x, y].hasBlock = true;
                cells[x, y].isWall = true;
                cells[x, y].block = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Block_B"));
                cells[x, y].block.transform.position = new Vector3(x, y, 0);
            }
            cells[x, 24].isDeadLine = true;
            GameObject deadline = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/DeadLine"));
            deadline.transform.position = new Vector3(x, 24, 1);
        }
    }
}
