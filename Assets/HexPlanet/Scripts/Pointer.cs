using UnityEngine;
using System.Collections;

public enum PointerStatus {TILE, SELECTED, BUSY, BUILD_OK, BUILD_INVALID, TARGETED};
public enum PointerMode {NORMAL, TARGETING, BUILDING};

public class Pointer : MonoBehaviour {
	public static Pointer instance;

	public Transform planetTransform;

	public PointerStatus status;
	public PointerMode mode;
	
	public Transform ptr;
	public Transform selector;
	public SpriteRenderer ptrRenderer;
	public SpriteRenderer selectorRenderer;
	
	public Sprite defaultPointer;
	public Sprite selectedPointer;
	public Sprite busyPointer;
	public Sprite buildValidPtr;
	public Sprite buildInvalidPtr;
	public Sprite targetedPtr;
	
	
	private Sprite[] pointers;
	private float spinSpeed = 25f;
	private bool showing;
	private bool selecting;
	private bool modeSet;
	
	private Transform selectedObj;
	
	void Awake(){
		instance = this;
	}
	
	// Use this for initialization
	void Start () {
		pointers = new Sprite[6];
		pointers [0] = defaultPointer;
		pointers [1] = selectedPointer;
		pointers [2] = busyPointer;
		pointers [3] = buildValidPtr;
		pointers [4] = buildInvalidPtr;
		pointers [5] = targetedPtr;
	}
	
	// Update is called once per frame
	void Update () {
		if (showing) {
			ptr.RotateAround(ptr.position, ptr.up, Time.deltaTime * spinSpeed);
		}
		if (selecting){
			selector.RotateAround(selector.position, selector.up, Time.deltaTime * spinSpeed);
			
		}
	}
	
	public void setPointer(PointerStatus s, Vector3 target){
		ptrRenderer.enabled = true;
		ptr.position = target;
		if (planetTransform != null) {
			ptr.up = target - planetTransform.position;
		}

		showing = true;
		if (!modeSet) {
			status = s;
			ptrRenderer.sprite = pointers [(int)status];		
		}
	}

    public void setPointer(PointerStatus s, Vector3 target, Vector3 norm)
    {
        ptrRenderer.enabled = true;
        ptr.position = target;
        ptr.up = norm; 

        showing = true;
        if (!modeSet)
        {
            status = s;
            ptrRenderer.sprite = pointers[(int)status];
        }
    }

    //Override version for guaranteed pointer rotation, which is aligned to the targetTransform's local up
    public void setPointer(PointerStatus s, Transform targetTransform){
		ptrRenderer.enabled = true;
		ptr.position = targetTransform.position;
		ptr.up = targetTransform.up;
		showing = true;
		if (!modeSet) {
			status = s;
			ptrRenderer.sprite = pointers [(int)status];		
		}
	}
	
	public void setPointer(PointerStatus s, Vector3 target, Vector3 relativeTo, float scaleFactor){
		ptrRenderer.enabled = true;
		ptr.position = target;
		Vector3 relDir = target - relativeTo;
		ptr.rotation = Quaternion.FromToRotation (Vector3.up, relDir);
		ptr.localScale = ptr.localScale * scaleFactor;
		showing = true;
		if (!modeSet) {
			status = s;
			ptrRenderer.sprite = pointers [(int)status];		
		}
	}
	
	public void unsetPointer(){
		showing = false;
		ptrRenderer.enabled = false;
	}
	
	public void setSelected(PointerStatus s, Vector3 target){
		//setPointer (s, target);
		selectorRenderer.sprite = pointers [(int)s];
		selectorRenderer.enabled = true;
		selector.position = target;
		selector.rotation = Quaternion.FromToRotation (Vector3.up, target);
		selecting = true;
		//selector.SetParent (transform);
		selectedObj = null;
	}
	
	public void setSelected(PointerStatus s, Vector3 target, Transform targetObj){
		//setPointer (s, target);
		selectorRenderer.sprite = pointers [(int)s];
		selectorRenderer.enabled = true;
		selector.position = target;
		selector.rotation = Quaternion.FromToRotation (Vector3.up, target);
		selecting = true;
		selectedObj = targetObj;
		selector.SetParent (selectedObj);
	}
	
	/// <summary>
	/// Overrides the sprite of the pointer until clearMode is called.
	/// </summary>
	/// <param name="s">Status of pointer to be set.</param>
	public void setMode(PointerStatus s){
		modeSet = true;
		status = s;
		ptrRenderer.sprite = pointers [(int)status];		
	}
	
	public void clearMode(){
		modeSet = false;
	}
	
	
	
	public void Deselect(){
		if (selecting) {
			selecting = false;
			selectorRenderer.enabled = false;
			selector.localScale = Vector3.one;
			selectedObj = null;
			selector.SetParent (transform);		
		}
		
	}
	
}
