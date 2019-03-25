using System.Collections.Generic;

namespace FreshMedia.Player
{
    public interface IPlayList
    {
        NgNet.Collections.IReadOnlyList<string> Medias { get; }
        IEnumerable<string> AddMeidas(IEnumerable<string> collection);
        void RemoveMedias(IEnumerable<string> collection);
        void Clean();
        void AddMedia(string path);
    }
}
