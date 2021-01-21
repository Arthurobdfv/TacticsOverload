using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveAction : IAction
{
    private readonly IMapGenerator _mapGen;

    public ActionType ActionType { get => ActionType.Move; }

    public string ActionName => "Move";

    public Unit Entity { get; set; }

    public MoveAction(Unit entity)
    {
        _mapGen = Injector.GetInstance<IMapGenerator>() ?? throw new ArgumentNullException(nameof(_mapGen));
        Entity = entity;
    }

    private List<MapTerrain> subscriptions;

    private ActionType m_actionType;
    public void Action(CharacterStats charStats, Action ActionCallback = null)
    {
        ShowActionUI();
        var ObservingTiles = _mapGen.HighlighTilesAround(Entity.transform.position, charStats.Move);
        ObservingTiles.ToList().ForEach(t => t.RaiseOnClick += TileSelected);
        subscriptions = ObservingTiles.ToList();
    }

    public void ShowActionUI()
    {

    }

    public void ShowConfirmation(Action onConfirm, Action onCancel)
    {
        throw new NotImplementedException();
    }

    public void TileSelected(object sender,OnClickEventArgs eventArgs)
    {
        subscriptions.ForEach(t => t.RaiseOnClick -= TileSelected);
        Debug.Log("Clicked");
    }
}
