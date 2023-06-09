using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck.Models
{
    public interface IHashModel
    {
        public string HashName { get; set; }

        public bool IsSelected { get; set; }
    }
}
