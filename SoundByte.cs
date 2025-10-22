using System.Media;

namespace PurpleNotebook.Audio
{
    public class SoundByte
    {
        public void PlaySaveSound()
        {
            SoundPlayer player = new SoundPlayer("Assets/save.wav");
            player.Play();
        }
    }
}
