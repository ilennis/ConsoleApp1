using System;
using System.Collections.Generic;

internal class Program
{
    // 꼭 들어가야하는것
    // 1. 메인 메뉴 - 1. 상태 보기, 인벤토리, 상점
    // 2. 상태 보기 (레벨, 이름, 직업, 공격력, 방어력, 체력, 골드)
    // 3. 인벤토리
    // 4. 장비 장착
    // 5. 상점 및 아이템 구매

    //기타 
    // 휴식 (골드 -500G = 체력 회복)
    // 아이템 팔기
    // 아이템 장착은 1개만
    // 레벨업 기능 추가 (EXP)

    //던전!

    // Player 기본 값부터 지정
    static string playerName;
    static int playerLevel;
    static string playerClass;
    static int playerAttack = 10;
    static int playerDefense = 5;
    static float playerHealth = 100;
    static int playerGold = 1500;
    static int menu = 0;
    static bool isMenu = false;

    static bool equipping = false;
    static bool buying = false;
    static List<Monster> monsterList = new List<Monster>();
    static List<Item> inventory = new List<Item>();
    static List<Item> storeItemList = new List<Item>()
    {
        new Item("단검", Item.Type.Weapon, 3, 200, "일반적인 단검", false),
        new Item("권총", Item.Type.Weapon, 3, 500, "누군가 가지고 다니던 권총", false),
        new Item("샷건", Item.Type.Weapon, 3, 800, "치괴에 왜 샷건이 있지?", false),
        new Item("청바지", Item.Type.Armor, 3, 200, "생각보다 단단한 청바지?", false)
    };


    static Player player;

    static void Main(string[] args)
    {



        // 처음 게임 시작할때 게임 임력

        Console.Title = "공포의 치과 RPG";
        Console.WriteLine("치과에 오신걸 환영합니다.");
        Console.WriteLine("환자 이름이 어떻게 되세요?: ");
        playerName = Console.ReadLine();

        bool pc = false;
        while (!pc)
        {
            Console.Clear();
            Console.WriteLine("환자 직업은 어떻게 되세요?: ");
            Console.WriteLine("1. 전사 ");
            Console.WriteLine("2. 법사 ");
            Console.WriteLine("3. 궁수 ");
            playerClass = Console.ReadLine();
            switch (playerClass)
            {
                case "1":
                    playerClass = "전사";
                    pc = true;
                    break;
                case "2":
                    playerClass = "법사";
                    pc = true;
                    break;
                case "3":
                    playerClass = "궁수";
                    pc = true;
                    break;
                default:
                    Console.WriteLine("환자 직업을 다시 써주세요: ");
                    break;
            }
        }

        Console.Clear();
        bool isMenu = true;
        while(isMenu)
        {
            MainMenu();
        }
        
    }
    static void MainMenu()
    {
        menu = 0;
        Console.WriteLine("\n안녕하세요 {0}! 무엇을 확인하고 싶은가요?", playerName);
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리 보기");
        Console.WriteLine("3. 상점 열기");
        Console.WriteLine("4. 게임 종료");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                menu = 1;
                while (menu == 1)
                {
                    ShowPlayerInfo();
                }
                break;
            case "2":
                menu = 2;
                while (menu == 2)
                {
                    Inventory();
                }
                break;
            case "3":
                menu = 3;
                while (menu == 3)
                {
                    Shop();
                }
                break;
            case "4":
                menu = 4;
                while (menu == 4)
                {
                    EndGame();
                }
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    static void ShowPlayerInfo()
    {
        Console.Clear();
        Console.WriteLine("환자 이름: {0}", playerName);
        Console.WriteLine("환자 직업: {0}", playerClass);
        Console.WriteLine("공격력: {0}", playerAttack);
        Console.WriteLine("방어력: {0}", playerDefense);
        Console.WriteLine("체력: {0}", playerHealth);
        Console.WriteLine("Gold: {0} G", playerGold);

        Console.WriteLine("\n0. 나가기");

        Console.WriteLine("원하는 행동을 입력해 주세요");
        string input = Console.ReadLine();
        switch (input){
            case "0":
                Console.Clear();
                MainMenu();
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");

                break;
        }

    }
    static void Inventory()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine("[아이템 목록]");
        // 장비 보여주는 기능
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하는 행동을 입력해 주세요");

        string input = Console.ReadLine();
        switch (input)
        {
            case "0":
                Console.Clear();
                MainMenu();
                break;
            case "1":
                Console.Clear();
                equipping = true;
                while (equipping) {
                    Equipment();
                }
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }

    }

    static void Equipment()
    {
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory[i];
            Console.WriteLine($"{i + 1}. {item.Name} - {item.type} | + {item.stat} | {item.description}");
        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하는 행동을 입력해 주세요");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= storeItemList.Count)
        {
            if (choice == 0)
                Inventory(); // 다시 시작
            var selectedItem = inventory[choice - 1];
            
        }

        else
        {
            Console.WriteLine("다시 입력해 주세요.");
        }

    }
    static void Shop()
    {
        int index;
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine("{0} G", playerGold);
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeItemList.Count; i++)
        {
            var item = storeItemList[i];
            Console.WriteLine($"{i + 1}. {item.Name} - {item.type} | + {item.stat} | {item.description} {(item.IsSold ? "Sold Out" : "Available")}");
        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하는 행동을 입력해 주세요");

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= storeItemList.Count)
        {
            if (choice == 0) 
                Shop(); // 다시 시작
            var selectedItem = storeItemList[choice - 1];
            if (selectedItem.IsSold)
            {
                Console.WriteLine("이미 다 팔렸습니다.");
            }
            else
            {
                if(playerGold >= selectedItem.price)
                {
                    playerGold = playerGold - selectedItem.price;
                    if (selectedItem.type == Item.Type.Weapon) index = 1;
                    else if (selectedItem.type == Item.Type.Armor) index = 2;
                    selectedItem.IsSold = true;

                    inventory.Add(new Item(selectedItem.Name, selectedItem.type, selectedItem.stat, selectedItem.price, selectedItem.description, true));
                    Console.WriteLine($"{selectedItem.Name} 구매 성공!!");
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다..");
                }
                
            }
        }
        
        else
        {
            Console.WriteLine("다시 입력해 주세요.");
        }

    }
    static void EndGame()
    {
      
    }
}
class Player // 플레이어 저장 용도
{
    public string name { get; set; }
    public int level { get; set; }
    public int hp { get; set; }
    public string pclass { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int gold { get; set; }
    public int exp { get; set; }
    public List<Item> itemList;

    public Player(string pname, int hp, string pc, int atk, int def, int gp, int xp, List<Item> itemList)
    {
        this.name = pname;
        this.pclass = pc;
        this.hp = hp;
        this.attack = atk;
        this.defense = def;
        this.gold = gp;
        this.exp = xp;
        this.itemList = itemList;

    }


}
class Item
{
    public enum Type { Weapon, Armor }
    public string Name { get; set; }

    public Type type { get; set; }
    public int stat { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public bool IsSold { get; set; }

    public Item(string name, Item.Type type, int stat, int price, string description, bool isSold)
    {
        this.Name = name;
        

        
        this.stat = stat;
        this.price = price;
        this.description = description;
        this.IsSold = isSold;
    }
}

class Monster
{

}
