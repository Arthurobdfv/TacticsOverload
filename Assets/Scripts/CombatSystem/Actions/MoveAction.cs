using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveAction : BaseAction
{
    private readonly IMapGenerator _mapGen;

    private Action callback;

    public MoveAction(Unit entity, Action<object, EventArgs> onComplete) : base(entity, ActionType.Move, "Move", onComplete)
    {
        _mapGen = Injector.GetInstance<IMapGenerator>() ?? throw new ArgumentNullException(nameof(_mapGen));
        Entity = entity;
        ActionCompleted += onComplete.Invoke;
    }

    private List<MapTerrain> subscriptions;

    private ActionType m_actionType;

    public override void Action(CharacterStats charStats, Action ActionCallback = null)
    {
        if (ActionCallback != null) callback = ActionCallback;
        ShowActionUI();
        var ObservingTiles = _mapGen.HighlighTilesAround(Entity.transform.position, charStats.Move);
        ObservingTiles.ToList().ForEach(t => t.RaiseOnClick += TileSelected);
        subscriptions = ObservingTiles.ToList();
    }

    public void ShowActionUI()
    {

    }

    public ActionResult ShowConfirmation(Action onConfirm = null, Action onCancel = null)
    {
        throw new NotImplementedException();
    }

    public void TileSelected(object sender, OnClickEventArgs eventArgs)
    {
        subscriptions.ForEach(t => t.RaiseOnClick -= TileSelected);
        subscriptions = new List<MapTerrain>();
        var startTile = _mapGen.GetTileOf(Entity.gameObject);
        var endTile = eventArgs.SenderGameObject.GetComponent<MapTerrain>();
        var path = _mapGen.FindPath(startTile, endTile);
        Entity.Move(path, callback);
        startTile.ClearObject();
        endTile.SetObject(Entity.gameObject);
        _mapGen.DeHighlight();\
        base.ActionCompleted()
    }
}
