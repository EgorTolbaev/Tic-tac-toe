using UnityEngine;


public class MainGame : MonoBehaviour
{
    int WhoWin = 0;

    //кем будем играть
    public int
                Player = 0,  //игрок
                Comp = 0;    //компьютер

    //может ли ходить игрок
    public bool CanStep = true;

    //поле выбора кнопок нарисованно
    bool DrawSelect = true;
    //картинка из которой будет строиться блок
    public GameObject Img;

    //массив игровых элементов
    GameObject[] SelectPlayer;
   
    //режим игры
    public int GameMode = 0;

    //редактирования lable
    void FontEditing()
    {
        //Устанавливаем стиль lable
        GUIStyle style = GUI.skin.GetStyle("label");
        //Устанавливаем размер шрифта
        style.fontSize = (int)(40.0f);
        //Устанавливаем стиль Button
        style = GUI.skin.GetStyle("Button");
        //Устанавливаем размер шрифта
        style.fontSize = (int)(20.0f);
    }
    //игровое меню
    void MainMenu()
    {
        //получаем размеры экрана
        int H = Screen.height / 3, //высота экрана
            W = Screen.width / 2;  //ширена экрана
        //размер кнопок и надписей по ширене
        int Widht_BL = 100, Height_BL = 40;

        //расчитываем центр для надписей и кнопок
        //из центра окна вычитаем пол кнопки
        W = (W - Widht_BL / 2);

        Rect Location = new Rect(
                                    W -50,          //задаем позицию по Х
                                    H,          //задаем позицию по Y
                                    Widht_BL  + 1000,   //задаем размер по X
                                    Height_BL + 60  //задаем размер по Y
                                    );
        GUI.Label(
                 Location,    //опеделеяем размеры и расположение надписи
                 "Меню игры"  //опеределяем название надписи
                 );

        //смещаем вниз на 10 и высоту надписи
        H += (Height_BL + 10);
        //так как кнопка шире на 15 точек поэтому вычьтем 
        //эту разницу из точки где должно быть начало кнопки
        W -= 15;

        Location = new Rect(
                                    W,          //задаем позицию по Х
                                    H,          //задаем позицию по Y
                                    Widht_BL + 50,   //задаем размер по X
                                    Height_BL   //задаем размер по Y
                                    );
        //создание кнопки
        if (
            GUI.Button(
                    Location,       // опеделеяем размеры и расположение кнопки
                    "Старт игры"    //опеределяем название надписи кнопки
                    )
           ) GameMode = 1;

        //смещаем вниз на 10 и высоту надписи
        H += (Height_BL + 10);

        Location = new Rect(
                                    W,          //задаем позицию по Х
                                    H,          //задаем позицию по Y
                                    Widht_BL + 50,   //задаем размер по X
                                    Height_BL   //задаем размер по Y
                                    );
        //создание кнопки
        if (GUI.Button(
                    Location,       // опеделеяем размеры и расположение кнопки
                    "Выход"         //опеределяем название надписи кнопки
                    )) Application.Quit();

    }

    //функция отображения 2 картинок отображающих Х и О
    void DrawSelectStep()
    {
        //2 игровых блока Х и О
        SelectPlayer = new GameObject[2];

        //определяем позицию где будет создан блок
        Vector3 BlockLocation = new Vector3(-3, 0, 0);
        //создаем блок крестика,выбераем позицию, без поворота
        SelectPlayer[0] = (GameObject)Instantiate(Img, BlockLocation, Quaternion.identity);
        //получаем у блока компонент наш скрипт
        ImgChame Imgchame = SelectPlayer[0].GetComponent<ImgChame>();
        //задаем крестик
        Imgchame.Status = 1;

        //определяем позицию где будет создан блок
        BlockLocation = new Vector3(3, 0, 0);
        //создаем блок нолик,выбераем позицию, без поворота
        SelectPlayer[1] = (GameObject)Instantiate(Img, BlockLocation, Quaternion.identity);
        //получаем у блока компонент наш скрипт
        Imgchame = SelectPlayer[1].GetComponent<ImgChame>();
        //задаем нолик
        Imgchame.Status = 2;

        DrawSelect = false;
    }

