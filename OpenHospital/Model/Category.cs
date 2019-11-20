using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHospital.Model
{
    public class Category
    {
        string id;
        string category;

        public string Id { get => id; set => id = value; }
        public string Category1 { get => category; set => category = value; }

        public Category(string id, string category1)
        {
            Id = id;
            Category1 = category1;
        }
    }
}
