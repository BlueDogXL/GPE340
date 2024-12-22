using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : Controller
{
    public bool isMouseRotation;
    public int lives;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void MakeDecisions()
    {
        // if we don't have a pawn
        if (pawn == null)
        {
            // there's nothing to be done
            return;
        }
        else
        {
            // get our direction from our inputs
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // clamp it to make it not ridiculous
            moveVector = Vector3.ClampMagnitude(moveVector, 1);
            // send it to our pawn to move that way
            pawn.Move(moveVector);
            // if we're using mouse rotation
            if (isMouseRotation)
            {
                // send a ray from our mouse pointer
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                // put a plane under our pawn
                Plane footPlane = new Plane(Vector3.up, pawn.transform.position);
                // variable for later
                float distanceToIntersect;
                // if we hit, put the distance into the float
                if (footPlane.Raycast(mouseRay, out distanceToIntersect))
                {
                    // get the point we intersect at
                    Vector3 intersectPoint = mouseRay.GetPoint(distanceToIntersect);
                    // tell our pawn to look there
                    pawn.RotateToLookAt(intersectPoint);
                }
                else
                {
                    // ideally this should never happen but like what if
                    Debug.Log("No intersection between plane and ray!");
                }
            }
            else
            {
                // otherwise just rotate based on the camera rotation axis
                pawn.Rotate(Input.GetAxis("CameraRotation"));
            }
            // if fire button is pressed
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire button pressed!");
                // if we have a weapon
                if (pawn.weapon != null)
                {
                    // begin the attack
                    pawn.weapon.OnPrimaryAttackBegin.Invoke();
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                // if we have a weapon
                if (pawn.weapon != null)
                {
                    // halt the attack
                    pawn.weapon.OnPrimaryAttackEnd.Invoke();
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Alt fire button pressed!");
                // if we have a weapon
                if (pawn.weapon != null)
                {
                    // begin the attack
                    pawn.weapon.OnSecondaryAttackBegin.Invoke();
                }
            }
            if (Input.GetButtonUp("Fire2"))
            {
                // if we have a weapon
                if (pawn.weapon != null)
                {
                    // halt the attack
                    pawn.weapon.OnSecondaryAttackEnd.Invoke();
                }
            }
        }
    }
}