    //удаление игровых объектов с поля
    void DestroyGameObgect()
    {
        //выставляем флаг рисования в правду 
        DrawSelect = true;
        //чистим массив игрока
        if (SelectPlayer != null)
            //перебираем массив созданных блоков 
            foreach (GameObject ForDel in SelectPlayer)
            {
                //уничтожаем объекты
                Destroy(ForDel);
            }
    }
    //функция отображения поля для игры
    void DrawPoleGame()
    {
        //определяем позицию где будет создан блок
        Vector3 BlockLocation = new Vector3(-3, 0, 0);
        //обьявялем поле из 9 элементов
        SelectPlayer = new GameObject[9];
        //номер создаваемого блока
        int Index = 0;
        //начальные координаты для блоков
        float X = -3, Y = 3;

        //цикл для пререхода по оси Y
        for (int j = 0; j < 3; j++)
        {
            //цикл для перехода по оси Х
            for (int i = 0; i < 3; i++)
            {
                //определяем позицию где будет создан блок
                BlockLocation = new Vector3(X, Y, 0);
                //создаем игровой блок 
                SelectPlayer[Index] = (GameObject)Instantiate(Img, BlockLocation, Quaternion.identity);
                //расчитываем следующюю координату по Х
                X += 2.8f;
                //расчитываем следующий номер блока
                Index++;
            }
            //расчитываем следующюю координату по Х
            X = -3;
            //расчитываем следующюю координату по Y
            Y -= 3;
        }

        DrawSelect = false;
    }
    //описываем внешний вид
    void OnGUI()
    {
        FontEditing();

        //режим игры Стартовое меню
        if (GameMode == 0) MainMenu();
        if (GameMode == 1)
        {
            //получаем размеры экрана
            int H = Screen.height / 3, //высота экрана
                W = Screen.width / 2;  //ширена экрана
            //размер кнопок и надписей по ширене
            int Widht_BL = 100, Height_BL = 40;

            //расчитываем центр для надписей и кнопок
            //из центра окна вычитаем пол кнопки
            W = (W - Widht_BL / 2);

            Rect Location = new Rect(
                                        W - 150,          //задаем позицию по Х
                                        H - 60,          //задаем позицию по Y
                                        Widht_BL + 1000,   //задаем размер по X
                                        Height_BL + 60  //задаем размер по Y
                                        );
            GUI.Label(
                     Location,    //опеделеяем размеры и расположение надписи
                     "Выберите за кого играть"  //опеределяем название надписи
                     );
            if (DrawSelect) DrawSelectStep();

            if (Player != 0) //если выбор сделан двигаемся дальше
            {
                //уничтожаем игровой объект
                DestroyGameObgect();
                //меняем режим игры на следующий
                GameMode++;
            }
        }
        //игровой режим
        if (GameMode == 2)
        {   //ресеум поле игрока
            if (DrawSelect) DrawPoleGame();
            //ход ПК
            if (CanStep == false)
            {
                //пробуем сделать ход
                int Index = Random.Range(0, 9);

                //получаем компоненте ImgChange
                ImgChame statusPoly = SelectPlayer[Index].GetComponent<ImgChame>();
                if (statusPoly.Status == 0)
                {
                    //записываем ход 
                    statusPoly.Status = Comp;
                    //разрешаем ходить игроку
                    CanStep = true;
                }
            }
            if (BeginsPaly()) GameMode = 3;
            if (TestWin(Player))
            {
                WhoWin = Player;
                GameMode = 3;
            }
            if (TestWin(Comp))
            {
                WhoWin = Comp;
                GameMode = 3;
            }
        }
        //окно с результатом игры
        if (GameMode == 3)
        {
            if(SelectPlayer != null) DestroyGameObgect();

            string Finish = "Ничья";

            if (WhoWin != 0)
            {
                if (WhoWin == Player) Finish = "Вы выиграли";
                if (WhoWin == Comp) Finish = "Вы проиграли";
            }

            //получаем размеры экрана
            int H = Screen.height / 3, //высота экрана
                W = Screen.width / 2;  //ширена экрана
                                       //размер кнопок и надписей по ширене
            int Widht_BL = 100, Height_BL = 140;

            //расчитываем центр для надписей и кнопок
            //из центра окна вычитаем пол кнопки
            W = (W - Widht_BL / 2);

            Rect Location = new Rect(
                                        W - 50,          //задаем позицию по Х
                                        H,          //задаем позицию по Y
                                        Widht_BL + 1000,   //задаем размер по X
                                        Height_BL + 60  //задаем размер по Y
                                        );
            GUI.Label(
                     Location,    //опеделеяем размеры и расположение надписи
                     Finish  //опеределяем название надписи
                     );
            //смещаем вниз на 10 и высоту надписи
            H += (Height_BL + 10);
            //так как кнопка шире на 15 точек поэтому вычьтем 
            //эту разницу из точки где должно быть начало кнопки
            W -= 15;

            Location = new Rect(
                                        W -20,          //задаем позицию по Х
                                        H,          //задаем позицию по Y
                                        Widht_BL +100,   //задаем размер по X
                                        Height_BL - 50   //задаем размер по Y
                                        );
            //создание кнопки
            if (
                GUI.Button(
                        Location,       // опеделеяем размеры и расположение кнопки
                        "Вернуться в меню"    //опеределяем название надписи кнопки
                        )
               )
            {
                //загрузить игру заново
                Application.LoadLevel("XO");
            }
        }
    }

