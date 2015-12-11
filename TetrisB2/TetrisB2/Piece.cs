using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TetrisB2
{
   public class Piece
    {
       private Point currPosition;
       private Point[] currPiece;
       private Brush currColor; // Brush gère les couleurs pour les pièces
       private bool rotate; // booléen pour savoir si il est possible d'effectuer la rotation d'une pièce

       public Piece()
       {
           currPosition = new Point(0, 0);
           currColor = Brushes.Transparent;
           currPiece = setRandomPiece();
       }


       public Brush getCurrColor()
       {
           return currColor;
       }

       public Point getCurrPosition()
       {
           return currPosition;
       }

       public Point[] getCurrPiece()
       {
           return currPiece;
       }

       public void movLeft()
       {
           currPosition.X -= 1;
       }
       public void movRight()
       {
           currPosition.X += 1;
       }
       public void movDown()
       {
           currPosition.Y += 1;
       }
       public void movRotation()
       {
           if (rotate)
           {
               for (int i = 0; i < currPiece.Length; i++)
                {
                   double x = currPiece[i].X;
                   currPiece[i].X = currPiece[i].Y * -1;
                   currPiece[i].Y = x;
               }
           }
       }

       // les pièces sont créent en fonction de point

       private Point[] setRandomPiece()
        {
            Random random = new Random();
            switch (random.Next() % 7)
            {
                case 0: // I
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0),
                        new Point(2, 0)
                    };
                case 1: // J 
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[]{
                        new Point(1, -1),
                        new Point(-1, 0),
                        new Point(0, 0),
                        new Point(1, 0)
                    };
                case 2: // L
                    rotate = true;
                    currColor = Brushes.DarkOrange;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(1, 0),
                        new Point(1, 1)
                    };
                case 3: // O
                    rotate = false;
                    currColor = Brushes.Yellow;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(0, 1),
                        new Point(1, 0),
                        new Point(1, 1)
                    };
                case 4: // S
                    rotate = true;
                    currColor = Brushes.Lime;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(0, 1),
                        new Point(-1, 1)
                    };
                case 5: // T 
                    rotate = true;
                    currColor = Brushes.Magenta;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, -1),
                        new Point(1, 0)
                    };
                case 6: // Z 
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[]{
                        new Point(0, 0),
                        new Point(-1, 0),
                        new Point(0, 1),
                        new Point(1, 1)
                    };
                default:
                    return null;
            }
        }
    }
}
