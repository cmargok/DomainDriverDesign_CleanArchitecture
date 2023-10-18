using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Video()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Actores = new HashSet<Actor>();
        }
        
        public string? Nombre { get; set; }

        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }

        public virtual ICollection<Actor> Actores { get; set; }

        public virtual Director Director { get; set; }

    }
}
