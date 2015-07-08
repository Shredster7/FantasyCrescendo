﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace Genso.API {


    public static class GameObjectExtensions
    {

        /// <summary>
        /// Enumerates all children of a GameObject.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<GameObject> Children(this GameObject obj, Predicate<GameObject> filter = null) 
        {
            if (filter == null) 
            {
                foreach (Transform child in obj.transform) 
                {
                    if (child != null && child != obj.transform)
                        yield return child.gameObject;
                }
            } else {
                foreach (Transform child in obj.transform)
                {
                    if (child != null && child != obj.transform && filter(child.gameObject))
                        yield return child.gameObject;
                }
            }
        }

        /// <summary>
        /// Finds a child by /name/ and returns it.
        /// 
        /// If no child with name can be found, null is returned. If name contains a '/' character it will traverse the hierarchy like a path name.
        /// </summary>
        /// <param name="obj">the GameObject to search</param>
        /// <param name="name"><the name of the child to be found</param>
        /// <returns>Found child, null if not found.</returns>
        public static GameObject FindChild(this GameObject obj, string name) {
            if(name == null)
                throw new ArgumentNullException("name");
            Transform child = obj.transform.Find(name);
            return child == null ? null : child.gameObject;
        }

        /// <summary>
        /// Activates the GameObject if it already isn't.
        /// Does nothing if is null.
        /// </summary>
        /// <param name="obj"><the GameObject to activate/param>
        public static void Activate(this GameObject obj) {
            obj.SetActiveIfNot(true);
        }

        /// <summary>
        /// Activates all GameObjects in a collection if it they are not.
        /// Does nothing if the objects collection is null.
        /// </summary>
        /// <param name="objects">the collection of objects</param>
        public static void Activate(this IEnumerable<GameObject> objects) {
            objects.SetActiveIfNot(true);
        }

        /// <summary>
        /// Deactivates the GameObject if it already isn't.
        /// Does nothing if is null.
        /// </summary>
        /// <param name="obj"><the GameObject to activate/param>
        public static void Deactivate(this GameObject obj) {
            obj.SetActiveIfNot(false);
        }

        /// <summary>
        /// Deactivates all GameObjects in a collection if it they are not.
        /// Does nothing if the objects collection is null.
        /// </summary>
        /// <param name="objects">the collection of objects</param>
        public static void Deactivate(this IEnumerable<GameObject> objects) {
            objects.SetActiveIfNot(false);
        }

        private static void SetActiveIfNot(this GameObject obj, bool state) {
            if(obj != null && obj.activeSelf != state)
                obj.SetActive(state);
        }

        private static void SetActiveIfNot(this IEnumerable<GameObject> objects, bool state) {
            if (objects == null)
                return;
            var asArray = objects as GameObject[];
            if (asArray != null) {
                foreach (GameObject obj in asArray)
                    obj.SetActiveIfNot(state);
            } else {
                foreach (GameObject obj in objects) {
                    obj.SetActiveIfNot(state);
                }
            }
        }

        /// <summary>
        /// Retrieves a Component of a certain type. If one doesn't already exist, it will add one.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find/add.</typeparam>
        /// <param name="obj">the GameObject </param>
        /// <returns>the retrieved/added Component. Will never be null.</returns>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            T retrievedComponent = obj.GetComponent<T>();

            if (retrievedComponent == null)
                retrievedComponent = obj.gameObject.AddComponent<T>();

            return retrievedComponent;
        }

        /// <summary>
        /// Safely gets a Component attached to a GameObject of a certain type.
        /// Unlike the normal GameObject.GetComponent, this method works with interface types.
        /// This also will log an error message if no Component of that type is found.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject </param>
        /// <returns>the retrieved Component. Null if not found.</returns>
        public static T SafeGetComponent<T>(this GameObject obj) where T : class {
            T retrievedComponent = obj.GetComponent(typeof(T)) as T;

            if (retrievedComponent == null)
            {
                Debug.LogError("Expected to find obj of type "
                   + typeof(T) + " but found none", obj);
            }

            return retrievedComponent;
        }

        /// <summary>
        /// Gets a Component attached to a GameObject of a certain type.
        /// Unlike the normal GameObject.GetComponent, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject </param>
        /// <returns>the retrieved Component. Null if not found.</returns>
        public static T GetIComponent<T>(this GameObject obj) where T : class
        {
            return obj.GetComponent(typeof(T)) as T;
        }

        /// <summary>
        /// Gets all Components attached to a GameObject of a certain type.
        /// Unlike the normal GameObject.GetComponents, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject</param>
        /// <returns>the retrieved Components. Empty if not found.</returns>
        public static T[] GetIComponents<T>(this GameObject obj) where T : class
        {
            return obj.GetComponents(typeof(T)) as T[];
        }

        /// <summary>
        /// Gets a Component attached to a GameObject or its children of a certain type.
        /// Unlike the normal GameObject.GetComponent, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject </param>
        /// <returns>the retrieved Component. Null if not found.</returns>
        public static T GetIComponentInChildren<T>(this GameObject obj) where T : class
        {
            return obj.GetComponentInChildren(typeof(T)) as T;
        }

        /// <summary>
        /// Gets all Components attached to a GameObject or its children of a certain type.
        /// Unlike the normal GameObject.GetComponents, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject</param>
        /// <returns>the retrieved Components. Empty if not found.</returns>
        public static T[] GetIComponentsInChildren<T>(this GameObject obj) where T : class
        {
            return obj.GetComponentsInChildren(typeof(T)) as T[];
        }

        /// <summary>
        /// Gets a Component attached to a GameObject or its ancestors of a certain type.
        /// Unlike the normal GameObject.GetComponent, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject </param>
        /// <returns>the retrieved Component. Null if not found.</returns>
        public static T GetIComponentInParent<T>(this GameObject obj) where T : class
        {
            return obj.GetComponentInParent(typeof(T)) as T;
        }

        /// <summary>
        /// Gets all Components attached to a GameObject or its ancestors of a certain type.
        /// Unlike the normal GameObject.GetComponents, this method works with interface types.
        /// </summary>
        /// <typeparam name="T">the target type of Component to find.</typeparam>
        /// <param name="obj">the GameObject</param>
        /// <returns>the retrieved Components. Empty if not found.</returns>
        public static T[] GetIComponentsInParent<T>(this GameObject obj) where T : class
        {
            return obj.GetComponentsInParent(typeof(T)) as T[];
        }
    }

}

