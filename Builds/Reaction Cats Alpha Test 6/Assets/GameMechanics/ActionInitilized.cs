using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionInitilized : MonoBehaviour {


    public GameObject actionsList;
    public GameObject selectedActions;
    public GameObject actionPrefab;
    public List<GameObject> actionPrefabs = new List<GameObject>();
    public List<GameObject> selectedActionPrefabs = new List<GameObject>();
    public List<Action> alwaysAvailibleActions = new List<Action>();
    public static Player currentPlayer;
    public static bool waitingForSelection = true;
    private bool generatedActions = false;
    private float positionHeightIncrease = 165f;
    private float selectedPositionHeightIncrease = 165f;
    private List<Action> combinedAvailibleActions = new List<Action>();

    public int phasesActionsTakingUp = 0;
    public int curSelectionPhase = 1;    
    List<Action> actionsSelected = new List<Action>();
    

    #region Singleton

    public static ActionInitilized instance;

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More Than One Instance Of A Single Hex found!");
            return;
        }

        instance = this;
    }

    #endregion
    public void FixedUpdate()
    {
        if (waitingForSelection == true)
        {
            generatedActions = false;
            actionsList.SetActive(false);
            selectedActions.SetActive(false);
            if (actionPrefabs.Count >= 1)
            {
                foreach (GameObject gObj in actionPrefabs)
                {
                    Destroy(gObj);
                    actionPrefabs.Remove(gObj);
                }
                actionPrefabs.Clear();
            }
            if (selectedActionPrefabs.Count >= 1)
            {
                foreach (GameObject gObj in selectedActionPrefabs)
                {
                    Destroy(gObj);
                    selectedActionPrefabs.Remove(gObj);
                }
                selectedActionPrefabs.Clear();
            }
        }

        if (waitingForSelection == false)
        {
            actionsList.SetActive(true);
            selectedActions.SetActive(true);

            if (generatedActions == false)
            {
                combinedAvailibleActions = alwaysAvailibleActions.Union<Action>(currentPlayer.curcat.possibleActions).ToList();
                foreach (Action action in combinedAvailibleActions)
                {
                    GameObject newAction = Instantiate(actionPrefab, new Vector3(actionsList.transform.position.x, actionsList.transform.position.y + positionHeightIncrease), Quaternion.identity) as GameObject;
                    newAction.transform.SetParent(actionsList.transform);
                    newAction.SetActive(true);
                    newAction.transform.GetComponentInChildren<Text>().text = action.name;
                    newAction.GetComponent<SelectableAction>().actionAttached = action;

                    actionPrefabs.Add(newAction);
                    positionHeightIncrease -= 35f;
                }
                generatedActions = true;
            }
        }

        if (currentPlayer != null && phasesActionsTakingUp == TurnSystem.maxPhases)
        {
            ActionInfo aInfo = new ActionInfo {
                actions = actionsSelected,
                actionPhaseAmount = TurnSystem.maxPhases,
                cat = currentPlayer,
                hexesToMoveThrough = currentPlayer.hexes
            };

            currentPlayer.FinishTurn(aInfo);
        }
    }

    public void SelectAction(Action action)
    {

        if (phasesActionsTakingUp < TurnSystem.maxPhases)
        {
            if (action.effects.Count() >= 1)
            {
                foreach (EffectTypes eff in action.effects)
                {
                    if (eff == EffectTypes.Move)
                    {
                        StartCoroutine(currentPlayer.SelectMovement(2, curSelectionPhase));
                    }
                }
            }
            GameObject selectedAction = Instantiate(actionPrefab, new Vector3(selectedActions.transform.position.x, selectedActions.transform.position.y + selectedPositionHeightIncrease), Quaternion.identity) as GameObject;
            selectedAction.transform.SetParent(selectedActions.transform);
            selectedAction.SetActive(true);
            selectedAction.transform.GetComponentInChildren<Text>().text = action.name;
            selectedAction.GetComponent<SelectableAction>().actionAttached = action;

            selectedActionPrefabs.Add(selectedAction);
            Action newAction = Instantiate(action);
            newAction.name = action.name;
            newAction.phaseToRunAt = curSelectionPhase;
            actionsSelected.Add(newAction);
            phasesActionsTakingUp += action.phaseCount;
            curSelectionPhase += 1;
            selectedPositionHeightIncrease -= 35f;
        }
    }
}
