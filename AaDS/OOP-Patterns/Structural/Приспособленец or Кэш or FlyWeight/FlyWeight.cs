using System.Drawing;

namespace AaDS.OOP_Patterns.Structural.Приспособленец_or_Кэш_or_FlyWeight;

/*
Легковес — это структурный паттерн, который экономит память, 
благодаря разделению общего состояния, вынесенного в один объект, 
между множеством объектов.

Легковес позволяет экономить память, кешируя одинаковые данные, 
используемые в разных объектах.

Применимость: Весь смысл использования Легковеса — в экономии памяти. 
Поэтому, если в приложении нет такой проблемы, то вы вряд ли найдёте там примеры Легковеса.

Признаки применения паттерна: Легковес можно определить по создающим методам класса, 
которые возвращают закешированные объекты, вместо создания новых.
*/

class Tree
{
    public int X { get; set; }
    public int Y { get; set; }

    TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        X = x;
        Y = y;
        _type = type;
    }

    public void Draw()
    {
        _type.Draw(X, Y);
    }
}

class TreeType
{
    private string _name;
    private Color _color;
    private string _otherData;

    public TreeType(string name, Color color, string otherData)
    {
        _name = name;
        _color = color;
        _otherData = otherData;
    }

    public void Draw(int x, int y)
    {
        //Console.WriteLine($"{x}:{y} – {_color}-{_name}");
    }
}

static class TreeFactory
{
    static Dictionary<string, TreeType> _treeTypes = new();

    public static TreeType GetTreeType(string name, Color color, string otherTreeData)
    {
        TreeType result;
        if (!_treeTypes.TryGetValue(name, out var type))
        {
            result = new TreeType(name, color, otherTreeData);
            _treeTypes[name] = result;
        }
        else
        {
            result = type;
        }

        return result;
    }
}

class Forest
{
    List<Tree> _trees = new();

    public void PlantTree(int x, int y, string name, Color color, string otherTreeData)
    {
        var type = TreeFactory.GetTreeType(name, color, otherTreeData);
        var tree = new Tree(x, y, type);
        _trees.Add(tree);
    }

    public void Paint()
    {
        foreach (var tree in _trees)
        {
            tree.Draw();
        }
    }
}

public class Demo
{
    static int CANVAS_SIZE = 500;
    static int TREES_TO_DRAW = 1000000;
    static int TREE_TYPES = 2;

    public static void Test() {
        Forest forest = new Forest();
        for (int i = 0; i < TREES_TO_DRAW / TREE_TYPES; i++) {
            forest.PlantTree(Random(0, CANVAS_SIZE), Random(0, CANVAS_SIZE),
                "Summer Oak", Color.Green, "Oak texture stub");
            forest.PlantTree(Random(0, CANVAS_SIZE), Random(0, CANVAS_SIZE),
                "Autumn Oak", Color.Orange, "Autumn Oak texture stub");
        }

        
        Console.WriteLine(TREES_TO_DRAW + " trees drawn");
        Console.WriteLine("---------------------");
        Console.WriteLine("Memory usage:");
        Console.WriteLine("Tree size (8 bytes) * " + TREES_TO_DRAW);
        Console.WriteLine("+ TreeTypes size (~30 bytes) * " + TREE_TYPES + "");
        Console.WriteLine("---------------------");
        Console.WriteLine("Total: " + ((TREES_TO_DRAW * 8 + TREE_TYPES * 30) / 1024 / 1024) +
                           "MB (instead of " + ((TREES_TO_DRAW * 38) / 1024 / 1024) + "MB)");
    }

    static int Random(int min, int max) {
        return min + System.Random.Shared.Next(min, max);
    }
}

/*
Отношения с другими паттернами
Компоновщик часто совмещают с Легковесом, чтобы реализовать 
общие ветки дерева и сэкономить при этом память.

Легковес показывает, как создавать много мелких объектов, а Фасад показывает, 
как создать один объект, который отображает целую подсистему.

Паттерн Легковес может напоминать Одиночку, если для конкретной задачи у вас получилось свести 
количество объектов к одному. Но помните, что между паттернами есть два кардинальных отличия:

1. В отличие от Одиночки, вы можете иметь множество объектов-легковесов.
2. Объекты-легковесы должны быть неизменяемыми, тогда как объект-одиночка допускает изменение своего состояния.
*/