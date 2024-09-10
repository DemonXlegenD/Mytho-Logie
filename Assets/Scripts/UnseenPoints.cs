using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnseenPoints : MonoBehaviour
{
    // Liste de GameObjects à remplir via l'inspecteur
    public List<GameObject> gameObjects = new List<GameObject>();

    // Structure pour contenir le nom et la distance
    public struct ClosestObjectInfo
    {
        public string name;
        public string StickerName;
        public float distance;

        public ClosestObjectInfo(string name, float distance)
        {
            this.name = name;
            this.StickerName = "None";
            this.distance = distance;
        }

        public void SetClosestObjectInfo(string Sname)
        {
            this.StickerName = Sname;
        }
    }

    // Méthode publique pour obtenir le nom du GameObject le plus proche (sans suppression immédiate)
    public ClosestObjectInfo? GetClosestGameObject(Transform targetTransform)
    {
        if (gameObjects.Count == 0)
        {
            Debug.Log("La liste des GameObjects de position est vide.");
            return null;
        }

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        // Parcourt la liste des objets pour trouver le plus proche
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                // Utilise les coordonnées mondiales (globales)
                Vector3 objWorldPosition = obj.transform.position;
                Vector3 targetWorldPosition = targetTransform.position;

                // Calcul de la distance entre les positions mondiales
                float distance = Vector3.Distance(objWorldPosition, targetWorldPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = obj;
                }
            }
        }

        if (closestObject != null)
        {
            // Retourne le nom et la distance de l'objet sans le supprimer tout de suite
            return new ClosestObjectInfo(closestObject.name, closestDistance);
        }

        return null;
    }

    // Méthode pour supprimer un GameObject de la liste
    public void RemoveGameObject(GameObject objToRemove)
    {
        gameObjects.Remove(objToRemove);
    }
}

