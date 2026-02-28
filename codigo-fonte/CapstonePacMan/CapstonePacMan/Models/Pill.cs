using CapstonePacMan.Game;
using Microsoft.UI.Xaml.Media.Imaging;

namespace CapstonePacMan.Models;

public class Pill : GameEntity
{
    public bool IsPower { get; }
    private static readonly BitmapImage PillImage;

    static Pill()
    {
        try
        {
            PillImage = new BitmapImage(
                new Uri("ms-appx:///Assets/Sprites/Pill.png")
            );
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine("URI do sprite da Pill é inválida.");
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar o sprite da Pill.");
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public Pill(double tileX, double tileY, int tileSize, bool isPower)
    {
        IsPower = isPower;

        Size = isPower ? 25 : 12;
        
        X = tileX * tileSize + (tileSize - Size) / 2;
        Y = tileY * tileSize + (tileSize - Size) / 2;
        
        Sprite = new Image
        {
            Width = Size,
            Height = Size,
            Source = PillImage,
            Stretch = Stretch.Uniform
        };
    }
    
    public override void Update(GameMap map)
    {
        // pill não se move
    }
}
