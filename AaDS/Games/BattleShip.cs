using System.Text;

namespace AaDS.Games;

public class BattleShip
{
    private readonly ulong _ships;
    private ulong _shots;

    //bit mask of ships location like 0b11110000_00000111_00000000_00110000_00000010_01000000_00000000_00000000
    public BattleShip(ulong ships) => _ships = ships;
    
    public bool Shoot(string shot)
    {
        int index = ConvertShotToIndex();

        ulong shotBit = (ulong) 1L << index;
        _shots |= shotBit;

        return (_ships & shotBit) != 0;
        
        int ConvertShotToIndex()
        {
            if (shot is not { Length: 2 })
            {
                throw new ArgumentException("Invalid shot format");
            }

            char row = shot[0];
            char column = shot[1];

            int rowIndex = 7 - (row - 'A'); // инверсия
            int colIndex = 7 - (column - '1'); // инверсия

            if (rowIndex < 0 || rowIndex > 7 || colIndex < 0 || colIndex > 7)
            {
                throw new ArgumentException("Invalid shot format");
            }

            return rowIndex + colIndex * 8;
        }
    }

    public string State()
    {
        StringBuilder result = new();

        for (int i = 0; i < 64; i++)
        {
            var cell = GetCellState(i);

            result.Append(cell);

            if (i % 8 == 7)
            {
                result.Append('\n');
            }
        }

        return result.ToString();
        
        char GetCellState(int index)
        {
            ulong mask = (ulong) 1L << (63 - index); // инверсия

            bool hasShip = (_ships & mask) != 0;
            bool hasShot = (_shots & mask) != 0;

            return hasShip switch
            {
                true when hasShot => '☒',
                true => '☐',
                _ => hasShot ? '×' : '.'
            };
        }
    }
}