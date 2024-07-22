using System;
using System.Collections.Generic;
using System.Text;
using xcab.como.common.Data;

namespace xcab.como.booker.Data.Variable
{
    public class Address
    {
        public Address()
        {
            this.suburb = new Suburb();
        }

        public string line1 { get; set; }

        public string line2 { get; set; }

        public Suburb suburb { get; private set; }

        public string addressifyString { get; set; }

        public string name { get; set; }
    }

    public class Suburb : IdentityDefinition
    {

    }
}
