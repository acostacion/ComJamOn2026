using UnityEngine;

public class ScrollTexture : MonoBehaviour {
    [SerializeField]  private float _scrollSpeedX;
    [SerializeField]  private float _scrollSpeedY;

    private MeshRenderer _meshRenderer;

    void Start() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        _meshRenderer.material.mainTextureOffset =
            new Vector2(Time.realtimeSinceStartup * _scrollSpeedX, Time.realtimeSinceStartup * _scrollSpeedY);
    }
}
