using LibVLCSharp.Shared;

namespace CapstonePacMan.Services;

public class AudioService
{
    private readonly LibVLC _libVlc;
    private readonly LibVLC _libVLCLoop;
    private readonly MediaPlayer _ghostPlayer;
    private readonly MediaPlayer _pacManPlayer;

    private readonly Media _ghostMediaSiren;
    private readonly Media _ghostMediaFrightMode;
    private readonly Media _pacManMediaEatingPill;
    private readonly Media _pacManMediaEatGhost;
    private readonly Media _pacManMediaDeathing;
    private readonly Media _pacManMediaVictory;

    public static bool Muted { get; set; } = false;
    
    public AudioService()
    {
        Core.Initialize();
        _libVlc = new LibVLC();
        _libVLCLoop = new LibVLC("--input-repeat=60000");

        _ghostPlayer = new MediaPlayer(_libVLCLoop);
        _pacManPlayer = new MediaPlayer(_libVlc);

        try
        {
            _ghostMediaSiren       = LoadSound("GhostSiren.mp3");
            _ghostMediaFrightMode  = LoadSound("GhostFrightMode.mp3");
            _pacManMediaEatingPill = LoadSound("PacManEating.mp3");
            _pacManMediaDeathing   = LoadSound("PacManDeath.wav");
            _pacManMediaEatGhost   = LoadSound("PacManEatGhost.mp3");
            _pacManMediaVictory    = LoadSound("PacManVictory.mp3");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar arquivos de áudio.");
            Console.WriteLine(ex);
            throw;
        }
    }

    public void PlaySoundGhostSiren()
    {
        if (Muted) return;
        _ghostPlayer.Stop();
        _ghostPlayer.Play(_ghostMediaSiren);
    }

    public void PlaySoundGhostFright()
    {
        if (Muted) return;
        _ghostPlayer.Stop();
        _ghostPlayer.Play(_ghostMediaFrightMode);
    }

    public void StopGhostSound()
    {
        _ghostPlayer.Stop();
    }

    public void PlaySoundEatingPacMan()
    {
        if (Muted) return;
        _pacManPlayer.Stop();
        _pacManPlayer.Play(_pacManMediaEatingPill);
    }

    public void PlaySoundEatGhost()
    {
        if (Muted) return;
        _pacManPlayer.Stop();
        _pacManPlayer.Play(_pacManMediaEatGhost);
    }

    public void PlaySoundDeathingPacMan()
    {
        if (Muted) return;
        _pacManPlayer.Stop();
        _pacManPlayer.Play(_pacManMediaDeathing);
    }
    
    public void PlaySoundVictory()
    {
        if (Muted) return;
        _pacManPlayer.Stop();
        _pacManPlayer.Play(_pacManMediaVictory);
    }
    
    private Media LoadSound(string fileName)
    {
        var uri = new Uri(
            Path.Combine(
                AppContext.BaseDirectory, 
                "Assets", 
                "Sounds", 
                fileName
            )
        );
        return new Media(_libVlc, uri);
    }
}
