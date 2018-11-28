using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ページスクロールビュー.
public class PageScrollRect : ScrollRect
{
    private GameObject panel;
    private GameObject prevArrow;
    private GameObject nextArrow;
    private GameObject pageNavi;
    // 1ページの幅.
    private float pageWidth;
    // 前回のページIndex. 最も左を0とする.
    private int prevPageIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        // 1ページの幅を取得.
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    private void Start(){
        panel = GameObject.Find("6_Panel_Stage (1)");
        prevArrow = panel.transform.GetChild(3).gameObject;
        nextArrow = panel.transform.GetChild(2).gameObject;
        pageNavi = GameObject.Find("PageNavi");
        UpdatePage(0);
    }

    // ドラッグを開始したとき.
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    // ドラッグを終了したとき.
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

            // ドラッグを終了したとき、スクロールをとめます.
            // スナップさせるページが決まった後も慣性が効いてしまうので.
            StopMovement();

            // スナップさせるページを決定する.
            // スナップさせるページのインデックスを決定する.
            int pageIndex = Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);
            // ページが変わっていない且つ、素早くドラッグした場合.
            // ドラッグ量の具合は適宜調整してください.
            if (pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 5)
            {
                pageIndex += (int)Mathf.Sign(eventData.delta.x);
            }

            // Contentをスクロール位置を決定する.
            // 必ずページにスナップさせるような位置になるところがポイント.

            /*
            float destX = pageIndex * pageWidth;
            content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);
            */

            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", content.anchoredPosition.x,
                "to", pageIndex * pageWidth,
                "delay", 0,
                "time", 0.3f,
                "easeType",iTween.EaseType.easeInSine,
                "onupdatetarget", this.gameObject,
                "onupdate", "OnUpdatePos",
                "oncomplete","OnCompletePos","oncompletetarget",gameObject)
            );

            // 「ページが変わっていない」の判定を行うため、前回スナップされていたページを記憶しておく.
            prevPageIndex = pageIndex;
    }

    public void MoveNextPage(){
        int pageIndex = prevPageIndex - 1;

        iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", content.anchoredPosition.x,
                "to", pageIndex * pageWidth,
                "delay", 0,
                "time", 0.3f,
                "easeType",iTween.EaseType.easeInOutSine,
                "onupdatetarget", this.gameObject,
                "onupdate", "OnUpdatePos",
                "oncomplete","OnCompletePos","oncompletetarget",gameObject)
            );

        // 「ページが変わっていない」の判定を行うため、前回スナップされていたページを記憶しておく.
        prevPageIndex = pageIndex;
    }

    public void MovePrevPage(){
        int pageIndex = prevPageIndex + 1;

        iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", content.anchoredPosition.x,
                "to", pageIndex * pageWidth,
                "delay", 0,
                "time", 0.3f,
                "easeType",iTween.EaseType.easeInOutSine,
                "onupdatetarget", this.gameObject,
                "onupdate", "OnUpdatePos",
                "oncomplete","OnCompletePos","oncompletetarget",gameObject)
            );

        // 「ページが変わっていない」の判定を行うため、前回スナップされていたページを記憶しておく.
        prevPageIndex = pageIndex;
    }

    void OnUpdatePos(float pos)
    {
        content.anchoredPosition = new Vector2(pos, content.anchoredPosition.y);
    }

    void OnCompletePos(){
        UpdatePage(prevPageIndex);
    }

    void UpdatePage(int n){
        Debug.Log("index:" + n);
        prevArrow.SetActive(true);
        nextArrow.SetActive(true);
        if(n==0)prevArrow.SetActive(false);
        if(n==-3)nextArrow.SetActive(false);

        for(int i=0;i<4;i++){
            pageNavi.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        pageNavi.transform.GetChild(n*(-1)).GetChild(0).gameObject.SetActive(true);
    }
}