using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class IO_Mouse
    {
        //---++ Constants and Enumerations ++---
        /////////////////////////////



        //---++ Variables ++---
        /////////////////////////////



        //---++ Functions ++---
        /////////////////////////////
        public static Vector3 MouseWorldPosition(Vector3 origin, LayerMask mask)
        {
            //This returns a Vector3 position in the ground of where the mouse is pointing

            Vector3 newPosition = origin;

            RaycastHit hitpoint;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitpoint, mask))
            {
                newPosition = hitpoint.point;
            }

            return newPosition;
        }
    }// End of IO_Mouse class
}//End of Utilities class
    
