using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public float accuracy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (pawn != null)
        {
            PossessPawn(pawn);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // if the game is paused don't do anything
        if (GameManager.instance.isPaused) return;
        MakeDecisions();
    }

    protected abstract void MakeDecisions();

    public virtual void PossessPawn(Pawn pawnToPossess)
    {
        // make that pawn ours
        pawn = pawnToPossess;
        // make that pawn's controller us
        pawn.controller = this;
        // put it on our layer
        pawn.gameObject.layer = this.gameObject.layer;
    }
    public virtual void UnpossessPawn()
    {
        // make that pawn's controller not us
        pawn.controller = null;
        // make that pawn no longer ours
        pawn = null;

        // divorce
    }
}
