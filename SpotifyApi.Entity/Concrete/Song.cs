using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.Concrete
{
    public class Song
    {
        
            public List<Item> items { get; set; }
            public List<Track> track { get; set; }
            public List<Albumm> album { get; set; }

   }



        public class Albumm
        {
            public string id { get; set; }

            public string name { get; set; }

        }





        //public class ExternalUrls
        //{
        //    public string spotify { get; set; }
        //}



        public class Item
        {

            public Track track { get; set; }
        }



        public class Track
        {

            public string id { get; set; }
            public string name { get; set; }

        }
    }

