using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Organization
{
    public class Organization
    {
        int index;
        string id;
        string name;
        string website;
        string country;
        string description;
        int founded;
        string industry;
        int employeesNumber;

        public Organization(string[] mezok)
        {
            if (mezok.Length != 9)
                throw new ArgumentException("Nem megfelelő számú mező!");

            if (!int.TryParse(mezok[0], out this.index))
                throw new ArgumentException("Hibás értékű mező! [index]");
            this.id = mezok[1];
            this.name = mezok[2];
            this.website = mezok[3];
            this.country = mezok[4];
            this.description = mezok[5];
            if (!int.TryParse(mezok[6], out this.founded))
                throw new ArgumentException("Hibás értékű mező! [founded]");
            this.industry = mezok[7];
            if (!int.TryParse(mezok[8], out this.employeesNumber))
                throw new ArgumentException("Hibás értékű mező! [employeesNumber]");
        }

        public int Index { get => index; }
        public string Id { get => id; }
        public string Name { get => name; }
        public string Website { get => website; }
        public string Country { get => country; }
        public string Description { get => description; }
        public int Founded { get => founded; }
        public string Industry { get => industry; }
        public int EmployeesNumber { get => employeesNumber; }
    }
}

