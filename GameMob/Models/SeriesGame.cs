//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameMob.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeriesGame
    {
        public SeriesGame()
        {
            this.Games = new HashSet<Game>();
        }
    
        public int idSeries { get; set; }
        public string nameSeries { get; set; }
        public string imgSeries { get; set; }
    
        public virtual ICollection<Game> Games { get; set; }
    }
}
