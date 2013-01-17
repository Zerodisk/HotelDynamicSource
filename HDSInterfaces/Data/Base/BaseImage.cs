using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class BaseImage : Identification 
    {
        public string Description { get; set; }
        public string URL { get; set; }
        public string UrlThumbnail { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
