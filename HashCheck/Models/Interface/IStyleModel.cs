using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.Models.Interface
{
    public interface IStyleModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public bool IsTransparent { get; set; }
    }
}
