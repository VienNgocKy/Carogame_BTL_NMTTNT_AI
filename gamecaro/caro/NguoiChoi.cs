using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caro
{
    public class NguoiChoi
    {
        public int NgChoi { get; set; }
        public string Ten { get; set; }
        public Image HinhDaiDien { get; set; }
        public NguoiChoi(string name, Image Anh, int SoHuu)
        {
            Ten = name;
            HinhDaiDien = Anh;
            NgChoi = SoHuu;
        }
    }
}