    bool BeginsPaly()
    {
        //перебираем каждый блок
        foreach (GameObject block in SelectPlayer)
        {
            ImgChame status = block.GetComponent<ImgChame>();
            if (status.Status == 0) return false;

        }
        return true;
    }
    //функция проверки на победу передаем значение кого будем проверять
    bool TestWin(int WHO)
    {
        //список вариантов выигрышных комбинаций
        int[,] WinVariant =
            {      {    //1 вариант
                    1,1,1,  //Х Х Х
                    0,0,0,  //_ _ _
                    0,0,0   //_ _ _
                }, {    //2 вариант
                    0,0,0,  //_ _ _
                    1,1,1,  //Х Х Х
                    0,0,0   //_ _ _
                }, {    //3 вариант
                    0,0,0,  //_ _ _
                    0,0,0,  //_ _ _
                    1,1,1   //Х Х Х
                }, {    //4 вариант
                    1,0,0,  //Х _ _
                    1,0,0,  //Х _ _
                    1,0,0   //Х _ _
                }, {    //5 вариант
                    0,1,0,  //_ Х _
                    0,1,0,  //_ Х _
                    0,1,0   //_ Х _
                }, {    //6 вариант
                    0,0,1,  //_ _ Х
                    0,0,1,  //_ _ Х
                    0,0,1   //_ _ Х
                }, {    //7 вариант
                    1,0,0,  //Х _ _
                    0,1,0,  //_ Х _
                    0,0,1   //_ _ Х
                }, {    //8 вариант
                    0,0,1,   //_ _ Х
                    0,1,0,   //_ Х _
                    1,0,0    //Х _ _
                }
            };
        //получаем поле
        int[] TestMap = new int[9];

        if (SelectPlayer != null)
        {
            //просчитываем поле
            for (int I = 0; I < 9; I++)
            {
                ImgChame status = SelectPlayer[I].GetComponent<ImgChame>();
                if (status.Status == WHO) TestMap[I] = 1;
            }
        }

        //выбираем вариант для сравнения 
        for (int VariantIndex = 0; VariantIndex < WinVariant.GetLength(0); VariantIndex++)
        {
            //счетчик для подсчета соотвествий
            int WinState = 0;
            for(int TestIndex = 0; TestIndex <TestMap.Length; TestIndex++)
            {
                //если параметр равен 1 то проверяем его иначе 0 тоже = 0
                if (WinVariant[VariantIndex, TestIndex] == 1)
                {
                    //если в параметр  в варианте выигрыша совпал с вариантом на карте считаем это в параметре WinState
                    if (TestMap[TestIndex] == 1) WinState++;
                }
            }
            //если найдены 3 совпадения значит это и есть выигрышная комбинация
            if (WinState == 3) return true;
        }
        return false;
    }

    // Use this for initialization
    void Start() { }



    // Update is called once per frame
    void Update() { }
}
		
	

