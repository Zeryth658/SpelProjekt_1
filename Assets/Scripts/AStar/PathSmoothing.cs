using UnityEngine;
using System.Collections.Generic;
public static class PathSmoothing
{
   public static List<Vector2> SmoothPath(List<Vector2> path, LayerMask obstacleMask, Vector2 currentPos)
   {
      if (path == null || path.Count < 2) return path;
      List<Vector2> newPath = new List<Vector2>();
      int currentIndex = 0;
      
      for (int i = path.Count - 1; i >= 0; i--)
      {
         if (!Physics2D.Linecast(currentPos, path[i], obstacleMask))
         {
            currentIndex = i;
            break;
         }
      }
      newPath.Add(path[currentIndex]);

      while (currentIndex < path.Count - 1)
      {
         int nextIndex = currentIndex + 1;

         for (int i = path.Count - 1; i > currentIndex; i--)
         {
            if (!Physics2D.Linecast(path[currentIndex], path[i], obstacleMask))
            {
               nextIndex = i;
               break;
            }
         }
         currentIndex = nextIndex;
         newPath.Add(path[nextIndex]);
      }
      
      return newPath;
   }
}
