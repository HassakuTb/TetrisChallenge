using UnityEngine;
using System.Collections.Generic;

public class Tetrimino : MonoBehaviour{

    public TeteriminoData data;

    public int xPos;

    public float offset;

    private float groundedTime;

    private Matrix matrix { get; set; }

    private GameObject[] objects;

    public void Init(TeteriminoData data, Matrix matrix) {
        this.data = data;
        offset = 23f;
        xPos = 9 - data.size / 2;

        groundedTime = 0f;

        this.matrix = matrix;

        objects = new GameObject[4];
        int i = 0;
        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                objects[i] = (GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Block_R")));
                ++i;
            }
        }
    }

    public void PositionSync() {
        int i = 0;
        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                objects[i].transform.position = new Vector3(x + xPos, y + offset, 0);
                ++i;
            }
        }
    }

    public void HandleInput() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            TurnRight();
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            TurnRight();
        }
    }

    private bool IsGrounded(float speed) {

        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                if (matrix.cells[x + xPos, y + Mathf.FloorToInt(offset - speed * Time.deltaTime)].hasBlock) return true;
            }
        }

        return false;
    }

    public bool FallDown() {

        if (Input.GetKey(KeyCode.DownArrow)) {
            if (IsGrounded(10f)) {
                PutMino();
                return true;
            }
            else {
                offset -= 10f * Time.deltaTime;
            }

        }
        else {
            if (IsGrounded(1f)) {
                groundedTime += Time.deltaTime;
                if (groundedTime >= 0.5f) {
                    PutMino();
                    return true;
                }
            }
            else {
                offset -= 1f * Time.deltaTime;
            }
        }

        return false;
    }

    public void PutMino() {

        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                Cell target = matrix.cells[x + xPos, y + Mathf.FloorToInt(offset)];
                target.hasBlock = true;
                target.block = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Block_G"));
            }
        }

        foreach (GameObject go in objects) {
            GameObject.Destroy(go);
        }
    }

    private void MoveLeft() {
        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                if (matrix.cells[x + xPos - 1, y + Mathf.CeilToInt(offset - 0.05f)].hasBlock) return;
                if (matrix.cells[x + xPos - 1, y + Mathf.CeilToInt(offset) - 1].hasBlock) return;
            }
        }
        xPos--;
    }

    private void MoveRight() {
        for (int x = 0; x < data.size; ++x) {
            for (int y = 0; y < data.size; ++y) {
                if (!data.actual[x, y]) continue;
                if (matrix.cells[x + xPos - 1, y + Mathf.CeilToInt(offset - 0.05f)].hasBlock) return;
                if (matrix.cells[x + xPos + 1, y + Mathf.CeilToInt(offset) - 1].hasBlock) return;
            }
        }
        xPos++;
    }

    private void TurnRight() {
        TeteriminoData tmpData = ScriptableObject.Instantiate<TeteriminoData>(data);
        tmpData.TurnRight();

        for (int x = 0; x < tmpData.size; ++x) {
            for (int y = 0; y < tmpData.size; ++y) {
                if (!tmpData.actual[x, y]) continue;
                if (matrix.cells[x + xPos, y + Mathf.CeilToInt(offset)].hasBlock) return;
                if (matrix.cells[x + xPos, y + Mathf.CeilToInt(offset) - 1].hasBlock) return;
            }
        }

        data.TurnRight();
    }

    private void TurnLeft() {
        TeteriminoData tmpData = ScriptableObject.Instantiate<TeteriminoData>(data);
        tmpData.TurnLeft();

        for (int x = 0; x < tmpData.size; ++x) {
            for (int y = 0; y < tmpData.size; ++y) {
                if (!tmpData.actual[x, y]) continue;
                if (matrix.cells[x + xPos, y + Mathf.CeilToInt(offset)].hasBlock) return;
                if (matrix.cells[x + xPos, y + Mathf.CeilToInt(offset) - 1].hasBlock) return;
            }
        }

        data.TurnLeft();
    }


}
