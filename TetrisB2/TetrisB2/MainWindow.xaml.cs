using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TetrisB2
{

    public partial class MainWindow : Window
    {

        // DispatcherTimer est une minuterie intégrée
        DispatcherTimer Timer;
        Grille maGrille;
        public MainWindow()
        {
            InitializeComponent();
        }
 
        // action à l'initialisation

        public void MainWindow_Initialized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(AffichageScore);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            GameStart();
        }

        public void GameStart()
        {
            MainGrid.Children.Clear();
            maGrille = new Grille(MainGrid);
            Timer.Start();
        }

        // gestion du score (nombre de ligne complète + score )

        public void AffichageScore(object sender, EventArgs e)
        {
            Score.Content = maGrille.getScore().ToString("0");
            Lines.Content = maGrille.getLignes().ToString("0");
            maGrille.CurrPieceMovDown();
        }

        private void GamePause()
        {
            if(Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }

        // Mode de difficulté plus élevé : les pièces tombes plus vite

        private void GameHard()
        {
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        // Les Contrôles clavier et leurs actions

        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if(Timer.IsEnabled) maGrille.CurrPieceMovLeft();
                    break;
                case Key.Right:
                    if(Timer.IsEnabled) maGrille.CurrPieceMovRight();
                    break;
                case Key.Down:
                    if(Timer.IsEnabled) maGrille.CurrPieceMovDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) maGrille.CurrPieceMovRotation();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    GamePause();
                    break;
                case Key.F4:
                    GameHard();
                    break;
                default:
                    break;
            }

        }
    }
}