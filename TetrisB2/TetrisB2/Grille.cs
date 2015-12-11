using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Drawing;

namespace TetrisB2
{
    class Grille
    {

        private int Lignes;
        private int Colonnes;
        private int Score;
        private int LignesCompletes;
        private Piece currPiece;
        private Label[,] BlockControls;
        private Brush NoBrush = Brushes.Black;
        private Brush SilverBrush = Brushes.Gray;

        public Grille(Grid TetrisGrid)
        {
            Lignes = TetrisGrid.RowDefinitions.Count;
            Colonnes = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LignesCompletes = 0;

            BlockControls = new Label[Colonnes, Lignes];
            for (int i = 0; i < Colonnes; i++)
            {
                for (int j = 0; j < Lignes; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetColumn(BlockControls[i, j], i);
                    Grid.SetRow(BlockControls[i, j], j);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            currPiece = new Piece();
            currNewPiece();

        }

        public int getScore(){
            return Score;
            }

        public int getLignes() {
            return LignesCompletes;
                    }

        private void currNewPiece() {
            Point Position = currPiece.getCurrPosition();
            Point[] Piece = currPiece.getCurrPiece();
            Brush Color = currPiece.getCurrColor();
            foreach (Point S in Piece)
            {
                BlockControls[(int)(S.X + Position.X) + ((Colonnes / 2) -1),
                    (int)(S.Y + Position.Y)+2].Background = Color;
            }
        }

        private void currDeletePiece() {
            Point Position = currPiece.getCurrPosition();
            Point[] Piece = currPiece.getCurrPiece();
            foreach (Point S in Piece)
            {
                BlockControls[(int)(S.X + Position.X) + ((Colonnes / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = NoBrush;
            }
        }

        private void LigneComplete() {
            bool complet;
            for (int i = Lignes - 1; i > 0; i--)
            {
                complet = true;
                for (int j = 0; j < Colonnes; j++)
                {
                    if (BlockControls[j, i].Background == NoBrush)
                    {
                        complet = false;
                    }
                }
                if (complet)
                {
                    DeleteRow(i);
                    Score += 100;
                    LignesCompletes += 1;
                }
            }

            
        }

        private void DeleteRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Colonnes; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j,i - 1].Background;
                }
            }
        }

        public void CurrPieceMovLeft()
        {
            Point Position = currPiece.getCurrPosition();
            Point[] Piece = currPiece.getCurrPiece();
            bool move = true;
            currDeletePiece();
            foreach (Point S in Piece)
            {
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currPiece.movLeft();
                currNewPiece();
            }
            else
            {
                currNewPiece();
            }
        }
        public void CurrPieceMovRight()
        {
            Point Position = currPiece.getCurrPosition();
            Point[] Piece = currPiece.getCurrPiece();
            currDeletePiece();
            bool move = true;
            foreach (Point S in Piece)
            {
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) + 1) >= Colonnes)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currPiece.movRight();
                currNewPiece();
            }
            else
            {
                currNewPiece();
            }
        }
        public void CurrPieceMovDown()
        {
            Point Position = currPiece.getCurrPosition();
            Point[] Piece = currPiece.getCurrPiece();
            bool move = true;
            currDeletePiece();
            foreach (Point S in Piece)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Lignes)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2+ 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currPiece.movDown();
                currNewPiece();
            }
            else
            {
                currNewPiece();
                LigneComplete();
                currPiece = new Piece();
            }
        }
        public void CurrPieceMovRotation()
        {
            Point Position = currPiece.getCurrPosition();
            Point[] S = new Point[4];
            Point[] Piece = currPiece.getCurrPiece();
            bool move = true;
            Piece.CopyTo(S, 0);
            currDeletePiece();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;

                if(((int)((S[i].Y + Position.Y) +  2)) >= Lignes)
                {
                    move = false;
                }
                else if(((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) >= Lignes)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Colonnes / 2) -1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }

            if (move)
            {
                currPiece.movRotation();
                currNewPiece();
            }
            else
            {
                currNewPiece();
            }
        }
    }
}
