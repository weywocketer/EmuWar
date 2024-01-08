using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FubarOps.Combat;
using FubarOps.Constants;
using FubarOps.BoidNS;
using FubarOps.FSM;

namespace FubarOps.SquadManagement
{
    public class SquadLeader : MonoBehaviour
    {
        public List<GameObject> squadMembers;
        List<int> selectedSoldierIndices = new List<int>();
        [SerializeField] GameObject squadDirectionPrefab;
        Transform squadDirection;
        SquadDirectionChanger squadDirectionChanger;
        List<Vector2> positionsInFormation;
        float formationSpacing = 5;

        void Start()
        {
            squadDirection = Instantiate(squadDirectionPrefab, transform).transform;
            squadDirectionChanger = squadDirection.GetComponent<SquadDirectionChanger>();
            SetSquadDirection(transform.position + transform.right); // Set initial squad direction to squad leader's orientation.

            positionsInFormation = new List<Vector2>(Formations.line);

            for (int i = 0; i < positionsInFormation.Count; i++)
            {
                positionsInFormation[i] *= formationSpacing;
            }

            InitializeSquadMembers();
        }

        void InitializeSquadMembers()
        {
            for (int i = 1; i < squadMembers.Count; i++)
            {
                squadMembers[i].GetComponent<StateMachine>().ChangeState(new SoldierFollowLeaderState(squadMembers[i].GetComponent<Boid>(), squadDirection, positionsInFormation[i]));
            }
        }

        void SetSquadDirection(Vector3 squadLookAt)
        {
            squadDirectionChanger.eulerRotation = new Vector3(0, 0, Vector3.SignedAngle(Vector3.right, squadLookAt - transform.position, Vector3.forward));
        }

        void ChangeFormation(List<Vector2> formationTemplate)
        {
            positionsInFormation = new List<Vector2>(formationTemplate);

            for (int i = 0; i < positionsInFormation.Count; i++)
            {
                positionsInFormation[i] *= formationSpacing;
            }

            // Update formation only for soldiers that are following leader.
            // Formation update is done by reentering the SoldierFollowLeaderState.
            for (int i = 1; i < squadMembers.Count; i++)
            {
                if (squadMembers[i].GetComponent<StateMachine>().currentState is SoldierFollowLeaderState)
                {
                    squadMembers[i].GetComponent<StateMachine>().ChangeState(new SoldierFollowLeaderState(squadMembers[i].GetComponent<Boid>(), squadDirection, positionsInFormation[i]));
                }
            }
        }

        void ChangeSpacing(float newSpacing)
        {

            // Divide positions by old spacing to get the "normalized" current formation.
            for (int i = 0; i < positionsInFormation.Count; i++)
            {
                positionsInFormation[i] /= formationSpacing;
            }

            formationSpacing = newSpacing;

            ChangeFormation(positionsInFormation);
        }

        void ChangeAttackRangeSquared(List<int> soldierIndices, float attackRangeSquared)
        {
            foreach (int i in soldierIndices)
            {
                squadMembers[i].GetComponent<DistanceAttack>().attackRangeSquared = attackRangeSquared;
            }
        }

        void BackToFormation(List<int> soldierIndices)
        {
            foreach (int i in soldierIndices)
            {
                squadMembers[i].GetComponent<StateMachine>().ChangeState(new SoldierFollowLeaderState(squadMembers[i].GetComponent<Boid>(), squadDirection, positionsInFormation[i]));
            }
        }

        void MoveTo(List<int> soldierIndices, Vector2 destination)
        {
            foreach (int i in soldierIndices)
            {
                squadMembers[i].GetComponent<StateMachine>().ChangeState(new SoldierMoveToState(squadMembers[i].GetComponent<Boid>(), destination));
            }
        }

        // Temp manual SL brain
        void Update()
        {
            if (Keyboard.current.numpad1Key.wasPressedThisFrame)
            {
                if (selectedSoldierIndices.Contains(1))
                {
                    selectedSoldierIndices.Remove(1);
                }
                else
                {
                    selectedSoldierIndices.Add(1);
                }
            }

            if (Keyboard.current.numpad2Key.wasPressedThisFrame)
            {
                if (selectedSoldierIndices.Contains(2))
                {
                    selectedSoldierIndices.Remove(2);
                }
                else
                {
                    selectedSoldierIndices.Add(2);
                }
            }

            if (Keyboard.current.numpad3Key.wasPressedThisFrame)
            {
                if (selectedSoldierIndices.Contains(3))
                {
                    selectedSoldierIndices.Remove(3);
                }
                else
                {
                    selectedSoldierIndices.Add(3);
                }
            }


            if (Mouse.current.middleButton.isPressed)
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);

                SetSquadDirection(mouseWorldPosition);
            }

            if (Mouse.current.rightButton.isPressed)
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);

                MoveTo(selectedSoldierIndices, mouseWorldPosition);
            }

            if (Keyboard.current.numpadEnterKey.wasPressedThisFrame)
            {
                BackToFormation(selectedSoldierIndices);
            }

            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                ChangeFormation(Formations.wedge);
            }

            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                ChangeFormation(Formations.line);
            }

            if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                ChangeFormation(Formations.column);
            }

            if (Keyboard.current.equalsKey.wasPressedThisFrame)
            {
                ChangeSpacing(formationSpacing + 1);
            }

            if (Keyboard.current.minusKey.wasPressedThisFrame)
            {
                ChangeSpacing(formationSpacing - 1);
            }
        }
    }
}
