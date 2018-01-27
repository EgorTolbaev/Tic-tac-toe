using UnityEngine;

public class ImgChame : MonoBehaviour {

    //статус ячейки
    public int Status = 0;

    //3 картинки (пусто крест и ноль)
    public Sprite[] Imgs = new Sprite[3];

    //смена статуса ячейки
    void ChangeImg()
    {
        //получаем компонент игрового поля картинку 
        //если поле пусто то меняем значение иначе оставляем как есть
        SpriteRenderer MyImage = this.GetComponent("SpriteRenderer") as SpriteRenderer;

        //в зависимости от статуса меняем изображение
        switch (Status)
        {
            case 0: MyImage.sprite = Imgs[0]; break; //пустое поле
            case 1: MyImage.sprite = Imgs[1]; break; // крестик
            case 2: MyImage.sprite = Imgs[2]; break; // нолик
        }
    }

    //обработка нажатия мыши на поле
    void OnMouseDown()
    {
        //находим камеру
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        //получаем ссылку на наш скрипт
        MainGame P = A.GetComponent<MainGame>();

        switch (P.GameMode)
        {
            case 1:
                //задаем игроку ту картинку которую он бырал
                P.Player = Status;
                //если наша картинка равна 1 значит компьютеру нужно задать 2 а нам 1
                if (Status == 1) P.Comp = 2; //0
                if (Status == 2) P.Comp = 1; //X
                break;
            case 2:
                if(P.CanStep)
                if (Status == 0)
                {
                    Status = P.Player;
                    P.CanStep = false;
                }
                break;
        }
    }
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeImg();

    }
}
