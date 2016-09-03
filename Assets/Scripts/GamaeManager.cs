using UnityEngine;
using System.Linq;

public class GamaeManager : MonoBehaviour {

    public Matrix matrix;
    public Emitter emitter;

	private bool InGame { get; set; }

    private Tetrimino CurrentMino { get; set; }
    
    protected void Awake() {
        InGame = false;
    }

    protected void Update() {
        if (InGame) {
            InGameProcess();
        }
        else {
            OutofGameProcess();
        }
    }

    private void InGameProcess() {
        if(CurrentMino == null) {
            EmitMino();
        }

        CurrentMino.HandleInput();
        if (CurrentMino.FallDown()) {

            DeleteLineAll();
            PutPositionSync();

            GameObject.Destroy(CurrentMino.gameObject);
            CurrentMino = null;

            if (IsGameOver()) {
                InGame = false;
            }
            return;
        }

        CurrentMino.PositionSync();

    }

    private void OutofGameProcess() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            InGame = true;
            DeleteAll();
            matrix.Init();
        }
    }

    private void EmitMino() {
        GameObject CuurentObject = new GameObject();
        CurrentMino = CuurentObject.AddComponent<Tetrimino>();
        CurrentMino.Init(emitter.RandomMinoData(), matrix);
        CurrentMino.PositionSync();
    }

    private void DeleteAll() {
        FindObjectsOfType<GameObject>().Where(x => x.tag != "MainCamera" && x.tag != "GameManager").ToList().ForEach(x => GameObject.Destroy(x));
        CurrentMino = null;
    }

    private bool IsGameOver() {
        return matrix.cells.Cast<Cell>().Where(x => x.isDeadLine).Where(x => x.hasBlock).Count() > 0;
    }

    private void DeleteLineAll() {
        for (int y = 4; y < 24; ++y) {
            DeleteLine(y);
        }
    }

    private void DeleteLine(int y) {
        for (int x = 4; x < 14; ++x) {
            if (!matrix.cells[x, y].hasBlock) return;
        }

        for(int x = 4; x < 14; ++x) {
            GameObject.Destroy(matrix.cells[x, y].block);
        }

        for(int ny = y; ny < 24; ny++) {
            for (int x = 4; x < 14; ++x) {
                matrix.cells[x, ny].hasBlock = matrix.cells[x, ny + 1].hasBlock;
                matrix.cells[x, ny].block = matrix.cells[x, ny + 1].block;
            }
        }

        for (int x = 4; x < 14; ++x) {
            matrix.cells[x, 24].hasBlock = false;
            matrix.cells[x, 24].block = null;
        }
    }

    private void PutPositionSync() {

        for (int y = 0; y < 28; y++) {
            for (int x = 0; x < 18; ++x) {
                if (!matrix.cells[x, y].hasBlock) continue;
                matrix.cells[x, y].block.transform.position = new Vector3(x, y, 0);
            }
        }
    }
}
