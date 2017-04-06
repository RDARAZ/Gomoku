using System;


namespace ConsoleApplication65
{
    //struktura opisująca gracza
    public struct Player
    {
        //nazwa gracza
        public string Name;
        //ilość zwycięstw 
        public int Winnings;
        //reprezentujący go symbol
        public FieldType Type;

    }
    public enum FieldType
    {
        ftCircle = 1, ftCross = 10
    };

    class GomokuEngine
    {
        //tablica gry 3x3
        private FieldType[,] FField = new FieldType[3, 3];
        //zmienna oznaczająca zwyciestwo któregoś z gracza
        private bool FWinner;
        //ID gracza który teraz wykonuje ruch
        private int FActive;
        //tablica statystyk graczy (tylko dwóch)
        private Player[] FPlayer = new Player[2];

        /*METODY PRYWATNE*/

        //do otrzymywania ilości wygranych gier
        private int GetWinnings()
        {
            return FPlayer[0].Winnings;
        }
        //do ustalenia liczby zwyciestw
        private void SetWPlayer(int Winnings)
        {
            FPlayer[0].Winnings = Winnings;
        }
        //do otrzymywania ilości wygranych gier
        private int GetWinnings2()
        {
            return FPlayer[1].Winnings;
        }
        //do ustalenia liczby zwyciestw
        private void SetWPlayer2(int Winnings)
        {
            FPlayer[1].Winnings = Winnings;
        }

        //do otrzymywania nazwy gracza
        private string GetPlayer1()
        {
            return FPlayer[0].Name;
        }
        //do ustalania nazwy gracza
        private void SetPlayer1(string Name)
        {
            FPlayer[0].Name = Name;
        }
        //do otrzymywania nazwy gracza
        private string GetPlayer2()
        {
            return FPlayer[1].Name;
        }
        //do ustalania nazwy gracza
        private void SetPlayer2(string Name)
        {
            FPlayer[1].Name = Name;
        }
        private Player GetActive()
        {
            return FPlayer[FActive];
        }
        //właściwości Winner
        public bool Winner
        {
            get
            {
                return FWinner;
            }
        }

        //właściwość zwraca aktywnego gracza
        public Player Active
        {
            get
            {
                return GetActive();
            }
        }
        //zwraca informacje o wygranych gracza 1
        public int WPlayer
        {
            get
            {
                return GetWinnings();
            }
            set
            {
                SetWPlayer(value);
            }
        }
        //zwraca informacje o wygranych gracza 1
        public int WPlayer2
        {
            get
            {
                return GetWinnings2();
            }
            set
            {
                SetWPlayer2(value);
            }
        }

        //zwraca informacje o graczu 1
        public string Player1
        {
            get
            {
                return GetPlayer1();
            }
            set
            {
                SetPlayer1(value);
            }
        }
        //zwraca informacje o graczu 2
        public string Player2
        {
            get
            {
                return GetPlayer2();
            }
            set
            {
                SetPlayer2(value);
            }
        }
        //wartość tylko do odczytu (zwraca stan pola bitwy)
        public FieldType[,] Field
        {
            get
            {
                return FField;
            }
        }
        //metoda sprawdza czy gracz nr 1 lub 2 wygrał grę
        private void Sum(int Value)
        {
            if (Value == 3 || Value == 30)
            {
                FPlayer[FActive].Winnings++;
                FWinner = true;

            }
        }
        //algorytm sprawdza, czy któryś z graczy wygrał
        private void CheckWinner()
        {
            Sum((int)FField[2, 0] + (int)FField[1, 1] + (int)FField[0, 2]);
            Sum((int)FField[0, 0] + (int)FField[1, 1] + (int)FField[2, 2]);
            Sum((int)FField[0, 0] + (int)FField[0, 1] + (int)FField[0, 2]);
            Sum((int)FField[1, 0] + (int)FField[1, 1] + (int)FField[1, 2]);
            Sum((int)FField[2, 0] + (int)FField[2, 1] + (int)FField[2, 2]);
            Sum((int)FField[0, 0] + (int)FField[1, 0] + (int)FField[2, 0]);
            Sum((int)FField[0, 1] + (int)FField[1, 1] + (int)FField[2, 1]);
            Sum((int)FField[0, 2] + (int)FField[1, 2] + (int)FField[2, 2]);
            /*
            for (int i = 0; i < 3; i++)
            {
                Sum((int)FField[i, 0] + (int)FField[i, 1] + (int)FField[i, 2]);
                Sum((int)FField[0, i] + (int)FField[1, 0] + (int)FField[2, i]);
            }
            Sum((int)FField[0, 0] + (int)FField[1, 1] + (int)FField[2, 2]);
            Sum((int)FField[0, 2] + (int)FField[1, 1] + (int)FField[2, 0]);
            */
        }
        /*ROZPOCZYNA WŁAŚCIWĄ GRĘ*/

        public void Start()
        {
            //przyporządkowanie symbolu danemu graczowi
            FPlayer[0].Type = FieldType.ftCircle;
            FPlayer[1].Type = FieldType.ftCross;

            FWinner = false;
            //czyszczenie tablicy
            System.Array.Clear(FField, 0, FField.Length);
        }

        //nowa gra - ilość zwycięstw zostaje wyzerowana
        public void NewGame()
        {
            FPlayer[0].Winnings = 0;
            FPlayer[1].Winnings = 0;
        }
        //Metoda służy do ustawiania symbolu na danym polu
        public bool Set(int X, int Y)
        {
            //ponieważ indeks tablic rozpoczyna się od zera, należy zmniejszyć
            //wartość współrzędnych, bo user podaje współrzędne numerowane od 1
            --X;
            --Y;
            /*
            //sprawdzamy czy użytkownik podał prawidłowe współrzędne 
            if ((X > 2) || (Y > 2))
            {
                Console.WriteLine("Nieprawidłowe wartości X lub/i Y");
                return false;
            }

            //sprawdzamy czy użytkownik podał prawidłowe współrzędne
            if ((X < 0) || (Y < 0))
            {
                Console.WriteLine("Nieprawidłowe wartości X lub/i Y");
                return false;
            }
            */
            //sprawdzenie, czy pole nie jest zajęte
            if (FField[X, Y] > 0)
            {
                Console.WriteLine("To pole jest zajęte!");
                return false;
            }
            try
            {
                //ustawienie znaku na danym polu
                //ustawienie znaku na danym polu
                FField[X, Y] = GetActive().Type;
            }
            catch
            {
                Console.WriteLine("Nieprawidłowe wartości X lub/i Y");
            }



            //sprawdzenie, czy należy zakończyć grę
            CheckWinner();
            //jeżeli nikt nie wygrał - zmiana graczy
            if (!Winner)
            {
                FActive = (FActive == 0 ? 1 : 0);
            }
            return true;
        }
    }
}
