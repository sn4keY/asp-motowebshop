using System;
using System.Collections.Generic;
using System.Text;

namespace MotoWebShop.Common
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureURL { get; set; }

        public int ManufacturerId { get; set; }
    }
}
