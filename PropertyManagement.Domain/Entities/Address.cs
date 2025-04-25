using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.Entities
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }       

        /// <summary>
        /// Address Line1
        /// </summary>
        public string  Line1 { get; set; }

        /// <summary>
        /// Address Line2
        /// </summary>
        public string Line2 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// ZipCode
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public  string Country { get; set; }

        public Property Property { get; set; }
    }
}
