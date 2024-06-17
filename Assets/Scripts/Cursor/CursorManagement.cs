using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    
    public static CursorManagement Instance { get; private set; }
    [SerializeField] private List<CursorAnimation> cursorAnimationList;

    CursorAnimation cursorAnimation;

    int currentFrame;
    float frameTimer;
    private int frameCount;


    public enum CursorType
    {
        Arrow,
        Shoot,
        Click,
        Grab
    }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetActiveCursorType(CursorType.Arrow);
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;
        if(frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset , CursorMode.Auto);
        }
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }
    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach(CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if(cursorAnimation.cursorType == cursorType)
            {
                return cursorAnimation;
            }
        }
        //Couldn't find this CursorType
        return null;
    }
    void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }

    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}
