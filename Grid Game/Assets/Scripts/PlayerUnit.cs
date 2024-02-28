using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
   [SerializeField] private GridManager gridManager;

   [SerializeField] private float moveSpeed = 5;

   private void OnEnable()
   {
      gridManager.TileSelected += OnTileSelected;
   }

   private void OnDisable()
   {
      gridManager.TileSelected -= OnTileSelected;
   }

   private void OnTileSelected(GridTile gridTile)
   {
      StopAllCoroutines();
      StartCoroutine(Co_MoveTo(gridTile.transform.position));
   }

   private IEnumerator Co_MoveTo(Vector3 targetPosition)
   {
      while (Vector3.Distance(transform.position, targetPosition) > 0.01)
      {
         transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
         yield return null;
      }

      transform.position = targetPosition;
   }
}
